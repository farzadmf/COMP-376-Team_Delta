
/// <summary>
/// Used to store Damage
/// </summary>
public class Damage {

    public int DamageValue { get; set; }
    public bool IsCrit { get; set; }
    public DamageType damageType { get; set; }


    //Constructor
    public Damage()
    {
        DamageValue = 0;
        IsCrit = false;
    }

    //Constructor
    public Damage(int damage)
    {
        DamageValue = damage;
        IsCrit = false;
    }

    //Constructor
    public Damage(int damage,bool isCrit)
    {
        DamageValue = damage;
        IsCrit = isCrit;
    }

    //Constructor
    public Damage(int damage, DamageType type)
    {
        DamageValue = damage;
        IsCrit = false;
        damageType = type;
    }

}
