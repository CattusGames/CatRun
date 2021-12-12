using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Leaves : MonoBehaviour
{
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
        gameObject.SetActive(false);
    }
}
