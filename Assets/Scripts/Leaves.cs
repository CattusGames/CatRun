using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Leaves : MonoBehaviour
{
    [SerializeField]private GameObject _raven;
    [SerializeField] private Transform _spawnPoint;

    float timerForDoubleClick = 0.0f;
    float delay = 0.3f;
    bool isDoubleClick = false;
    // Start is called before the first frame update
    private void Start()
    {
        gameObject.SetActive(true);
    }

    void Update()
    {
        if (isDoubleClick == true)
        {

            timerForDoubleClick += Time.deltaTime;
        }


        if (timerForDoubleClick >= delay)
        {
            timerForDoubleClick = 0.0f;
            isDoubleClick = false;
        }

    }


    void OnMouseOver()
    {
        if (Input.GetButtonDown("Fire1") && isDoubleClick == false)
        {
            Debug.Log("Mouse clicked once");
            isDoubleClick = true;
        }
    }

    void OnMouseDown()
    {
        if (isDoubleClick == true && timerForDoubleClick < delay)
        {
            gameObject.GetComponent<MeshRenderer>().enabled = false;
            gameObject.GetComponent<MeshCollider>().enabled = false;
            var raven = Instantiate(_raven);
            raven.gameObject.transform.position = _spawnPoint.position;
        }
    }
}
