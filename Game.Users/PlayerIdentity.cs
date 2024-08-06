using Microsoft.AspNetCore.Identity;

namespace Game.Users;

public class PlayerIdentity : IdentityUser
{
    public Guid PlayerId { get; set; }
}