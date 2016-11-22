using UnityEngine;
using System.Collections;

public class IdleState : IAIState {

    //Handle to an enemy
    private Enemy thisEnemy;

    //Timer to leave Idle state if timer excedes duration
    private float idleTimer;

    //Duraction of idle time
    private float idleDuration;

    //Default idle duration
    private const float DEFAULT_IDLE_MIN = 1;
    private const float DEFAULT_IDLE_MAX = 5;


    //Runs while in the current State
    public void Execute()
    {
        Idle();

        //If while Idling the enemy finds a target
        if(thisEnemy.Target != null)
        {
            if(!thisEnemy.cantMove)
               thisEnemy.ChangeState(new ChasingState());
            else
            {
                //if he is in range for projectile attack then go into ranged state
                if (thisEnemy.InProjectileRange)
                    thisEnemy.ChangeState(new RangedState());
                else if (thisEnemy.InMeleeRange)
                    thisEnemy.ChangeState(new MeleeState());
            }
        }
    }
    //Should be triggered when we enter this state holds a reference to its enemy
    public void Enter(Enemy enemy)
    {
        thisEnemy = enemy;
        idleDuration = Random.Range(DEFAULT_IDLE_MIN, DEFAULT_IDLE_MAX);
    }
    //Should be triggered when we exit this state
    public void Exit()
    {

    }
    //Handles collisions with other objects
    public void OnTriggerEnter(Collider2D otherObject)
    {

    }

    //method that tells this enemy's animator that it is idleing
    private void Idle()
    {

        thisEnemy.ThisAnimator.SetFloat("movementSpeed", 0);

        if (!thisEnemy.cantMove)
        {
            idleTimer += Time.deltaTime;

            if (idleTimer >= idleDuration)
            {
                thisEnemy.ChangeState(new PatrolState());
            }
        }
      
        
    }
}
