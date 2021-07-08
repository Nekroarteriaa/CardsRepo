using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DraggingBehaviour : MonoBehaviour, IDraggingBehaviour
{
    [SerializeField]
    Canvas canvas;
    RectTransform rectTransform;
    CanvasGroup canvasGroup;
    Vector2 originalPosition;

    public RectTransform DraggedElementRectTransform => rectTransform;
    public CanvasGroup DraggedElementCanvasGroup => canvasGroup;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        originalPosition = rectTransform.anchoredPosition;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        SetDraggedElementToOriginalPosition();
    }

    public void SetDraggedElementToOriginalPosition()
    {
        rectTransform.anchoredPosition = originalPosition;
        canvasGroup.blocksRaycasts = true;
    }

}
