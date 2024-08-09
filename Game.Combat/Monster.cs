namespace Game.Combat;

public class Monster
{
    public double Strength { get; }
    public double Endurance { get; }
    public double HP { get; }
    public double CurrentHP { get;  }

    public Monster(double strength, double endurance,double hp)
    {
        Strength = strength;
        Endurance = endurance;
        HP = hp;
        CurrentHP = hp;
    }
}