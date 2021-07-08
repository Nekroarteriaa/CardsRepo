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

    private void Awake()
    {
        selfButton = GetComponent<Button>();
        selfImage = GetComponent<Image>();
        originalSprite = selfImage.sprite;
    }

    private void OnEnable()
    {
        selfButton.onClick.AddListener(()=>{
            OnSortButtonClicked(counter);
            if (counter < 2)
                counter++;
            else
             counter = 0;

            SwitchingTypesOfSortsText(counter);
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

    void SwitchingTypesOfSortsText(uint counter)
    {
        switch (counter)
        {
            case 0:
                selfImage.sprite = originalSprite;
                buttonText.text = "Level";
                break;
            case 1:
                buttonText.text = "Energy";
                break;
            case 2:
                selfImage.sprite = typeSortButtonSprite;
                buttonText.text = "Rarity";
                break;
            default:
                break;
        }
        
    }
}
