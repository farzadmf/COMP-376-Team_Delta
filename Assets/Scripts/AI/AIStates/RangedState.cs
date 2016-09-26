using UnityEngine;
using System.Collections;

public class RangedState : IAIState {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    //Runs while in the current State
    public void Execute()
    {

    }
    //Should be triggered when we enter this state holds a reference to its enemy
    public void Enter(Enemy enemy)
    {

    }
    //Should be triggered when we exit this state
    public void Exit()
    {

    }
    //Handles collisions with other objects
    public void OnTriggerEnter(Collider2D otherObject)
    {

    }
}
