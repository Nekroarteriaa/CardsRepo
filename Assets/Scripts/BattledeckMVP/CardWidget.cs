using UnityEngine.UI;
using UnityEngine;
using System;

// Extend as requiered
public class CardWidget : MonoBehaviour, ICardButton<CardWidget>, IEditModeElement
{
    public event Action<CardWidget> onElementClicked;
    public event Action<CardWidget> onInfoButtonClicked;
    public event Action<CardWidget> onSelectButtonClicked;

    bool isDeckCard;
    public bool IsDeckCard => isDeckCard;

    CardData selfData;
    public CardData CardWidgetData => selfData;

    CardWidget selectedCardCollectionElement;
    public CardWidget SelectedCardCollectionElement => selectedCardCollectionElement;

    [SerializeField]
    Image cardPicture;
    [SerializeField]
    Image cardFrame;
    [SerializeField]
    Image cardMask;
    [SerializeField]
    TMPro.TMP_Text level;
    [SerializeField]
    Button cardButton;
    [SerializeField]
    TMPro.TMP_Text energy;

    [SerializeField]
    Canvas subMenuCanvas;
    [SerializeField]
    Image subMenuBackground;
    [SerializeField]
    Button infoButton;
    [SerializeField]
    Button selectButton;

    private void Awake()
    {
        HideClickSubMenu();
        selectedCardCollectionElement = this;
    }

    private void OnEnable()
    {
        cardButton.onClick.AddListener(() => { OnCardClicked(this); });
        selectButton.onClick.AddListener(() => { OnSelectedButtonClicked(this); });
        infoButton.onClick.AddListener(()=> { OnInfoButtonClicked(this); });
    }

    private void OnDisable()
    {
        cardButton.onClick.RemoveAllListeners();
        selectButton.onClick.RemoveAllListeners();
        infoButton.onClick.RemoveAllListeners();
    }

    public void ShowClickSubMenu()
    {
        subMenuBackground.gameObject.SetActive(true);
        subMenuCanvas.enabled = true;
        if (IsDeckCard)
            selectButton.gameObject.SetActive(false);
        else
            selectButton.gameObject.SetActive(true);
    }
    public void HideClickSubMenu()
    {
        subMenuBackground.gameObject.SetActive(false);
        subMenuCanvas.enabled = false;
        selectButton.gameObject.SetActive(false);
    }

    public void SetCardData(CardData cardData, bool isADeckCard)
    {
        this.isDeckCard = isADeckCard;
        SetCardData(cardData);
    }
    void SetCardData (CardData cardData)
    {
        selfData = cardData;
        level.text  = string.Format("Level {0:N0}", cardData.level);
        energy.text = cardData.energy.ToString("N0");
        cardPicture.sprite = cardData.picture;
        cardMask.sprite = cardData.mask;
        cardFrame.sprite = cardData.frame;
        
    }

    

    void OnCardClicked(CardWidget cardWidget)
    {
        onElementClicked?.Invoke(cardWidget);
    }

    void OnInfoButtonClicked(CardWidget cardWidget)
    {
        onInfoButtonClicked?.Invoke(cardWidget);
    }

    void OnSelectedButtonClicked(CardWidget cardWidget)
    {
        onSelectButtonClicked?.Invoke(cardWidget);
    }

    public void SetSelectedElementTransform(CardWidget selectedCardCollectionElement)
    {
        this.selectedCardCollectionElement = selectedCardCollectionElement;
    }
}