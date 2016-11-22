using UnityEngine;
using System.Collections;
using System;

public class ChasingState : IAIState {
    //Handle to an enemy
    private Enemy thisEnemy;

    private float chaseDelay;


    //Runs while in the current State
    public void Execute()
    {

        //If the enemy finds a target while patroling
        if (thisEnemy.Target == null)
        {
             //If he lost his target then go back to patroling
             thisEnemy.ChangeState(new PatrolState());
         }
        else
        {
            //if he is in range for projectile attack then go into ranged state
            if (thisEnemy.InProjectileRange)
                thisEnemy.ChangeState(new RangedState());
            //if he is in melee range then go into MeleeState
            else if (thisEnemy.InMeleeRange)
                thisEnemy.ChangeState(new MeleeState());
            else
            {
                //Wait for chase delay before chasing
                if (chaseDelay < Time.time)
                    thisEnemy.Move();
                else
                    thisEnemy.StopMoving();
            }
        }
        
      

    }
    //Should be triggered when we enter this state holds a reference to its enemy
    public void Enter(Enemy enemy)
    {
        thisEnemy = enemy;
        chaseDelay = enemy.delayBeforeChasing + Time.time;

    }


    //Should be triggered when we exit this state
    public void Exit()
    {

    }
    //Handles collisions with other objects
    public void OnTriggerEnter(Collider2D otherObject)
    {
        //If collides with a platform edge then change direction
        if (otherObject.tag == "PlatformEdge")
        {
            thisEnemy.ChangeDirection();
        }
    }


}
