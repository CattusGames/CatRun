using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{
    private Vector3 _mouseDownPos;
    private bool _getDown = false;
    [SerializeField] private Transform _player;

    private void Start()
    {

    }

    private void Update()
    {
        Debug.Log("Rotation" + _player.transform.position.y);
        if (Input.GetMouseButtonDown(0) && _getDown == false && Input.mousePosition.y > 1500)
        {
            Debug.Log("MOUSEDOWN");
            _mouseDownPos = Input.mousePosition;
            _getDown = true;
        }
        if (Input.GetMouseButtonUp(0) && Input.mousePosition.y > 1500)
        {
            var swipeTrigger = Mathf.Abs(Input.mousePosition.x - _mouseDownPos.x);
            Debug.Log("MOUSEUP");
            if (Input.mousePosition.x > _mouseDownPos.x && swipeTrigger>100)
            {
                Debug.Log("");
                RightDrag();

            }
            if (Input.mousePosition.x < _mouseDownPos.x && swipeTrigger > 100)
            {
                LeftDrag();

            }
        }
    }
    private void RightDrag()
    {
        transform.RotateAround(_player.transform.position, Vector3.up, 90);
        Debug.Log("RDrag" + _player.transform.position.y);
        _getDown = false;
       
    }

    private void LeftDrag()
    {
        transform.RotateAround(_player.transform.position, Vector3.up, -90);
        Debug.Log("LDrag" + _player.transform.position.y);
        _getDown = false;

    }

}


