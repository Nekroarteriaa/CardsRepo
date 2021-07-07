using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardCollectionPresenter : ICardCollectionPresenter<CardData[]>
{
    private readonly ICardCollectionView view;

    public CardCollectionPresenter(ICardCollectionView view)
    {
        this.view = view;
    }

    public void Present(CardData[] cardsData)
    {
        PresentCardCollection(cardsData);
    }
    

    public void PresentCardCollection(CardData[] cardsData)
    {
        view.CreateCardCollection(cardsData);
    }
}
