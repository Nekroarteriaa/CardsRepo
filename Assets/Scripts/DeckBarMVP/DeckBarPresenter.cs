using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckBarPresenter : IDeckBarPresenter<uint>
{

    public bool NeedsToReloadCardCollection => !isTheSameButtonIndex;
    
    private readonly IDeckBarView<uint> deckBarView;
    private readonly PlayerModel player;
    uint previousIndexButton = 0;
    bool isTheSameButtonIndex;

    public DeckBarPresenter(IDeckBarView<uint> deckBarView, PlayerModel player)
    {
        this.deckBarView = deckBarView;
        this.player = player;
    }

    public void Present()
    {
        ShowCurrentDeck();
    }

    void ShowCurrentDeck()
    {
        var activeIndex = player.GetActiveDeckIndex();
        OnDeckButtonPressed(activeIndex);
        deckBarView.SwitchDeck(activeIndex);
        previousIndexButton = 0;
    }

    public void OnDeckButtonPressed(uint indexButton)
    {
        player.SetActiveDeck(indexButton);
        deckBarView.SwitchDeck(indexButton);

        isTheSameButtonIndex = previousIndexButton == indexButton;
        previousIndexButton = indexButton;
    }

}
