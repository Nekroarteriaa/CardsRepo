using UnityEngine;
using TMPro;
using DG.Tweening;

public class CardCollectionBarView : ICardCollectionBarView
{
    private readonly CollectionCardsSortButton sortButton;
    private readonly SortTypes sortTypes;
    private readonly TextMeshProUGUI indexCollectionBarText;

    public CardCollectionBarView(CollectionCardsSortButton sortButton, SortTypes sortTypes, TextMeshProUGUI indexCollectionBarText)
    {
        this.sortButton = sortButton;
        this.sortTypes = sortTypes;
        this.indexCollectionBarText = indexCollectionBarText;
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
        
        indexCollectionBarText.text = string.Format("{0}/53", data.Length);
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
    }

    public void AnimateSortButton()
    {
        sortButton.transform.DOScale(new Vector3(1.2f, 1.2f, 1.2f), .1f).OnComplete(() => {
            sortButton.transform.DOScale(new Vector3(1f, 1f, 1f), .1f);
        });
    }

    public void ChangeSortButtonTextAndAppearance(SortTypes sortTypes)
    {
        sortButton.ApplyCustomChangesInSortButton(sortTypes);
    }

    public void HideCardCollectionBar()
    {
        sortButton.transform.parent.gameObject.SetActive(false);
    }

    public void ShowCardCollectionBar()
    {
        sortButton.transform.parent.gameObject.SetActive(true);
    }
}
