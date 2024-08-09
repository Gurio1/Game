namespace Game.Combat;

public class Battle
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public Monster Monster { get; set; }
    public Player Player { get; set; }    

    public Battle(Monster monster,Player player)
    {
        Monster = monster;
        Player = player;
    }
}