using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    private ScoreManager _scoreManager;
    private void Start()
    {
        _scoreManager = FindObjectOfType<ScoreManager>();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag=="Player")
        {
            _scoreManager.IncrementCoin();
            Destroy(gameObject);
        }
    }
    private void OnMouseDown()
    {
        _scoreManager.IncrementCoin();
        Destroy(gameObject);
    }
}
