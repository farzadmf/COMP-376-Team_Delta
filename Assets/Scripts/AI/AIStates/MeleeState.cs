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
            thisEnemy.ChangeState(new PatrolState());
        }
    }
    //Should be triggered when we enter this state holds a reference to its enemy
    public void Enter(Enemy enemy)
    {
        Debug.Log("In Melee State");
        thisEnemy = enemy;
        attackCooldown = ATTACK_COOLDOWN;
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

        if (canAttack)
        {
            canAttack = false;
            thisEnemy.ThisAnimator.SetTrigger("basicAttack");
        }

    }
}
