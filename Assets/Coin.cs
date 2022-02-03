using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag=="Player")
        {
            PlayerPrefs.SetInt("Coin",PlayerPrefs.GetInt("Coin")+1);
            Destroy(gameObject);
        }
    }
    private void OnMouseDown()
    {
        PlayerPrefs.SetInt("Coin", PlayerPrefs.GetInt("Coin") + 1);
        Destroy(gameObject);
    }
}
