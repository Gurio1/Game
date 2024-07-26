using FastEndpoints;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Game.Users.Endpoints;

public class IsEmailUnique(UserManager<ApplicationUser> userManager) : Endpoint<IsEmailUniqueRequest>
{
    
    public override void Configure()
    {
        Post("/users/check-email");
        AllowAnonymous();
    }
    
    public override async Task HandleAsync(IsEmailUniqueRequest req, CancellationToken ct)
    {
        var isUnique = await userManager.Users.AnyAsync(u => u.Email == req.Email, cancellationToken: ct);
        
        await SendOkAsync(isUnique, ct);
    }
}