using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CardCollectionBarView : ICardCollectionBarView
{
    private readonly CollectionCardsSortButton sortButton;
    private readonly SortTypes sortTypes;

    public CollectionCardsSortButton SortButton => sortButton;

    public CardCollectionBarView(CollectionCardsSortButton sortButton, SortTypes sortTypes)
    {
        this.sortButton = sortButton;
        this.sortTypes = sortTypes;
    }

    public void SortArrayByLevel(ref CardData[] data)
    {
        int minIndex;
        CardData temp = null;

        for (int i = 0; i < data.Length; i++)
        {
            minIndex = i;
            for (int j = i + 1; j < data.Length; j++)
            {
                if (data[j].level < data[minIndex].level)
                    minIndex = j;
            }

            if (minIndex != i)
            {
                temp = data[i];
                data[i] = data[minIndex];
                data[minIndex] = temp;
            }
        }
        AnimateSortButton();
    }
    public void SortArrayByEnergyCost(ref CardData[] data)
    {
        int minIndex;
        CardData temp = null;

        for (int i = 0; i < data.Length; i++)
        {
            minIndex = i;
            for (int j = i + 1; j < data.Length; j++)
            {
                if (data[j].energy < data[minIndex].energy)
                    minIndex = j;
            }

            if (minIndex != i)
            {
                temp = data[i];
                data[i] = data[minIndex];
                data[minIndex] = temp;
            }
        }
        AnimateSortButton();
    }
    public void SortArrayByRarity(ref CardData[] data)
    {
        int minIndex;
        CardData temp = null;

        for (int i = 0; i < data.Length; i++)
        {
            minIndex = i;
            for (int j = i + 1; j < data.Length; j++)
            {
                if ((int)data[j].rarity < (int)data[minIndex].rarity)
                    minIndex = j;
            }

            if (minIndex != i)
            {
                temp = data[i];
                data[i] = data[minIndex];
                data[minIndex] = temp;
            }
        }
        AnimateSortButton();
    }

    void AnimateSortButton()
    {
        sortButton.transform.DOPunchScale(new Vector3(1.3f, 1.3f, 1.3f), .1f, 2, .5f);
    }

}
