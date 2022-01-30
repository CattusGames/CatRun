using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InWaterBehaviour : StateMachineBehaviour
{
    private PlayerController _playerController;
    private GameManager _gameManager;
    private ScoreManager _scoreManager;
    private Rigidbody _rb;
    private float _waitTimer;
    private Water _water;
    private bool _preAdActivate = false;
    private bool _endActivate = false;
    private bool _life = true;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _preAdActivate = false;
        _scoreManager = animator.gameObject.GetComponentInParent<ScoreManager>();
        _gameManager = FindObjectOfType<GameManager>();
        _water = FindObjectOfType<Water>();
        _waitTimer = 0f;
        _rb = animator.GetComponentInParent<Rigidbody>();
        _playerController = animator.gameObject.GetComponentInParent<PlayerController>();


    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (_scoreManager._score >= 5 && _life==true)
        {
            if (_preAdActivate==false)
            {
                _gameManager.PreEndPanelActivation();
                _preAdActivate = true;
            }
            
            _waitTimer += Time.deltaTime;
            if (_waitTimer < 2f)
            {
                _gameManager._endButton.SetActive(false);
            }

            if (_gameManager._deathAd == true)
            {
                _gameManager.StartGamePanelActivation();
                if (_playerController.OnFetchChecker() == false)
                {
                    _rb.drag = 0f;
                    _playerController.ToCheckPoint();

                }
                if (_playerController.OnFetchChecker() == true)
                {
                    _gameManager.StartGamePanelActivation();
                    animator.SetBool("InWater", false);
                    _water.DecrementPosition();
                    _water._isMove = true;
                    _gameManager._deathAd = false;
                    _life=false;
                }
                _gameManager._deathAd = false;
            }
            else
            {
                _gameManager._deathAd = false;
                _gameManager._endButton.SetActive(true);

            }
        }
        else
        {
            if (_endActivate==false&&_gameManager._deathAd == false&& _playerController.OnFetchChecker() == false)
            {
                _gameManager.EndPanelActivation();
                _endActivate = true;
            }

        }

        

    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _gameManager.StartGamePanelActivation();

    }

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
