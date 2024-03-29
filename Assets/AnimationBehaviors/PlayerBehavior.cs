﻿using UnityEngine;
using System.Collections;

public class PlayerBehavior : StateMachineBehaviour
{

    //OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    //override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
        
    //}

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    //
    //}

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("attack1");
        animator.ResetTrigger("attack2");
        animator.ResetTrigger("attack3");
        animator.ResetTrigger("dodge");
        animator.ResetTrigger("DemonMelee");
        animator.ResetTrigger("shootMagic");
        animator.ResetTrigger("chargeShot");
        animator.ResetTrigger("releaseShot");
        animator.GetComponent<PlayerControllerScript>().IsAttacking = false;
        animator.GetComponent<PlayerControllerScript>().IsDodging = false;
        animator.GetComponent<PlayerControllerScript>().IsBlocking = false;
        animator.GetComponent<PlayerControllerScript>().IsChargingAShot = false;
    }

    // OnStateMove is called right after Animator.OnAnimatorMove(). Code that processes and affects root motion should be implemented here
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    //
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK(). Code that sets up animation IK (inverse kinematics) should be implemented here.
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    //
    //}
}