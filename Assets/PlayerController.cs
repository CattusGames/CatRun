using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    private Rigidbody rb;
    private float _jumpMagnitude;
    private Vector3 _jumpDirection;
    public TrajectoryRenderer Trajectory;
    public LayerMask _whatIsFetch;
    [SerializeField]private Vector3 _mouseDownPos;
    [SerializeField]private Vector3 _mouseUpPos;
    public CylinderController _cylinder;
    public bool _rotate;
    [SerializeField] private Transform _mainCamera;
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        _rotate = false;
    }

    // Update is called once per frame
    void Update()
    {

        
    }
    private void OnMouseDown()
    {
        var rotation = _mainCamera.transform.rotation.eulerAngles.y;
        if (rotation==0f)
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

    private void OnMouseDrag()
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
        _jumpDirection = _mouseUpPos-_mouseDownPos;
        _jumpMagnitude = _jumpDirection.magnitude / 100;
        Vector3 speed = _jumpDirection * _jumpMagnitude / 50;
        Trajectory.ShowTrajectory(gameObject.transform.localPosition, speed);
        Debug.Log(_jumpDirection);


    }

    private void OnMouseUp()
    {
        rb.AddForce(_jumpDirection * _jumpMagnitude, ForceMode.Acceleration);

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
    private void ChangeControlAxis()
    {
        switch (_mainCamera.transform.rotation.y)
        {
            case 0: break;
            case 180: break;
            case 90: break;
            case -90: break;
            default:
                break;
        }
    }
}
