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
        var draggedElement = eventData.pointerDrag.GetComponent<DraggingBehaviour>() ? eventData.pointerDrag.GetComponent<IDraggingBehaviour>() : null;
        if (draggedElement == null)
            return;

        OnCardDropped(selfCard);
        draggedElement.SetDraggedElementToOriginalPosition();
        
    }

    void OnCardDropped(CardWidget cardWidget)
    {
        onCardDropped?.Invoke(cardWidget);
    }
}
