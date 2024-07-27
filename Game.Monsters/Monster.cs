namespace Game.Monsters;

public class Monster
{
    public double Strength { get; }
    public double Endurance { get; }

    public double HP => Endurance * 5.3;
    public double CurrentHP { get; set; }

    public Monster(double strength, double endurance)
    {
        Strength = strength;
        Endurance = endurance;
        CurrentHP = HP;
    }
}