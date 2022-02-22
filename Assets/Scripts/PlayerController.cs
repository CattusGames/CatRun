using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;
public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    private Rigidbody rb;
    private float _jumpMagnitude;
    private Vector3 _jumpDirection;
    private ScoreManager _scoreManager;
    public TrajectoryRenderer Trajectory;
    public LayerMask _whatIsFetch;
    [HideInInspector] public Vector3 _checkPoint;
    [HideInInspector] public bool _rotate, _touchActive;



    private Vector3 _mouseDownPos;
    private Vector3 _mouseUpPos;
    [SerializeField] private Transform _mainCamera;
    [SerializeField] private Transform _water;

    private Animator _animator;
    [SerializeField] private GameObject _body;
    [SerializeField] private GameManager _gameManager;
    private GameObject _highFetch;
    private void Start()
    {
        _scoreManager = gameObject.GetComponent<ScoreManager>();
        _highFetch = OnFetch();
        _animator = gameObject.GetComponentInChildren<Animator>();
        rb = gameObject.GetComponent<Rigidbody>();
        _rotate = false;
        _touchActive = true;
    }
    private void OnMouseDown()
    {
        Trajectory._time = 0;
            if (OnFetchChecker() == true && _touchActive == true)
            {
            _gameManager.StartGamePanelActivation();
            _gameManager._start = true;

                _checkPoint = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z);
                _animator.SetBool("IsJump", true);
                float rotation = _mainCamera.transform.rotation.eulerAngles.y;
                if (rotation == 0f)
                {
                    _mouseDownPos = Input.mousePosition;
                }
                else if (rotation == 180f)
                {
                    _mouseDownPos = new Vector3(-Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z);
                }
                else if (rotation == 90f)
                {
                    _mouseDownPos = new Vector3(Input.mousePosition.z, Input.mousePosition.y, -Input.mousePosition.x);
                }
                else if (rotation == 270f)
                {
                    _mouseDownPos = new Vector3(Input.mousePosition.z, Input.mousePosition.y, Input.mousePosition.x);
                }
            }
        
    }

    private void OnMouseDrag()
    {
        if (OnFetchChecker() == true&& _touchActive == true)
            {
                float rotation = _mainCamera.transform.rotation.eulerAngles.y;
                if (rotation == 0f)
                {
                    _mouseUpPos = Input.mousePosition;
                    JumpRotate(_mouseDownPos, _mouseUpPos, _body);
                }
                else if (rotation == 180f)
                {
                    _mouseUpPos = new Vector3(-Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z);
                    JumpRotate(_mouseDownPos, _mouseUpPos, _body);

                }
                else if (rotation == 90f)
                {
                    _mouseUpPos = new Vector3(Input.mousePosition.z, Input.mousePosition.y, -Input.mousePosition.x);
                    JumpRotate(_mouseDownPos, _mouseUpPos, _body);

                }
                else if (rotation == 270f)
                {
                    _mouseUpPos = new Vector3(Input.mousePosition.z, Input.mousePosition.y, Input.mousePosition.x);
                    JumpRotate(_mouseDownPos, _mouseUpPos, _body);

                }
                _jumpDirection = (_mouseDownPos - _mouseUpPos) / 4;
                _jumpMagnitude = _jumpDirection.magnitude / 100;
                Vector3 speed = _jumpDirection * _jumpMagnitude / 50;
                Trajectory.ShowTrajectory(gameObject.transform.localPosition, speed);

            }

        
    }

    private void OnMouseUp()
    {
        Trajectory._time = 0;

        if (OnFetchChecker() == true&& _touchActive == true)
            {
                rb.AddForce(_jumpDirection * _jumpMagnitude, ForceMode.Acceleration);
            }
    }
    private void JumpRotate(Vector3 mouseDown,Vector3 mouseUp,GameObject body)
    {
        Vector3 focus;
        focus = Vector3.Scale(mouseDown - mouseUp, new Vector3(1, 0, 1));
        if (focus.z < 0)
        {
            focus.y = 180;
            focus.x = 0;
            focus.z = 0;
            focus = Vector3.Scale(mouseDown - mouseUp, new Vector3(1, 0, 1));
        }
        body.transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(focus), 1f);
    }

    public GameObject OnFetch()
    {
        Collider[] hitColliders = Physics.OverlapBox(gameObject.transform.position, transform.localScale/2, Quaternion.identity, _whatIsFetch);
        if (hitColliders.Length>0)
        {
            //gameObject.transform.position = hitColliders[0].gameObject.transform.position;
            return hitColliders[0].gameObject;
        }
        else
        {
            return null;
        }

    }
    public bool OnFetchChecker()
    {
        if (OnFetch() != null) return true;
        else return false;
    }
    public void ToCheckPoint()
    {
        rb.drag = 0f;
        gameObject.transform.position = new Vector3(_checkPoint.x, _checkPoint.y, _checkPoint.z);
    }
    private void OnTriggerStay(Collider collision)
    {
        if (collision.gameObject == _water.gameObject)
        {
            _animator.SetBool("InWater",true);
            _animator.SetBool("InJump",false);
            rb.drag = 3f;
            rb.AddForce(transform.up*15f);
            
        }
    }
    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject == OnFetch())
        {
            if (_highFetch==null) {
                _highFetch = OnFetch();
                _scoreManager.IncrementScore();
            }
            else if (_highFetch.transform.position.y < OnFetch().transform.position.y)
            {
                _highFetch = OnFetch();
                _scoreManager.IncrementScore();
            }
        }
    }
}
