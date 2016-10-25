using UnityEngine;
using System.Collections;
using System;

public class Enemy : CombatCharacter {

    private IAIState currentAIState;

    public GameObject Target { get; set; }

    [SerializeField]
    private float meleeRange;

    [SerializeField]
    private float projectileRange;
    

    // Use this for initialization
    public override void Start()
    {
        //Call the start method of the parent class
        base.Start();
        ChangeState(new IdleState());
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (!IsDead())
        {

            //Look at it's taget if he has one
            LookAtTarget();

            //If he is not taking damage then he can execute his state. This makes it so that he stands still after taking damage
            if (!IsTakingDamage)
            {
                //Executes the current Ai state
                currentAIState.Execute();
            }

        }
        else
            Die();
        
	}
    //Changes the current Ai state
    public void ChangeState(IAIState newState)
    {
        if( currentAIState != null)
        {
            //Runs the state exit code
            currentAIState.Exit();
        }

        currentAIState = newState;

        //Do enter state code and send our self as the Enemy
        currentAIState.Enter(this);
    }

    public void Move()
    {
        //Currently can only move if he is not attacking
        if (!IsAttacking)
        {
            //Tell animator that we are moving
            ThisAnimator.SetFloat("movementSpeed", 1);

            //Translate
            transform.Translate(new Vector3(1 * (characterStats.MovementSpeed * Time.deltaTime), 0,0));
        
        }
       

    }

    //Gets a vector of which direction they are facing
    public Vector2 GetDirection()
    {
        return isFacingRight ? Vector2.right : Vector2.left;
    }

    //Is called when this object triggers a collision
    public override void OnTriggerEnter2D(Collider2D other)
    {
        //Enemies Don't take damage from other enemies
        if(other.tag != "EnemyDamage")
        {
            //Tell parent about collision
            base.OnTriggerEnter2D(other);

            //Tells the current state that there was a collision with an object
            currentAIState.OnTriggerEnter(other);
        }
    }

    public bool InMeleeRange
    {
        get
        {
            //If you have a target And the distance to it is less then melee range or this character then return true else return false
            if (Target != null)
            {

                return Vector2.Distance(transform.position, Target.transform.position) <= meleeRange;
            }
            else
                return false;
        }
    }

    public bool InProjectileRange
    {
        get
        {
            //If you have a target And the distance to it is less then projectile range or this character then return true else return false
            if (Target != null)
            {

                return Vector2.Distance(transform.position, Target.transform.position) <= projectileRange;
            }
            else
                return false;
        }
    }


    private void LookAtTarget()
    {
      
        if(Target != null)
        {
            //If target has died then remove it
            if (Target.GetComponent<Character>().IsDead())
            {
                Target = null;
                ChangeState(new IdleState());
            }
            else
            {
                float distance = Target.transform.position.x - transform.position.x;

                //if distance is negative then the target is to the left else they are to the right
                //If im already looking there then don't do anything else change direction

                if (distance < 0 && isFacingRight || distance > 0 && !isFacingRight)
                {

                    ChangeDirection();
                }
            }

        }
    }

}
