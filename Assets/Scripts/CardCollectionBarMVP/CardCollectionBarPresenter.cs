﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardCollectionBarPresenter : ICardCollectionBarPresenter<SortTypes>
{
    public ICardCollectionBarView CardCollectionBarView { get; }
    public CardCollectionBarPresenter(ICardCollectionBarView cardCollectionBarView)
    {
        CardCollectionBarView = cardCollectionBarView;
    }


    public void Present(ref CardData[] cardsData, SortTypes sortTypes)
    {
        SortCardCollection(ref cardsData, sortTypes);
    }

    void SortCardCollection(ref CardData[] cardsData, SortTypes sortTypes)
    {
        switch (sortTypes)
        {
            case SortTypes.LevelType:
                CardCollectionBarView.SortArrayByLevel(ref cardsData);
                break;
            case SortTypes.EnergyCost:
                CardCollectionBarView.SortArrayByEnergyCost(ref cardsData);
                break;
            case SortTypes.RarityType:
                CardCollectionBarView.SortArrayByRarity(ref cardsData);
                    break;
            default:
                break;
        }
        CardCollectionBarView.SortButton.SwitchingTypesOfSortsText(sortTypes);
    }

    public void Present(SortTypes args)
    {
       
    }
}
