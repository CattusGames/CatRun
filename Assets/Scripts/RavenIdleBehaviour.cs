using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RavenIdleBehaviour : StateMachineBehaviour
{
    private Raven _raven;
    private float _attackDistance;
    [Range(0,2)]
    [SerializeField] private float _attackTriggerDistance;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _raven = animator.GetComponent<Raven>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _attackDistance = Vector3.Distance(_raven.transform.position,_raven._player.transform.position);
        if (_attackDistance<=_attackTriggerDistance)
        {
            animator.SetTrigger("IsAttack");
            _raven._speed = 8f;
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
