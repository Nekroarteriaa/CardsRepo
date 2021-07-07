using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardSubMenuPresenter : ICardSubMenuPresenter<CardWidget>
{
    CardWidget previousPickedCard;

    public void Present(CardWidget selectedCard)
    {
        if (previousPickedCard != null)
            previousPickedCard.HideClickSubMenu();

        selectedCard.ShowClickSubMenu();
        previousPickedCard = selectedCard;
    }
    public void HideSubMenu()
    {
        previousPickedCard.HideClickSubMenu();
        previousPickedCard = null;
    }

}
