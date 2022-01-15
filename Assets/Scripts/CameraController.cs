using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{
    public bool _getDown = false;
    [SerializeField] private Transform _player;

    private void Start()
    {

    }

    private void Update()
    {

    }
    public void RightDrag()
    {
        transform.RotateAround(_player.transform.position, Vector3.up, 90);
        _getDown = false;
       
    }

    public void LeftDrag()
    {
        transform.RotateAround(_player.transform.position, Vector3.up, -90);
        _getDown = false;

    }

}


