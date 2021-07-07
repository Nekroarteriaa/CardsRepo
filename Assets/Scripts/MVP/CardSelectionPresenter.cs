using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardSelectionPresenter : IPresenter
{
    public readonly IBattleDeckPresenter BattleDeckPrsntr;
    public readonly IDeckBarPresenter<uint> DeckBarPrsntr;
    public readonly ICardCollectionPresenter<CardData[]> CardCollectionPrsntr;
    public readonly CardSubMenuPresenter CardSubMenuPrstr;
    public readonly IEditModePresenter<CardWidget> EditCardModePrsntr;
    public readonly ICardSelectionView cardSelectionView;


    public CardSelectionPresenter(ICardSelectionView cardSelectionView, PlayerModel player, IGraphicResources graphicResources)
    {
        BattleDeckPrsntr = new BattleDeckPresenter(cardSelectionView.DeckView, player, graphicResources);
        DeckBarPrsntr = new DeckBarPresenter(cardSelectionView.BarView, player);
        CardCollectionPrsntr = new CardCollectionPresenter(cardSelectionView.CollectionView);
        CardSubMenuPrstr = new CardSubMenuPresenter();
        EditCardModePrsntr = new EditModePresenter(cardSelectionView.EditModeView);

        this.cardSelectionView = cardSelectionView;
        this.cardSelectionView.onDeckBarButtonClicked += OnDeckBarButtonClicked;
        this.cardSelectionView.onSelectedButtonClicked += OnSelectedButtonClicked;
        this.cardSelectionView.onCardClicked += OnCardClicked;
        this.cardSelectionView.onExitEditModeButtonClicked += OnExitEditModeButtonClicked;
        this.cardSelectionView.onDropCard += OnDropCard;

    }

    public void Present()
    {
        LoadGameCards();
    }

    private void LoadGameCards()
    {
        BattleDeckPrsntr.Present();
        DeckBarPrsntr.Present();
        CardCollectionPrsntr.Present(BattleDeckPrsntr.CardCollectionData);
        cardSelectionView.SetClickListenerToCards();
    }

  

    private void OnCardClicked(CardWidget obj)
    {
        if (!cardSelectionView.EditModeView.IsInEditMode)
            CardSubMenuPrstr.Present(obj);
        else
        {
            EditCardModePrsntr.SwitchCardFromCollectionToDeck(obj);
            ExitEditMode();
        }

    }

    private void OnDropCard(CardWidget obj)
    {
        EditCardModePrsntr.SwitchCardFromCollectionToDeck(obj);
        ExitEditMode();
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
    }

    private void OnSelectedButtonClicked(CardWidget obj)
    {
        EditCardModePrsntr.Present(obj);
    }

    private void OnDeckBarButtonClicked(uint buttonIndex)
    {
        LoadDeck(buttonIndex);
    }

    private void LoadDeck(uint buttonIndex)
    {
        BattleDeckPrsntr.PresentActiveDeck();
        DeckBarPrsntr.OnDeckButtonPressed(buttonIndex);

        if (!DeckBarPrsntr.NeedsToReloadCardCollection)
            return;
    }



    ~CardSelectionPresenter()
    {
        cardSelectionView.onDeckBarButtonClicked -= OnDeckBarButtonClicked;
        cardSelectionView.onSelectedButtonClicked -= OnSelectedButtonClicked;
        this.cardSelectionView.onCardClicked -= OnCardClicked;
        this.cardSelectionView.onExitEditModeButtonClicked -= OnExitEditModeButtonClicked;
    }
}
