using UnityEngine;
using System.Collections;

//Interface for the different AI states
public interface IAIState
{
    //Runs while in the current State
    void Execute();
    //Should be triggered when we enter this state holds a reference to its enemy
    void Enter(Enemy enemy);
    //Should be triggered when we exit this state
    void Exit();
    //Handles collisions with other objects
    void OnTriggerEnter(Collider2D otherObject);


}
