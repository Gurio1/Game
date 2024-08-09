namespace Game.Combat;

public class Player
{
    public double Strength { get; }
    public double Endurance { get; }
    public double HP { get; }
    public double CurrentHP { get;  }

    public Player(double strength, double endurance,double hp,double currentHp)
    {
        Strength = strength;
        Endurance = endurance;
        HP = hp;
        CurrentHP = currentHp;
    }
}