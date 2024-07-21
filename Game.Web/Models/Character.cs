namespace Game.Web.Models;

public class Character
{
    public int HP { get; set; } = 150;
    public int Defence { get; set; } = 10;
    public int Damage { get; set; } = 20;

    public void Attack(Monster? monster)
    {
        monster.CurrentHp -= Damage = monster.Defence;
    }
}