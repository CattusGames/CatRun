using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Leaves : MonoBehaviour
{
    [SerializeField]private GameObject _raven;
    [SerializeField] private Transform _spawnPoint;

    int TapCount;
    [Range(0, 0.5f)] public float MaxDubbleTapTime;
    float NewTime;
    // Start is called before the first frame update
    private void Start()
    {
        gameObject.SetActive(true);
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Ended)
            {
                TapCount += 1;
            }

            if (TapCount == 1)
            {

                NewTime = Time.time + MaxDubbleTapTime;
            }
            else if (TapCount == 2 && Time.time <= NewTime)
            {

                gameObject.GetComponent<MeshRenderer>().enabled = false;
                gameObject.GetComponent<MeshCollider>().enabled = false;
                var raven = Instantiate(_raven);
                raven.gameObject.transform.position = _spawnPoint.position;


                TapCount = 0;
            }

        }
        if (Time.time > NewTime)
        {
            TapCount = 0;
        }
    }
}
