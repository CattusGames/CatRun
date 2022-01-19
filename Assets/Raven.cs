using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class Raven : MonoBehaviour
{

    [HideInInspector]public GameObject _player;
    [HideInInspector] public float _speed;

    private Vector3 _attackPos;
    private Vector3 _playerPos;
    private bool _inPoint;
    private bool _touch;
    private bool _particle;
    [SerializeField] private GameObject _deadParticle;
    [SerializeField] private GameObject _body;
    private float _waitTimer = 0f;

    // Start is called before the first frame update
    private void Awake()
    {
        _particle = false;
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
        Vector3 new_rotation = _playerPos - transform.position;
        transform.right = new_rotation;

        ToAttackPosition();
    }
    private void ToAttackPosition()
    {
       

        if(_inPoint==false)
        {
            _speed = 2f;
            gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, _attackPos, _speed * Time.deltaTime);
            if (gameObject.transform.position == _attackPos)
            {
                if (_touch==true)
                {
                    GoAhead();
                }
                else
                {
                    _inPoint = true;
                }
            }
            
        }
        else if (_inPoint == true)
        {
            Attack();
        }
    }
    public void Attack()
    {
        gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, _playerPos, _speed * Time.deltaTime);

    }
    public void GoAhead()
    {
        _body.SetActive(false);
        if (_particle == true)
        {
            Instantiate(_deadParticle, gameObject.transform.position, Quaternion.identity);
            _particle = false;
        }

        _waitTimer += Time.deltaTime;
        if (_waitTimer >= 4f)
        {
            Destroy(gameObject);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            _inPoint = false;
            _touch = true;
        }
    }
    private void OnMouseDown()
    {
        _particle = true;
        gameObject.GetComponent<BoxCollider>().enabled = false;
        GoAhead();
    }
}
