using UnityEngine;
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


    //Runs while in the current State
    public void Execute()
    {
        Idle();

        //If while Idling the enemy finds a target
        if(thisEnemy.Target != null)
        {
            thisEnemy.ChangeState(new PatrolState());
        }
    }
    //Should be triggered when we enter this state holds a reference to its enemy
    public void Enter(Enemy enemy)
    {
        Debug.Log("In Idle State");
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
