using UnityEngine;
using UnityEngine.UI;

// Your view will be responsible for displaying the formated data
// and manage user interaction
// Feel free to modify anything in there
public class BattleDeckView : IBattleDeckView
{
    private readonly CardWidget[] deckCards;

    public BattleDeckView(CardWidget[] deckCards)
    {
        this.deckCards = deckCards;
    }

    public void AssignDataToBattleDeck(CardData[] cardsData)
    {
        var deckCardsLength = deckCards.Length;
        var cardsDataLength = cardsData.Length;

        for (int i = 0; i < deckCardsLength; i++)
        {
            if (i <= cardsDataLength)
            {
                deckCards[i].SetCardData(cardsData[i], true);
                deckCards[i].gameObject.SetActive(true);
            }
            else
            {
                // hide the extra widgets if the deck is not using all of them
                deckCards[i].gameObject.SetActive(false);
            }
        }
    }
}