namespace Game.Characters;

public class Player 
{
    public Guid Id { get; set; } = Guid.Empty;

    public Guid BattleId { get; set; } = Guid.Empty;
    
    public string UserName { get; set; }
    
    public double Strength { get; set; }
    public double Endurance { get; set; }

    public double HP { get; set; }
    public double CurrentHP { get; set; }

    private Player()
    {
        //for ef core
    }
    
    public Player(Guid id,string userName,double strength, double endurance)
    {
        Id = id;
        UserName = userName;
        Strength = strength;
        Endurance = endurance;
        HP = endurance * 5.3;
        CurrentHP = HP;
    }
}