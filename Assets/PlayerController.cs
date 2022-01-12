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
    public TrajectoryRenderer Trajectory;
    public LayerMask _whatIsFetch;
    [HideInInspector] public Vector3 _checkPoint;
    public bool _rotate;



    [SerializeField] private Vector3 _mouseDownPos;
    [SerializeField] private Vector3 _mouseUpPos;
    [SerializeField] private Transform _mainCamera;
    [SerializeField] private Transform _water;
    [SerializeField] private float _timeLeft;
    [SerializeField] private float _time;
    private int _score;
    [SerializeField] Text _scoreText;
    [SerializeField] Text _highScoreText;
    private Animator _animator;
    [SerializeField]private GameObject _body;

    void Start()
    {
        
        _animator = gameObject.GetComponentInChildren<Animator>();
        _timeLeft = _time;
        rb = gameObject.GetComponent<Rigidbody>();
        _rotate = false;
        _highScoreText.text = "HS: " + PlayerPrefs.GetInt("Score").ToString();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.LogError("SPEED :"+rb.velocity.magnitude);
        _scoreText.text = _score.ToString();
        var recentScore = (int)gameObject.transform.position.y;
        if (_score<recentScore)
        {
            _score = recentScore;
        }

    }
    private void OnMouseDown()
    {
        if (OnFetchChecker()==true)
        {
            _checkPoint = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z);
            _animator.SetBool("IsJump",true);
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
        if (OnFetchChecker()==true)
        {
            float rotation = _mainCamera.transform.rotation.eulerAngles.y;
            if (rotation == 0f)
            {
                _mouseUpPos = Input.mousePosition;
                JumpRotate(_mouseDownPos,_mouseUpPos,_body);
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
            _jumpDirection = (_mouseDownPos - _mouseUpPos) / 3;
            _jumpMagnitude = _jumpDirection.magnitude / 100;
            Vector3 speed = _jumpDirection * _jumpMagnitude / 50;
            Trajectory.ShowTrajectory(gameObject.transform.localPosition, speed);
            Debug.Log(_jumpDirection);

        }


    }

    private void OnMouseUp()
    {
        if (OnFetchChecker()==true)
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
            if (Input.GetKey("W")==true)
            {
                if (PlayerPrefs.GetInt("Score") < _score)
                {
                    PlayerPrefs.SetInt("Score", _score);
                }
                SceneManager.LoadScene(0);
            }
           
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag=="Raven")
        {
            foreach (ContactPoint contact in collision.contacts)
            {
                if (contact.thisCollider.tag =="Raven")
                {
                    Debug.Log("IMPULSE");
                    rb.AddForce(contact.thisCollider.transform.position * 5f, ForceMode.Impulse);
                }
            }
        }
    }
    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject == OnFetch())
        {
            if (_animator.GetBool("InWater") == true)
            {
                Physics.IgnoreCollision(OnFetch().GetComponent<Collider>(), GetComponent<Collider>());
            }
        }
    }
}
