using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Leaves : MonoBehaviour
{
    [SerializeField]private GameObject _raven;
    [SerializeField] private Transform _spawnPoint;
    // Start is called before the first frame update
    private void Start()
    {
        gameObject.SetActive(true);
    }

    // Update is called once per frame
    private void Update()
    {
        
    }
    private void OnMouseDown()
    {
        gameObject.GetComponent<MeshRenderer>().enabled = false;
        gameObject.GetComponent<MeshCollider>().enabled = false;
        var raven = Instantiate(_raven);
        raven.gameObject.transform.position = _spawnPoint.position;


    }
}
