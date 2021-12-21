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
    [SerializeField] private Vector3 _mouseDownPos;
    [SerializeField] private Vector3 _mouseUpPos;
    public bool _rotate;
    [SerializeField] private Transform _mainCamera;
    [SerializeField] private Transform _water;
    [SerializeField] private float _timeLeft;
    [SerializeField] private float _time;
    PlayerPrefs _record;
    private int _score;
    [SerializeField] Text _scoreText;
    [SerializeField] Text _highScoreText;
    void Start()
    {
        _timeLeft = _time;
        rb = gameObject.GetComponent<Rigidbody>();
        _rotate = false;
        _highScoreText.text = "HS: " + PlayerPrefs.GetInt("Score").ToString();
    }

    // Update is called once per frame
    void Update()
    {
        _scoreText.text = _score.ToString();
        var recentScore = (int)gameObject.transform.position.y;
        if (_score<recentScore)
        {
            _score = recentScore;
        }
        _water.position = Vector3.MoveTowards(_water.position, gameObject.transform.position, 0.2f * Time.deltaTime);

    }
    private void OnMouseDown()
    {
        if (OnFetch() != null)
        {
            var rotation = _mainCamera.transform.rotation.eulerAngles.y;
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
        if (OnFetch() != null)
        {
            var rotation = _mainCamera.transform.rotation.eulerAngles.y;
            if (rotation == 0f)
            {
                _mouseUpPos = Input.mousePosition;
            }
            else if (rotation == 180f)
            {
                _mouseUpPos = new Vector3(-Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z);
            }
            else if (rotation == 90f)
            {
                _mouseUpPos = new Vector3(Input.mousePosition.z, Input.mousePosition.y, -Input.mousePosition.x);
            }
            else if (rotation == 270f)
            {
                _mouseUpPos = new Vector3(Input.mousePosition.z, Input.mousePosition.y, Input.mousePosition.x);
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
        if (OnFetch() != null)
        {
            rb.AddForce(_jumpDirection * _jumpMagnitude, ForceMode.Acceleration);
        }
    }
    public GameObject OnFetch()
    {
        Collider[] hitColliders = Physics.OverlapBox(gameObject.transform.position, transform.localScale, Quaternion.identity, _whatIsFetch);
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
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject == _water.gameObject)
        {

            if (PlayerPrefs.GetInt("Score")<_score)
            {
                PlayerPrefs.SetInt("Score", _score);
            }
            SceneManager.LoadScene(0);
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


        if (collision.gameObject == OnFetch())
        {

            _timeLeft -= Time.deltaTime;
            if (_timeLeft == 0)
            {
                _water.position = gameObject.transform.position - new Vector3(0, 5, 0);
            }
            if (_timeLeft < -0.5)
            {
                _water.position = Vector3.MoveTowards(_water.position,gameObject.transform.position,2f*Time.deltaTime);
            }
        }
        else
        {
            _water.position = Vector3.MoveTowards(_water.position, gameObject.transform.position, 0f * Time.deltaTime);
        }

    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject==OnFetch())
        {
            _timeLeft = _time;
        }
    }
    private IEnumerator WaterComing(float speed)
    {
        
        yield return new WaitForSeconds(1f);
        
        
    }
}
