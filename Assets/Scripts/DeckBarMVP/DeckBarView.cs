using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckBarView : IDeckBarView<uint>
{
    uint previousActiveIndex;
    private readonly DeckButtonWidget[] deckButtonWidgets;

    public int BarButtonsCount => deckButtonWidgets.Length;

    public DeckBarView(DeckButtonWidget[] deckButtonWidgets)
    {
        this.deckButtonWidgets = deckButtonWidgets;
    }

    public void SwitchDeck(uint deckIndex)
    {
        deckButtonWidgets[previousActiveIndex].DeckState(false);
        deckButtonWidgets[deckIndex].DeckState(true);
        previousActiveIndex = deckIndex;
    }

}
