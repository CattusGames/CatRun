using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class CylinderController : MonoBehaviour
{
    private Transform _cylinderTransform;
    [SerializeField] private List<GameObject> _fetchList;
    [SerializeField] private PlayerController _player;
    [SerializeField] private CameraController _mainCamera;


    // Start is called before the first frame update
    void Start()
    {
        _cylinderTransform = gameObject.transform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CameraRotate(GameObject onFetch)
    {
        if (onFetch==_fetchList.First())
        {
                 
            _player.transform.rotation = onFetch.transform.rotation;
        }

        else if(onFetch != _fetchList.First())
        {
            Debug.Log("Ne rovno First");
            for (int i=0;i<_fetchList.Count;i++)
            {
                if (_fetchList.ElementAt(i)==onFetch)
                {
                    _player.transform.rotation = onFetch.transform.rotation;
                }
            }
        }
    }

}
