using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InWaterBehaviour : StateMachineBehaviour
{
    private PlayerController _playerController;
    private Rigidbody _rb;
    private float _waitTimer;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _waitTimer = 0f;
        _rb = animator.GetComponent<Rigidbody>();
        _playerController = animator.gameObject.GetComponent<PlayerController>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _waitTimer += Time.deltaTime;
        if (_waitTimer>=0.5f)
        {
            if (_playerController.OnFetchChecker() == false)
            {
                _rb.drag = 0f;
                _playerController.ToCheckPoint();

            }
            if (_playerController.OnFetchChecker()==true)
            {
                
                animator.SetBool("InWater", false);
            }

        }
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
