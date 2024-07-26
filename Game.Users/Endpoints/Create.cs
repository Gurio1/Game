using FastEndpoints;
using Microsoft.AspNetCore.Identity;

namespace Game.Users.Endpoints;

internal class Create(UserManager<ApplicationUser> userManager) : Endpoint<CreateRequest>
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
            UserName = req.UserName,
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

        await SendOkAsync(ct);
    }
}