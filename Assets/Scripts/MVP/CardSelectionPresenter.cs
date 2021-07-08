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
    public readonly ICardCollectionBarPresenter<uint> CardCollectionBarPrsntr;
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
        BattleDeckPrsntr.Present(cardSelectionView.BarView.BarButtonsCount);
        DeckBarPrsntr.Present();
        CardCollectionPrsntr.Present(BattleDeckPrsntr.CardCollectionData());
        cardSelectionView.SetClickListenerToCards();
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
        ReloadTheCardCollection();
    }

    private void ReloadTheCardCollection()
    {
        if (!DeckBarPrsntr.NeedsToReloadCardCollection)
            return;
        CardCollectionPrsntr.ReloadCardCollection(BattleDeckPrsntr.CardCollectionData());
    }

    private void PresentTheSelectedDeck(uint buttonIndex)
    {
        DeckBarPrsntr.OnDeckButtonPressed(buttonIndex);
        BattleDeckPrsntr.PresentActiveDeck();
    }

    void OnSortButtonClicked(uint counter)
    {

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
