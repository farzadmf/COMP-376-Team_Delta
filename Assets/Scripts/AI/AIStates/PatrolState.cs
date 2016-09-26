using UnityEngine;
using System.Collections;

public class PatrolState : IAIState {

    //Handle to an enemy
    private Enemy thisEnemy;

    //Timer to leave Patroling state if timer excedes duration
    private float patrolTimer;

    //Max Duraction of idle time
    private float patrolDuration;

    //Default idle duration to 5 seconds
    private const float DEFAULT_PATROL_DURATION = 5;


    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    //Runs while in the current State
    public void Execute()
    {
        Patrol();
        thisEnemy.Move();
    }
    //Should be triggered when we enter this state holds a reference to its enemy
    public void Enter(Enemy enemy)
    {
        thisEnemy = enemy;
        patrolDuration = DEFAULT_PATROL_DURATION;

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
