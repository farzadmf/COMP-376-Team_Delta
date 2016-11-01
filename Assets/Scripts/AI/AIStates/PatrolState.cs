using UnityEngine;
using System.Collections;

public class PatrolState : IAIState {

    //Handle to an enemy
    private Enemy thisEnemy;

    //Timer to leave Patroling state if timer excedes duration
    private float patrolTimer;

    //Max Duraction of idle time
    private float patrolDuration;

    //Default idle duration to
    private const float DEFAULT_PATROL_MIN = 2;
    private const float DEFAULT_PATROL_MAX = 6;



    //Runs while in the current State
    public void Execute()
    {
        Patrol();
        thisEnemy.Move();

        //If the enemy finds a target while patroling
        if(thisEnemy.Target != null)
        {
            //if he is in range for projectile attack then go into ranged state
            if (thisEnemy.InProjectileRange)
                thisEnemy.ChangeState(new RangedState());
            else if(thisEnemy.InMeleeRange)
                thisEnemy.ChangeState(new MeleeState());
        }

    }
    //Should be triggered when we enter this state holds a reference to its enemy
    public void Enter(Enemy enemy)
    {
          thisEnemy = enemy;
        patrolDuration = Random.Range(DEFAULT_PATROL_MIN, DEFAULT_PATROL_MAX);

    }
    //Should be triggered when we exit this state
    public void Exit()
    {

    }
    //Handles collisions with other objects
    public void OnTriggerEnter(Collider2D otherObject)
    {
        //If collides with a platform edge then change direction
        if(otherObject.tag == "PlatformEdge")
        {
            thisEnemy.ChangeDirection();
        }
    }

    //method that tells this enemy's animator that it is idleing
    private void Patrol()
    {
       
        patrolTimer += Time.deltaTime;

        //When patrol times has exceded the duration then go into idle state
        if (patrolTimer >= patrolDuration)
        {
            thisEnemy.ChangeState(new IdleState());
        }

    }


}
