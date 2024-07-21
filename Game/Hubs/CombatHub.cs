using Game.Models;
using Microsoft.AspNetCore.SignalR;

namespace Game.Hubs;

public class CombatHub(CombatService combatService) : Hub
{
    private readonly Character _character = new Character();

    public override async Task OnConnectedAsync()
    {
        await Clients.All.SendAsync("ReceiveCombatLog", await combatService.CreateBattle());
        await base.OnConnectedAsync();
    }

    public async Task Attack(string data)
    {
        var monster = await combatService.AttackMonster("1", _character);
        
        await Clients.All.SendAsync("ReceiveCombatLog", monster);
    }
}