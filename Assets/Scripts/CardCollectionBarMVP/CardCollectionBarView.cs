using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CardCollectionBarView : ICardCollectionBarView
{
    private readonly CollectionCardsSortButton sortButton;
    private readonly TextMeshProUGUI collectedCardsText;

    public CardCollectionBarView(CollectionCardsSortButton sortButton, TextMeshProUGUI collectedCardsText)
    {
        this.sortButton = sortButton;
        this.collectedCardsText = collectedCardsText;
    }

    public void SortCardCollection()
    {
        throw new System.NotImplementedException();
    }
}
