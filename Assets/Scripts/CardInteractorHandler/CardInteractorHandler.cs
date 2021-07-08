using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;
using System;

public class CardInteractorHandler : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{

    public event Action onClickDown;
    public event Action onClickUp;
    public void OnPointerDown(PointerEventData eventData)
    {
        onClickDown?.Invoke();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        onClickUp?.Invoke();
    }
}
