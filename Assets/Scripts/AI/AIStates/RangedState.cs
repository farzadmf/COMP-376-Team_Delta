﻿using UnityEngine;
using System.Collections;

public class RangedState : IAIState {

    private Enemy thisEnemy;

    private float nextThrowTimer;
    private float throwCooldown;
    private bool canThrow;

    private const float RANGED_COOLDOWN = 3;

    //Runs while in the current State
    public void Execute()
    {
        //Thow projectile every loop
        ThrowProjectile();

        //Check if enemy is in melee range
        if (thisEnemy.InMeleeRange)
        {
            thisEnemy.ChangeState(new MeleeState());
        }

        //If there is a target then move towards him
        else if (thisEnemy.Target != null && !thisEnemy.cantMove)
        {
            thisEnemy.Move();
        }
        else if(thisEnemy.cantMove && thisEnemy.Target == null)
        {
            thisEnemy.ChangeState(new IdleState());
        }
    }
    //Should be triggered when we enter this state holds a reference to its enemy
    public void Enter(Enemy enemy)
    {
        thisEnemy = enemy;
        throwCooldown = RANGED_COOLDOWN;
        nextThrowTimer = 0;
    }
    //Should be triggered when we exit this state
    public void Exit()
    {

    }
    //Handles collisions with other objects
    public void OnTriggerEnter(Collider2D otherObject)
    {

    }

    private void ThrowProjectile()
    {
        Debug.Log(nextThrowTimer + "Current " + Time.time  );
        //Check the current time has exceeded the next throw timer
        if (nextThrowTimer < Time.time)
        {
            canThrow = true;
            //Set next timer to the cooldown + the current time
            nextThrowTimer = throwCooldown + Time.time;

        }

        if (canThrow)
        {
            canThrow = false;
            thisEnemy.ThisAnimator.SetTrigger("rangedAttack");
        }

    }
}
