using UnityEngine;
using DG.Tweening;

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
        AnimateClickedButton(deckIndex);
    }

    public void AnimateClickedButton(uint deckIndex)
    {
        deckButtonWidgets[deckIndex].transform.DOScale(new Vector3(1.2f, 1.2f, 1.2f), .1f).OnComplete(() => {
            deckButtonWidgets[deckIndex].transform.DOScale(new Vector3(1f, 1f, 1f), .1f);
        });
    }

}
