using FastEndpoints;
using FastEndpoints.Security;
using Microsoft.AspNetCore.Identity;

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
        var newUser = new ApplicationUser()
        {
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

        await SendOkAsync(token,ct);
    }
}