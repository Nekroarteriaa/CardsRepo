using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class EditModeView : IEditModeView
{
    bool isInEditMode;
    public bool IsInEditMode => isInEditMode;
    private CardWidget editModeCard;
    public CardWidget EditModeCard => editModeCard;

    private readonly Transform cardCollectionVerticalContainer;
    private readonly Transform editModeCardContainer;
    VerticalLayoutGroup verticalLayout;
    List<HorizontalLayoutGroup> horizontalContainers;
    Vector3 editModeCardOriginalPosition;

    int leftPadding, rightPadding, topPadding, bottomPadding;
    float spacingValue;


    public EditModeView(Transform cardCollectionVerticalContainer, Transform editModeCardContainer, CardWidget editModeCard)
    {
        this.cardCollectionVerticalContainer = cardCollectionVerticalContainer;
        this.editModeCardContainer = editModeCardContainer;
        this.editModeCard = editModeCard;
        editModeCardOriginalPosition = editModeCard.transform.localPosition;


        verticalLayout = cardCollectionVerticalContainer.GetComponent<VerticalLayoutGroup>();
        leftPadding = verticalLayout.padding.left;
        rightPadding = verticalLayout.padding.right;
        topPadding = verticalLayout.padding.top;
        bottomPadding = verticalLayout.padding.bottom;
        spacingValue = verticalLayout.spacing;
    }

   

    public void SetUpForEditMode()
    {
        isInEditMode = true;

        if(horizontalContainers == null)
           horizontalContainers = new List<HorizontalLayoutGroup>(cardCollectionVerticalContainer.GetComponentsInChildren<HorizontalLayoutGroup>());

        foreach (var item in horizontalContainers)
        {
            item.gameObject.SetActive(false);
        }

        verticalLayout.padding.left = 0;
        verticalLayout.padding.right = 0;
        verticalLayout.padding.top = 0;
        verticalLayout.padding.bottom = 0;
        verticalLayout.spacing = 0;

        editModeCardContainer.gameObject.SetActive(true);
    }

    public void SetEditModeCardData(CardWidget selectedCard)
    {
        editModeCard.SetCardData(selectedCard.CardWidgetData, false);
        editModeCard.SetSelectedElementTransform(selectedCard);
        editModeCard.transform.localPosition = selectedCard.transform.localPosition;
        editModeCard.transform.DOLocalMove(editModeCardOriginalPosition, 1f);
    }

    public void SetUpForNormalMode()
    {
        isInEditMode = false;

        foreach (var item in horizontalContainers)
        {
            item.gameObject.SetActive(true);
        }

        verticalLayout.padding.left = leftPadding;
        verticalLayout.padding.right = rightPadding;
        verticalLayout.padding.top = topPadding;
        verticalLayout.padding.bottom = bottomPadding;
        verticalLayout.spacing = spacingValue;

        editModeCardContainer.gameObject.SetActive(false); ;
    }

}
