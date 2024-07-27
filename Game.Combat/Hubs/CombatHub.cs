using Microsoft.AspNetCore.SignalR;

namespace Game.Combat.Hubs;

public class CombatHub(CombatService combatService) : Hub
{
    public override async Task OnConnectedAsync()
    {
        await Clients.All.SendAsync("ReceiveCombatLog", await combatService.CreateBattle());
        await base.OnConnectedAsync();
    }

    /*public async Task Attack()
    {
        var monster = await combatService.AttackMonster("1", _character);
        
        await Clients.All.SendAsync("ReceiveCombatLog", monster);
    }*/
}