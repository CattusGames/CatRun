using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class Raven : MonoBehaviour
{
    private Vector3 _attackPos;
    private GameObject _player;
    private Vector3 _playerPos;
    public bool _inPoint;
    // Start is called before the first frame update
    private void Awake()
    {

        _inPoint = false;
        _player = GameObject.FindWithTag("Player");
    }
    void Start()
    {
        _attackPos = new Vector3(gameObject.transform.position.x, transform.position.y + 1f, transform.position.z);

    }

    // Update is called once per frame
    void Update()
    {
        _playerPos = _player.transform.position;
        ToAttackPosition();
    }
    private void ToAttackPosition()
    {
       

        if(_inPoint==false)
        {
            
       gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, _attackPos, 2f * Time.deltaTime);
            if (gameObject.transform.position == _attackPos)
            {
                _inPoint = true;
            }
            
        }
        else if (_inPoint == true)
        {
            Debug.Log("IN_POINT");
            Attack();
        }
    }
    public void Attack()
    {
        gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, _playerPos, 4f * Time.deltaTime);

    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            _inPoint = false;
        }
    }
    private void OnMouseDown()
    {
        gameObject.SetActive(false);
    }
}
