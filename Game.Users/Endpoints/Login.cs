using FastEndpoints;
using FastEndpoints.Security;
using Microsoft.AspNetCore.Identity;

namespace Game.Users.Endpoints;

public class Login(UserManager<ApplicationUser> userManager) : Endpoint<LoginRequest>
{
    public override void Configure()
    {
        Post("/users/login");
        AllowAnonymous();
    }
    
    public override async Task HandleAsync(LoginRequest req, CancellationToken ct)
    {
        var user = await userManager.FindByEmailAsync(req.Email);

        if (user == null)
        {
            await SendUnauthorizedAsync(ct);
            return;
        }

        var loginSuccessful = await userManager.CheckPasswordAsync(user, req.Password);

        if (!loginSuccessful)
        {
            await SendUnauthorizedAsync(ct);
            return;
        }

        var jwtSecret = Config["Auth:JwtSecret"]!;
        var token = JwtBearer.CreateToken(opt =>
        {
            opt.SigningKey = jwtSecret;
            opt.User["EmailAddress"] = user.Email!;
        });

        await SendAsync(token, cancellation: ct);
    }
}