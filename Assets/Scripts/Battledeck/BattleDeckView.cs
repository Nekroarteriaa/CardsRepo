using UnityEngine;
using UnityEngine.UI;

// Your view will be responsible for displaying the formated data
// and manage user interaction
// Feel free to modify anything in there
public partial class BattleDeckView : MonoBehaviour, ICardShower
{
    [SerializeField]
    Transform deck;

    //[SerializeField]
    //Button 

    CardWidget[] deckCards;

    #region UnityMethods
    void Awake()
    {
        deckCards = deck.GetComponentsInChildren<CardWidget>();
    }
    #endregion

    public void ShowActiveDeck(CardData[] cardsData)
    {
        var deckCardsLength = deckCards.Length;
        var cardsDataLength = cardsData.Length;

        for (int i = 0; i < deckCardsLength; i++)
        {
            if (i <= cardsDataLength)
            {
                deckCards[i].SetCardData(cardsData[i]);
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