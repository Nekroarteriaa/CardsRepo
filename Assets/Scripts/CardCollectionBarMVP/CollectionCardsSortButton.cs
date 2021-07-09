using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class CollectionCardsSortButton : MonoBehaviour , IElementClicked<uint>
{
    [SerializeField]
    Sprite typeSortButtonSprite;
    [SerializeField]
    TextMeshProUGUI buttonText;
    Button selfButton;
    Image selfImage;
    Sprite originalSprite;

    uint counter = 0;

    public event Action<uint> onElementClicked;

    const string LEVEL_TEXT = "Level";
    const string ENERGY_TEXT = "Energy";
    const string RARITY_TEXT = "Rarity";

   

    private void Awake()
    {
        selfButton = GetComponent<Button>();
        selfImage = GetComponent<Image>();
        originalSprite = selfImage.sprite;
    }

    private void OnEnable()
    {
        selfButton.onClick.AddListener(()=>{

            if (counter < 2)
                counter++;
            else
             counter = 0;
            OnSortButtonClicked(counter);
        });
    }

    void OnDisable()
    {
        selfButton.onClick.RemoveAllListeners();
    }

    void OnSortButtonClicked(uint counter)
    {
        onElementClicked?.Invoke(counter);
    }

    public void SwitchingTypesOfSortsText(SortTypes sortTypes)
    {
        switch (sortTypes)
        {
            case SortTypes.LevelType:
                selfImage.sprite = originalSprite;
                buttonText.text = LEVEL_TEXT;
                break;
            case SortTypes.EnergyCost:
                buttonText.text = ENERGY_TEXT;
                break;
            case SortTypes.RarityType:
                selfImage.sprite = typeSortButtonSprite;
                buttonText.text = RARITY_TEXT;
                break;
            default:
                break;
        }
    }
}

[System.Serializable]
public enum SortTypes
{
    LevelType,
    EnergyCost,
    RarityType
}
