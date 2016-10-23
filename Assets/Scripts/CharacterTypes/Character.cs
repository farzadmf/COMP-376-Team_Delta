using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class Character : MonoBehaviour {


    public Animator ThisAnimator { get; private set; }

    public Collider2D ThisCollider { get; private set; }

    public Rigidbody2D ThisRigidBody { get; private set; }

    public bool IsTakingDamage { get;  set; }

    //Character Stats Class, Serializable so that can view in Editor, Can be inherited
    [SerializeField]
    protected CharacterStats characterStats;

    //If not faceing right then facing left
    protected bool isFacingRight;

    //Tags of damage sources
    [SerializeField]
    private List<string> damageSources;

    // Use this for initialization
    public virtual void Start () {

        //By default a character will start by facing right
        isFacingRight = true;
        ThisAnimator = GetComponent<Animator>();
        ThisCollider = GetComponent<Collider2D>();
        ThisRigidBody = GetComponent<Rigidbody2D>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    //Called only after all objects are loaded, called only once
    public void Awake()
    {
        //Ignore Body collisions
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("EnemyBody"), LayerMask.NameToLayer("EnemyBody"), true);
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("PlayerBody"), LayerMask.NameToLayer("EnemyBody"), true);
    }

    public virtual void ChangeDirection()
    {
        //Change facing direction to the opposite of what it currently is
        isFacingRight = !isFacingRight;

        //Rotate 180 to go towards other direction
        transform.Rotate(new Vector3(transform.rotation.x, 180, transform.rotation.z));
    }

    public bool IsDead()
    {
        return characterStats.Health <= 0;
    }

    private void Die()
    {
        if (ThisAnimator != null)
        {
            ThisAnimator.SetTrigger("death");

            DeleteChildren();
        }
        else
        {
            //If has no death animation then just destroy the object
            Destroy(gameObject);
        }

    }

    private void DeleteChildren()
    {
        foreach(Transform child in transform)
        {
            Destroy(child.gameObject);
        }
    }

    /* ---------------------------------------------Combat--------------------------------------------*/

    /* ----------------Give Damage----------------*/

    //Calculates damage based on Character Stats And An Attack
    private Damage CalculateDamage(Attack attack, CharacterStats stats)
    {
        Damage damage = new Damage();

        float critMultiplier = GetCritDamage(stats.CritChance, stats.CritMultiplier);

        //Tell damage if this hit is a crit or not
        if (critMultiplier == 1)
            damage.IsCrit = false;
        else
            damage.IsCrit = true;

        //Calculate Damage Based on Crit multipler Base attack damage and Strenght
        damage.DamageValue = Mathf.RoundToInt(critMultiplier * (attack.BaseDamage + StrToDamage(stats.Strength) - DefenseToReducedDamage()));

        return damage;
    }

    //Checks if this attack is a crit if it is then return crit multiplier else return 1
    private float GetCritDamage(float chance, float multiplier)
    {
            //If this hit is a crit
            if (Random.value > (1- chance))
            {
                return multiplier;
            }
            else
                return 1.0f;
    }

    //Returns a damage multiplier based on Strength Currently Does 2 Times his strength in damage
    private int StrToDamage(int str)
    {
        return str * 2;

    }

    //Returns a damage multiplier based on Def Currently Reduces * 2 def
    private int DefenseToReducedDamage()
    {
        return characterStats.Defense * 2;

    }

    /* ----------------Take Damage----------------*/


    //For Collisions, Only these kinds of collisions will trigger a Force push if an attack has it
    public virtual void OnCollisionEnter2D(Collision2D other)
    {
        
        if (!IsDead())
        {
            //If character can take damage from this source
            if (damageSources.Contains(other.gameObject.tag))
            {
                Attack attack = other.gameObject.GetComponent<DamageDealer>().Attack;

                CharacterStats enemyStats = other.gameObject.transform.parent.gameObject.GetComponent<Character>().characterStats;

                if (enemyStats != null)
                {
                    TakeDamage(CalculateDamage(attack, enemyStats));
                }
                //Else A character did not do the damage, it was an object or the world so take base damage
                else
                    TakeDamage(new Damage(attack.BaseDamage - DefenseToReducedDamage()));


                //Check Force push values of attack and compare them to our stats
                //Adds a force based on collision direction but only if Character didn't resits it then applies the remainder of force after resistence
                //AddForce(attack, other);
            }
        }

    }

    //For Trigger Collisions
    public virtual void OnTriggerEnter2D(Collider2D other)
    {
       
        if (!IsDead())
        {
            //If character can take damage from this source
            if (damageSources.Contains(other.gameObject.tag))
            {
                Attack attack = other.gameObject.GetComponent<DamageDealer>().Attack;

                //If the damage source was a Character then calculate his stats else do Base damage
                if (other.gameObject.transform.parent && other.gameObject.transform.parent.gameObject.GetComponent<Character>())
                {

                    CharacterStats enemyStats = other.gameObject.transform.parent.gameObject.GetComponent<Character>().characterStats;

                    TakeDamage(CalculateDamage(attack, enemyStats));

                }
                //Else A character did not do the damage, it was an object or the world so take base damage
                else
                    TakeDamage(new Damage(attack.BaseDamage - DefenseToReducedDamage()));
            }
        }

    }



    //Deals with removing health, activating damage animations, and checking if dead then trigger death
    private void TakeDamage(Damage damage)
    {
        characterStats.decreaseHealth(damage.DamageValue);

        //Create a UI Damage Pop up
        DamagePopUpController.CreateDamagePopUp(damage.DamageValue.ToString(), new Vector2(transform.position.x + ThisCollider.bounds.size.x/4.0f, transform.position.y + ThisCollider.bounds.size.y/2.0f), damage.IsCrit);

        if(ThisAnimator != null)
            ThisAnimator.SetTrigger("damage");

        if (IsDead())
        {
            Die();
        }
 
    }

    //Adds a force based on collision direction but only if Character didn't resits it then applies the remainder of force after resistence
    private void AddForce(Attack attack, Collision2D other)
    {
        
        if (attack.Force > characterStats.MaxForceResistence)
        {
            //Get direction hit was received
            Vector2 direction = other.contacts[0].point - (new Vector2(transform.position.x,transform.position.y));

            direction = direction.normalized;

            if (ThisRigidBody)
            {
                Debug.Log(direction);
                ThisRigidBody.AddForce(new Vector2 (direction.x * (attack.Force - characterStats.MaxForceResistence),0.0f));
            }
            
        }
    }

}
