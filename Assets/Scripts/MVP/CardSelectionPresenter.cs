using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardSelectionPresenter : IPresenter
{
    public readonly IBattleDeckPresenter<int> BattleDeckPrsntr;
    public readonly IDeckBarPresenter<uint> DeckBarPrsntr;
    public readonly ICardCollectionPresenter<CardData[]> CardCollectionPrsntr;
    public readonly CardSubMenuPresenter CardSubMenuPrstr;
    public readonly IEditModePresenter<CardWidget> EditCardModePrsntr;
    public readonly ICardCollectionBarPresenter<SortTypes> CardCollectionBarPrsntr;
    public readonly ICardSelectionView cardSelectionView;
    


    public CardSelectionPresenter(ICardSelectionView cardSelectionView, PlayerModel player, IGraphicResources graphicResources)
    {
        BattleDeckPrsntr = new BattleDeckPresenter(cardSelectionView.DeckView, player, graphicResources);
        DeckBarPrsntr = new DeckBarPresenter(cardSelectionView.BarView, player);
        CardCollectionPrsntr = new CardCollectionPresenter(cardSelectionView.CollectionView);
        CardSubMenuPrstr = new CardSubMenuPresenter();
        EditCardModePrsntr = new EditModePresenter(cardSelectionView.EditModeView);
        CardCollectionBarPrsntr = new CardCollectionBarPresenter(cardSelectionView.CardCollectionBarView);

        this.cardSelectionView = cardSelectionView;
        this.cardSelectionView.onDeckBarButtonClicked += OnDeckBarButtonClicked;
        this.cardSelectionView.onSelectedButtonClicked += OnSelectedButtonClicked;
        this.cardSelectionView.onCardClicked += OnCardClicked;
        this.cardSelectionView.onExitEditModeButtonClicked += OnExitEditModeButtonClicked;
        this.cardSelectionView.onCardDroppedInEditMode += OnDropCard;
        this.cardSelectionView.onSortButtonClicked += OnSortButtonClicked;

    }

    public void Present()
    {
        LoadGameCards();
    }

    private void LoadGameCards()
    {
        LoadDeckCards();
        LoadCollectionCards();
        cardSelectionView.SetClickListenerToCards();
    }

    private void LoadDeckCards()
    {
        BattleDeckPrsntr.Present(cardSelectionView.BarView.BarButtonsCount);
        DeckBarPrsntr.Present();
    }

    private void LoadCollectionCards()
    {
        CardData[] collectionCardData = BattleDeckPrsntr.CardCollectionData();
        collectionCardData = SortCollectionCards(ref collectionCardData);
        CardCollectionPrsntr.Present(collectionCardData);
    }


    private void OnCardClicked(CardWidget clickedCard)
    {
        if (!cardSelectionView.EditModeView.IsInEditMode)
            CardSubMenuPrstr.Present(clickedCard);
        else
        {
            SwitchCardsPlaces(clickedCard);
        }

    }


    private void OnDropCard(CardWidget deckCard)
    {
        SwitchCardsPlaces(deckCard);
    }

    private void SwitchCardsPlaces(CardWidget deckCard)
    {
        SaveSwitchDataOnTheDictionary(deckCard);
        EditCardModePrsntr.SwitchCardFromCollectionToDeck(deckCard);
        ExitEditMode();

        CardData[] collectionCardData = BattleDeckPrsntr.CardCollectionData();
        SortAndReloadCardCollection(ref collectionCardData);
    }

    private void SaveSwitchDataOnTheDictionary(CardWidget deckCard)
    {
        var deckCardId = BattleDeckPrsntr.GetCardDataIDByValue(deckCard.CardWidgetData);
        var collectionCardId = BattleDeckPrsntr.GetCardDataIDByValue(cardSelectionView.EditModeView.EditModeCard.CardWidgetData);
        BattleDeckPrsntr.SaveSelectedDeckChanges(deckCardId, deckCard, collectionCardId, cardSelectionView.EditModeView.EditModeCard);
    }

    private void OnExitEditModeButtonClicked()
    {
        ExitEditMode();
    }

    private void ExitEditMode()
    {
        DeckBarPrsntr.ShowDeckBar();
        CardCollectionBarPrsntr.ShowCardCollectionBar();
        CardSubMenuPrstr.HideSubMenu();
        EditCardModePrsntr.ExitEditMode();
        cardSelectionView.ReloadView();
        BattleDeckPrsntr.StopEditModeOnDeckCards();
    }

    private void OnSelectedButtonClicked(CardWidget obj)
    {
        EnterEditMode(obj);
    }

    private void EnterEditMode(CardWidget obj)
    {
        DeckBarPrsntr.HideDeckBar();
        CardCollectionBarPrsntr.HideCardCollectionBar();
        EditCardModePrsntr.Present(obj);
        BattleDeckPrsntr.PresentEditModeOnDeckCards();
    }

    private void OnDeckBarButtonClicked(uint buttonIndex)
    {
        LoadDeck(buttonIndex);
    }

    private void LoadDeck(uint buttonIndex)
    {
        PresentTheSelectedDeck(buttonIndex);
        LoadSelectedDeckCardCollection();
    }

    private void PresentTheSelectedDeck(uint buttonIndex)
    {
        DeckBarPrsntr.OnDeckButtonPressed(buttonIndex);
        BattleDeckPrsntr.PresentActiveDeck();
    }

    private void LoadSelectedDeckCardCollection()
    {
        if (!DeckBarPrsntr.NeedsToReloadCardCollection)
            return;

        CardData[] collectionCardData = BattleDeckPrsntr.CardCollectionData();
        SortAndReloadCardCollection(ref collectionCardData);
    }

   

    void OnSortButtonClicked(SortTypes counter)
    {
        CardData[] collectionCardData = BattleDeckPrsntr.CardCollectionData();
        SortAndReloadCardCollection(ref collectionCardData);
        this.cardSelectionView.CardCollectionBarView.AnimateSortButton();
    }


    CardData[] SortCollectionCards(ref CardData[] cardsData)
    {
        CardCollectionBarPrsntr.Present(ref cardsData, cardSelectionView.SortSearch);
        BattleDeckPrsntr.SaveSorttedCardCollectionOnDictionary(cardsData);
        return cardsData;
    }

    void SortAndReloadCardCollection(ref CardData[] cardsData)
    {
        CardData[] collectionCardData = cardsData;
        collectionCardData = SortCollectionCards(ref collectionCardData);
        CardCollectionPrsntr.ReloadCardCollection(collectionCardData);
    }

    ~CardSelectionPresenter()
    {
        this.cardSelectionView.onDeckBarButtonClicked -= OnDeckBarButtonClicked;
        this.cardSelectionView.onSelectedButtonClicked -= OnSelectedButtonClicked;
        this.cardSelectionView.onCardClicked -= OnCardClicked;
        this.cardSelectionView.onExitEditModeButtonClicked -= OnExitEditModeButtonClicked;
        this.cardSelectionView.onCardDroppedInEditMode -= OnDropCard;
        this.cardSelectionView.onSortButtonClicked -= OnSortButtonClicked;
    }
}
