using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    private ScoreManager _scoreManager;
    [SerializeField] private Particle _coinParticle;
    [SerializeField] private MeshRenderer _mesh;
    bool increment=true;
    private void Start()
    {
        _scoreManager = FindObjectOfType<ScoreManager>();

    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            _coinParticle.PlayAnimation();
            _scoreManager.IncrementCoin();
            Destroy(gameObject);

        }
    }
    private void OnMouseDown()
    {
        if (increment)
        {
            StartCoroutine(ParticleAnimation());
        }

    }
    private IEnumerator ParticleAnimation()
    {
        increment = false;
        _mesh.enabled = false;
        _scoreManager.IncrementCoin();
        _coinParticle.PlayAnimation();
        yield return new WaitForSeconds(1f);
        
        Destroy(gameObject);
    }
}
