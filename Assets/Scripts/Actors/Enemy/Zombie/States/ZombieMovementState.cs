using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieMovementState : StateMachineBehaviour
{
    private Zombie _zombie;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _zombie = animator.GetComponent<Zombie>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Vector3 targetForward = _zombie.Target.position.X0Z() - _zombie.transform.position.X0Z();
        _zombie.transform.rotation = Quaternion.LookRotation(Vector3.RotateTowards(_zombie.transform.forward.X0Z(),  targetForward, _zombie.MaxDegreeDelta * Mathf.Deg2Rad * Time.deltaTime, Mathf.Infinity), Vector3.up);
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
