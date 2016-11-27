using UnityEngine;
using System.Collections;

public class MeleeState : IAIState {


    private Enemy thisEnemy;

    private float nextAttackTimer;
    private float attackCooldown;
    private bool canAttack;

    private const float ATTACK_COOLDOWN = 3;


    //Runs while in the current State
    public void Execute()
    {
        //Attack in every loop
        basicAttack();

        //If taget is not in range anymore for melee but is in range for projectiles
        if(!thisEnemy.InMeleeRange && thisEnemy.InProjectileRange)
        {
            thisEnemy.ChangeState(new RangedState());
        }
        //If target Is not in any range then go into patrol
        else if (!thisEnemy.InMeleeRange && !thisEnemy.InProjectileRange)
        {
            thisEnemy.ChangeState(new ChasingState());
        }
    }
    //Should be triggered when we enter this state holds a reference to its enemy
    public void Enter(Enemy enemy)
    { 
        thisEnemy = enemy;
        attackCooldown = enemy.delayBeforeAttacking;
        nextAttackTimer = attackCooldown + Time.time;
    }
    //Should be triggered when we exit this state
    public void Exit()
    {

    }
    //Handles collisions with other objects
    public void OnTriggerEnter(Collider2D otherObject)
    {

    }

    private void basicAttack()
    {

        //Check the current time has exceeded the next throw timer
        if (nextAttackTimer < Time.time)
        {
            canAttack = true;
            //Set next timer to the cooldown + the current time
            nextAttackTimer = attackCooldown + Time.time;

        }
        else
            thisEnemy.StopMoving();

        if (canAttack)
        {
            canAttack = false;
			if (thisEnemy.gameObject.tag != "Boss") {
				thisEnemy.ThisAnimator.SetTrigger ("basicAttack");
			}
            else
            {
                if (thisEnemy.targetDistance() <= 3)
                {
                    thisEnemy.ThisAnimator.SetTrigger("closeRangeAttack");
                }
                   
                else if (thisEnemy.targetDistance() < 5 && thisEnemy.targetDistance() > 3)
                {
                    thisEnemy.ThisAnimator.SetTrigger("mediumRangeAttack");
                }

                else if(thisEnemy.targetDistance() < 10 & thisEnemy.targetDistance() > 8)
                {
                    thisEnemy.ThisAnimator.SetTrigger("longRangeAttack");
                }
                else
                    thisEnemy.ChangeState(new ChasingState());

            }
        }

    }
}
