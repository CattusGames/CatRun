using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : MonoBehaviour
{
    private GameObject _player;
    [HideInInspector]public bool _isMove;
    [SerializeField] private GameManager _gameManager;
    // Start is called before the first frame update

    void Start()
    {
        _player = GameObject.FindWithTag("Player");
        _isMove = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (_gameManager._start == true)
        {
            if (_isMove)
            {
                gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, _player.transform.position, 0.2f * Time.deltaTime);

            }
            else
            {
                gameObject.transform.position = gameObject.transform.position;
            }
        }
        
    }
    public void DecrementPosition()
    {
        gameObject.transform.position = new Vector3(gameObject.transform.position.x,gameObject.transform.position.y - 2,gameObject.transform.position.y);
    }
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject == _player)
        {
            _isMove = false;
        }
    }
}
