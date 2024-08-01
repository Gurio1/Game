using FastEndpoints;
using FastEndpoints.Security;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Game.Users.Endpoints;

public class Create(UserManager<ApplicationUser> userManager) : Endpoint<CreateRequest>
{
    public override void Configure()
    {
        Post("/users");
        AllowAnonymous();
    }
    
    public override async Task HandleAsync(CreateRequest req, CancellationToken ct)
    {
        var isEmailNotUnique = await userManager.Users.AnyAsync(u => u.Email == req.Email, cancellationToken: ct);

        if (isEmailNotUnique)
        {
            ThrowError(request => request.Email,"This email is already taken");
        }
        
        var newUser = new ApplicationUser()
        {
            UserName = req.Email,
            Email = req.Email,
        };

        var result = await userManager.CreateAsync(newUser, req.Password);

        if (!result.Succeeded)
        {
            foreach (var er in result.Errors)
            {
                AddError(er.Description);
            }
            ThrowIfAnyErrors();
        }
        
        var jwtSecret = Config["Auth:JwtSecret"]!;
        var token = JwtBearer.CreateToken(opt =>
        {
            opt.SigningKey = jwtSecret;
            opt.User["EmailAddress"] = newUser.Email;
        });

        await SendOkAsync(new {token = token},ct);
    }
}