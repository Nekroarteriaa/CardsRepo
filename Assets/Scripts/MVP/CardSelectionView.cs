using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CardSelectionView : MonoBehaviour, ICardSelectionView
{
    #region BattleDeck
    [SerializeField]
    Transform deck;
    CardWidget[] deckCards;
    public IBattleDeckView DeckView { get; set; }
    #endregion

    #region BarButtons
    [SerializeField]
    GameObject deckBarButtonsContainer;
    DeckButtonWidget[] deckButtonWidgets;
    public IDeckBarView<uint> BarView { get; set; }

    public event Action<uint> onDeckBarButtonClicked;
    #endregion

    #region CollectionCards
    [SerializeField]
    CardWidget cardPrefab;
    [SerializeField]
    Transform cardCollectionColumnContainer;
    [SerializeField]
    Transform cardCollectionRowsContainer;
    [SerializeField]
    int cardCollectionRowsContentCount;
    public ICardCollectionView CollectionView { get; set; }

    public event Action<CardWidget> onCardClicked;
    public event Action<CardWidget> onSelectedButtonClicked;
    public event Action onExitEditModeButtonClicked;
    #endregion

    #region EditMode
    [SerializeField]
    Transform editModeCardContainer;
    [SerializeField]
    CardWidget editModeCard;
    [SerializeField]
    Button exitEditModeButton;
    DroppingBehaviour[] droppingsSlots;
    public IEditModeView EditModeView { get; set;}
    public event Action<CardWidget> onCardDroppedInEditMode;
    #endregion


    #region  SortBarView
    [SerializeField]
    TextMeshProUGUI cardCollectionBarText;

    [SerializeField]
    CollectionCardsSortButton cardsSortButton;
    public ICardCollectionBarView CardCollectionBarView { get; set ; }
    public event Action<uint> onSortButtonClicked;
    #endregion

    #region Unity Methods
    private void Awake()
    {
        deckCards = deck.GetComponentsInChildren<CardWidget>();
        deckButtonWidgets = deckBarButtonsContainer.GetComponentsInChildren<DeckButtonWidget>();
        droppingsSlots = deck.GetComponentsInChildren<DroppingBehaviour>();

        DeckView = new BattleDeckView(deckCards);
        BarView = new DeckBarView(deckButtonWidgets);
        CollectionView = new CardCollectionView(cardPrefab, cardCollectionColumnContainer, cardCollectionRowsContainer, cardCollectionRowsContentCount);
        EditModeView = new EditModeView(cardCollectionColumnContainer, editModeCardContainer, editModeCard);
        CardCollectionBarView = new CardCollectionBarView(cardsSortButton, cardCollectionBarText);
    }


    private void OnEnable()
    {
        cardsSortButton.onElementClicked += OnSortButtonClicked;
        exitEditModeButton.onClick.AddListener(OnExitEditModeButtonClicked);
        foreach (var item in deckButtonWidgets)
            item.onElementClicked += OnBarButtonClicked;
    }
    private void OnDisable()
    {
        cardsSortButton.onElementClicked -= OnSortButtonClicked;
        exitEditModeButton.onClick.RemoveAllListeners();
        foreach (var item in deckButtonWidgets)
            item.onElementClicked -= OnBarButtonClicked;
    }
    #endregion


    private void OnBarButtonClicked(uint buttonIndex)
    {
        onDeckBarButtonClicked?.Invoke(buttonIndex);
    }

    public void SetClickListenerToCards()
    {
        foreach (var item in CollectionView.GetCardCollection)
        {
            item.onElementClicked += OnCardClicked;
            item.onSelectButtonClicked += OnSelectButtonClicked;
        }

        foreach (var item in deckCards)
        {
            item.onElementClicked += OnCardClicked;
        }

        foreach (var item in droppingsSlots)
        {
            item.onCardDropped += OnCardDropped;
        }

        ReloadView();
    }

    public void RemoveClickListenerToCards()
    {
        foreach (var item in CollectionView.GetCardCollection)
        {
            item.onElementClicked -= OnCardClicked;
            item.onSelectButtonClicked -= OnCardClicked;
        }

        foreach (var item in deckCards)
        {
            item.onElementClicked -= OnCardClicked;
        }

        foreach (var item in droppingsSlots)
        {
            item.onCardDropped -= OnCardDropped;
        }
    }

    private void OnCardClicked(CardWidget clickedCard)
    {
        onCardClicked?.Invoke(clickedCard);
    }

    private void OnSelectButtonClicked(CardWidget obj)
    {
        onSelectedButtonClicked?.Invoke(obj);
        ReloadView();
    }

    private void OnExitEditModeButtonClicked()
    {
        onExitEditModeButtonClicked?.Invoke();
    }

    private void OnCardDropped(CardWidget cardDropped)
    {
        onCardDroppedInEditMode?.Invoke(cardDropped);
    }

    private void OnSortButtonClicked(uint counter)
    {
        onSortButtonClicked?.Invoke(counter);
    }


    public void ReloadView()
    {
        StartCoroutine(ReloadGridGroup());
    }

    IEnumerator ReloadGridGroup()
    {
        yield return new WaitForEndOfFrame();
        cardCollectionColumnContainer.gameObject.SetActive(false);
        cardCollectionColumnContainer.gameObject.SetActive(true);
    }

}

