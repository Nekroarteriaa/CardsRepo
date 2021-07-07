using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class DeckButtonWidget : MonoBehaviour, IDeckButton<uint>
{
    [SerializeField]
    Image activeSprite;
    [SerializeField]
    TMPro.TextMeshProUGUI deckNumber;
    Button showDeckButton;

    uint buttonIndex;

    public event Action<uint> onElementClicked;

    #region UnityMethods
    private void Awake()
    {
        buttonIndex = Convert.ToUInt32(transform.GetSiblingIndex());
        deckNumber.text = (buttonIndex + 1).ToString();
        showDeckButton = GetComponent<Button>();
        DeckState(false);
    }

    void OnEnable()
    {
        showDeckButton.onClick.AddListener(() => { OnButtonDeckPressed(buttonIndex); });
    }

    private void OnDisable()
    {
        showDeckButton.onClick.RemoveAllListeners();
    }
    #endregion

    public void DeckState(bool state)
    {
        activeSprite.enabled = state;
    }
    void OnButtonDeckPressed(uint buttonIndex)
    {
        onElementClicked?.Invoke(buttonIndex);
    }
}
