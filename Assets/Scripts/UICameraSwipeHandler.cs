using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UICameraSwipeHandler : MonoBehaviour, IBeginDragHandler, IDragHandler
{
    [SerializeField] CameraController _mainCamera;
    [SerializeField] GameObject _swipe;
    private void Awake()
    {
        if (PlayerPrefs.GetInt("SwipeTutorial",0)==1)
        {
            _swipe.SetActive(false);
        } 
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        PlayerPrefs.SetInt("SwipeTutorial",1);
        Vector2 delta = eventData.delta;

        if (delta.x > 0)
            _mainCamera.RightDrag();
        else _mainCamera.LeftDrag();

    }

    public void OnDrag(PointerEventData eventData)
    {

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
