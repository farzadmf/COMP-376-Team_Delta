using UnityEngine;
using System.Collections;

public class Enemy : CombatCharacter {

    private IAIState currentAIState;

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
        //Executes the current Ai state5
        currentAIState.Execute();
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
        //Tell animator that we are moving
        ThisAnimator.SetFloat("movementSpeed", 1);
        //Translate
        transform.Translate(GetDirection() * (movementSpeed * Time.deltaTime));

    }

    //Gets a vector of which direction they are facing
    public Vector2 GetDirection()
    {
        return isFacingRight ? Vector2.right : Vector2.left;
    }

    //Is called when this object triggers a collision
    void OnTriggerEnter2D(Collider2D otherObject)
    {
        //Tells the current state that there was a collision with an object
        currentAIState.OnTriggerEnter(otherObject);
    }
}
