using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DroppingBehaviour : MonoBehaviour, IDroppingBehaviour<CardWidget>
{
    public event Action<CardWidget> onCardDropped;
    CardWidget selfCard;
    private void Awake()
    {
        selfCard = GetComponent<CardWidget>();
    }

    public void OnDrop(PointerEventData eventData)
    {
        OnCardDropped(selfCard);
        eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = GetComponent<RectTransform>().anchoredPosition;
        eventData.pointerDrag.GetComponent<CanvasGroup>().blocksRaycasts = true;
    }

    void OnCardDropped(CardWidget cardWidget)
    {
        onCardDropped?.Invoke(cardWidget);
    }
}
