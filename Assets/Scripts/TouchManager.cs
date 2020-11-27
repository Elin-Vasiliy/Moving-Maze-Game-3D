using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class TouchManager : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private float _clickTime;
    private float _timeRefreshDoubleTap = 5f;

    public event UnityAction DoubleTap;
    [HideInInspector] public bool isRight = false;
    [HideInInspector] public bool isLeft = false;

    private void Update()
    {
        _timeRefreshDoubleTap += Time.deltaTime;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (_clickTime > 0)
        {
            if ((Time.realtimeSinceStartup - _clickTime) < 0.3f)
            {
                if (_timeRefreshDoubleTap > 5)
                {
                    DoubleTap?.Invoke();
                    _timeRefreshDoubleTap = 0;
                }
            }
            else
            {
                if (eventData.pressPosition.x > (Camera.main.pixelWidth / 2))
                {
                    isRight = true;
                }
                else if (eventData.pressPosition.x < (Camera.main.pixelWidth / 2))
                {
                    isLeft = true;
                }
            }
        }

        _clickTime = Time.realtimeSinceStartup;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isRight = false;
        isLeft = false;
    }
}