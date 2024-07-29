using Microsoft.AspNetCore.Identity;

namespace Game.Users;

public class ApplicationUser : IdentityUser
{
    public Guid PlayerId { get; set; }
}