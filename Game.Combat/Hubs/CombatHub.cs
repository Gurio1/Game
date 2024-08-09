using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Serilog;

namespace Game.Combat.Hubs;

[Authorize]
public class CombatHub(CombatService combatService,ILogger logger) : Hub
{
    public override async Task OnConnectedAsync()
    {
        var userEmail =Context.User!.Claims.First(c => c.Type == "EmailAddress").Value;
        
        await Clients.All.SendAsync("ReceiveCombatLog", await combatService.CreateBattle(userEmail));
        await base.OnConnectedAsync();
    }

    public async Task Attack()
    {
        var userEmail =Context.User!.Claims.First(c => c.Type == "EmailAddress").Value;
        
        await combatService.AttackMonster(userEmail);
        
        await Clients.All.SendAsync("ReceiveCombatLog", "message");
    }
}