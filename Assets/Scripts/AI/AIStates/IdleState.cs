﻿using UnityEngine;
using System.Collections;

public class IdleState : IAIState {

    //Handle to an enemy
    private Enemy thisEnemy;

    //Timer to leave Idle state if timer excedes duration
    private float idleTimer;

    //Duraction of idle time
    private float idleDuration;

    //Default idle duration to 5 seconds
    private const float DEFAULT_IDLE_DURATION = 2;

    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }

    //Runs while in the current State
    public void Execute()
    {
        Idle();
    }
    //Should be triggered when we enter this state holds a reference to its enemy
    public void Enter(Enemy enemy)
    {
        thisEnemy = enemy;
        idleDuration = DEFAULT_IDLE_DURATION;
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

        idleTimer += Time.deltaTime;

        if (idleTimer >= idleDuration)
        {
            thisEnemy.ChangeState(new PatrolState());
        }
        
    }
}