namespace Game.Combat;

public class Battle
{
    public string Id { get; set; } = Guid.NewGuid().ToString();

    public Monster Monster { get; set; }

    public Battle(Monster monster)
    {
        Monster = monster;
    }
}