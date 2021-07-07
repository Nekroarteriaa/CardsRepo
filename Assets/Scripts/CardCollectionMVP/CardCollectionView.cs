using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardCollectionView : ICardCollectionView
{
    public List<CardWidget> GetCardCollection => cardWidgets;
    List<CardWidget> cardWidgets;
    

    private readonly CardWidget cardPrefab;
    private readonly Transform cardCollectionVerticalContainer;
    private readonly Transform cardCollectionRowsContainer;
    private readonly int cardCollectionRowsContentCount;
    

   

    public CardCollectionView(CardWidget cardPrefab, Transform cardCollectionVerticalContainer, Transform cardCollectionRowsContainer, int cardCollectionRowsContentCount)
    {
        this.cardPrefab = cardPrefab;
        this.cardCollectionVerticalContainer = cardCollectionVerticalContainer;
        this.cardCollectionRowsContainer = cardCollectionRowsContainer;
        this.cardCollectionRowsContentCount = cardCollectionRowsContentCount;
        cardWidgets = new List<CardWidget>();
    }


    public void CreateCardCollection(CardData[] cardsData)
    {
        if (cardWidgets.Count != 0)
            return;

        var cardsDataLength = cardsData.Length;
        var totalRowsContent = cardCollectionRowsContentCount;
        var totalColumns = cardsDataLength / cardCollectionRowsContentCount;

        var indexCollectionCard = 0;
        var security = 0;

        for (int i = 0; i < totalColumns + 1; i++)
        {
            var column = GameObject.Instantiate(cardCollectionRowsContainer, cardCollectionVerticalContainer);
            security = ((i + 1) * totalRowsContent);
            
            if(security > cardsDataLength)
            {
                security = security - cardsDataLength;
                totalRowsContent -= security;
            }

            for (int j = 0; j < totalRowsContent; j++)
            {
                var cardWidget = GameObject.Instantiate(cardPrefab, column.transform);
                cardWidget.SetCardData(cardsData[indexCollectionCard], false);
                cardWidget.gameObject.SetActive(true);
                cardWidgets.Add(cardWidget);
                indexCollectionCard++;
            }

        }

       // ReloadGrid();
    }



    void ReloadGrid()
    {
        cardCollectionVerticalContainer.gameObject.SetActive(false);
        cardCollectionVerticalContainer.gameObject.SetActive(true);
    }
}