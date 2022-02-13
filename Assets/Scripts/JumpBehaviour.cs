using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpBehaviour : StateMachineBehaviour
{
    private PlayerController _playerController;
    private TrajectoryRenderer _trajectory;
    private Rigidbody _rb;
    private float _maxMagnitude;
    private float _recentMagnitude;
    private float _jumpProgress;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _rb = animator.GetComponentInParent<Rigidbody>();
        _playerController = animator.gameObject.GetComponentInParent<PlayerController>();
        _trajectory = _playerController.Trajectory;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _recentMagnitude = _rb.velocity.magnitude;

        if (_playerController.OnFetchChecker() == true&&_recentMagnitude==0)
        {

            _jumpProgress = 0.1f;
            animator.SetFloat("JumpProgress", _jumpProgress);

        }
        else
        {
            if (_recentMagnitude<_maxMagnitude)
            {
                _maxMagnitude = _recentMagnitude;
                if (_jumpProgress<0.5f)
                {
                    

                    _jumpProgress += Time.deltaTime;
                    animator.SetFloat("JumpProgress", _jumpProgress);
                }
                else
                {
                    _jumpProgress += 0.5f;
                    animator.SetFloat("JumpProgress", _jumpProgress);
                }
               

            }
            else if (_recentMagnitude>_maxMagnitude)
            {
                _trajectory.HideTrajectory();
                if (_jumpProgress >= 1f)
                {
                    animator.SetBool("IsJump", false);
                }
                _jumpProgress += Time.deltaTime;
                animator.SetFloat("JumpProgress", _jumpProgress);
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
   // {
        //    // Implement code that processes and affects root motion
//
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}