
/// <summary>
/// Holds a Character's Attributes, is Serializable
/// </summary>
[System.Serializable]
public class CharacterStats {

    /*Stats are all Public so they are visible in Unity Editor*/

    //Health
    //Affects if Character is alive or dead
    public int TotalHealth;
    //Current health
    public int Health;

    //Stamina
    //Physical Actions cost Stamina to perform (Jump, roll , block, etc) Stamina regenerates overtime.
    public int TotalStamina;
    //Current Stamina
    public int Stamina;

    //Strenght
    //Increase attack damage
    public int Strength;

    //Defense
    //Reduces incoming damage
    public int Defense;

    //Critical Hit Chance
    //Percent chance of increasing damage of an attack
    public float CritChance;

    //Percent of increase in Damage if it is a critical hit
    public float CritMultiplier;

    //Max Force Resistence
    //Character will be affected by the force of a damage source if the value is larger then this max value
    public float MaxForceResistence;

    //Movement Speed
    public float MovementSpeed;



    //Default constructor ---- Initializes with the defaults
    public CharacterStats()
    {
        TotalHealth = 0;
        Health = TotalHealth;
        TotalStamina = 0;
        Stamina = TotalStamina;
        Strength = 0;
        Defense = 0;
        CritChance = 0.0f;
        CritMultiplier = 0.0f;
        MaxForceResistence = 0.0f;
        MovementSpeed = 0.0f;
    }

    //Initilises all values with parameters
    public CharacterStats(int maxHealth,int maxStamina,int strength,int defense,float critChance, float critMultiplier, float maxForceResistence, float movementSpeed)
    {
        this.TotalHealth = maxHealth;
        this.TotalStamina = maxStamina;
        this.Health = TotalHealth;
        this.Stamina = TotalStamina;
        this.Strength = strength;
        this.Defense = defense;
        this.CritChance = critChance;
        this.CritMultiplier = critMultiplier;
        this.MaxForceResistence = maxForceResistence;
        this.MovementSpeed = movementSpeed;
    }

    /* ---------------Health--------------*/

    //Removes Health based on value provided
    public void decreaseHealth(int value)
    {
        Health -= value;
    }

    //Adds Health based on value provided
    public void increaseHealth(int value)
    {
        Health += value;
    }

    //Returns health to his max value
    public void maxRegenerateHealth()
    {
        Health = TotalHealth;
    }

    /* ---------------Stamina--------------*/


    //Removes Stamina based on value provided
    public void decreaseStamina(int value)
    {
        Stamina -= value;
    }

    //Adds Stamina based on value provided
    public void increaseStamina(int value)
    {
        Stamina += value;
    }

    //Returns stamina to his max value
    public void maxRegenerateStamina()
    {
        Stamina = TotalStamina;
    }

    /* ---------------Strength--------------*/


    //Removes Strength based on value provided
    public void decreaseStrength(int value)
    {
        Strength -= value;
    }

    //Adds Strength based on value provided
    public void increaseStrength(int value)
    {
        Strength += value;
    }


    /* ---------------Defense--------------*/


    //Removes Defense based on value provided
    public void decreaseDefense(int value)
    {
        Defense -= value;
    }

    //Adds Defense based on value provided
    public void increaseDefense(int value)
    {
        Defense += value;
    }

    /* ---------------Crit Chance--------------*/


    //Removes CritChance based on value provided
    public void decreaseCritChance(float value)
    {
        CritChance -= value;
    }

    //Adds CritChance based on value provided
    public void increaseCritChance(float value)
    {
        CritChance += value;
    }

    //Reduces CritMultiplier based on value provided
    public void decreaseCritMultiplier(float value)
    {
        CritMultiplier -= value;
    }

    //Adds CritMultiplier based on value provided
    public void increaseCritMultiplier(float value)
    {
        CritMultiplier += value;
    }

    /* ---------------Max Force--------------*/


    //Removes MaxForceResistence based on value provided
    public void decreaseMaxForceResistence(float value)
    {
        MaxForceResistence -= value;
    }

    //Adds MaxForceResistence based on value provided
    public void increaseMaxForceResistence(float value)
    {
        MaxForceResistence += value;
    }

    /* ---------------Movement Speed--------------*/


    //Removes MovementSpeed based on value provided
    public void decreaseMovementSpeed(int value)
    {
        MovementSpeed -= value;
    }

    //Adds MovementSpeed based on value provided
    public void increaseMovementSpeed(int value)
    {
        MovementSpeed += value;
    }



}


/// <summary>
/// Used in order to get Stats from the unity editor, is Serializable, Curently is not used Because we want to see stats in editor in real time Thefore The Character stats class was made serializable
/// </summary>
[System.Serializable]
public class EditorCharacterStats
{
    public int health, stamina, strength, defense;
    public float critChance, maxForceResistence, movementSpeed;
}
