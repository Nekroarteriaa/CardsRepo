using System;
using System.Collections.Generic;
using UnityEngine;

// The presenter will be repsonsible for communicating with the "PlayerModel"
// and formating the Data provided by the "PlayerModel" for the view
public class BattleDeckPresenter : IBattleDeckPresenter<int>
{
    private readonly IBattleDeckView view;
    private readonly PlayerModel player;
    private readonly IGraphicResources resources;

    // list of the cards the player has on its account.
    //private Dictionary<ulong, CardData> uiCards;
    private Dictionary<ulong, Dictionary<ulong, CardData>> decks = new Dictionary<ulong, Dictionary<ulong, CardData>>();
    //public CardData[] CardCollectionData => GetCardCollectionData(player.GetActiveDeck());


    public BattleDeckPresenter(IBattleDeckView view, PlayerModel player, IGraphicResources resources)
    {
        this.view = view;
        this.player = player;
        this.resources = resources;
    }

    // Entry point
    public void Present(int numberOfDecks)
    {
        CreateDecks(numberOfDecks);
        PresentActiveDeck();
    }


    public void PresentActiveDeck()
    {
        var cardIds = player.GetActiveDeck();
        view.AssignDataToBattleDeck(GetCardsData(cardIds));
    }
    public void PresentEditModeOnDeckCards()
    {
        view.AnimateDeckOnEditMode();
    }

    public void StopEditModeOnDeckCards()
    {
        view.StopAnimationOnDeckOnEditMode();
    }

    public void SaveSelectedDeckChanges(ulong deckCarId, CardWidget deckCard, ulong collectionCardId, CardWidget collectionCard)
    {
        var activeDeck = decks[player.GetActiveDeckIndex()];
        activeDeck[deckCarId] = collectionCard.CardWidgetData; 
        activeDeck[collectionCardId] = deckCard.CardWidgetData;

        decks[player.GetActiveDeckIndex()] = activeDeck;
    }

    public CardData[] CardCollectionData()
    {
        return GetCardCollectionData(player.GetActiveDeck());
    }

    void CreateDecks(int numberOfDecks)
    {
        var deckSize = Convert.ToUInt64(numberOfDecks);
        for (ulong i = 0; i < deckSize; i++)
        {
            decks.Add(i, GenerateUIData(player.GetCards()));
        }
            
    }

    Dictionary<ulong, CardData> GenerateUIData(Card[] cards)
    {
        var uiData = new Dictionary<ulong, CardData>();
        for (int i = 0; i < cards.Length; i++)
        {
            var card = cards[i];
            uiData.Add(card.id, FormatCardData(card));
        }

        return uiData;
    }

    CardData FormatCardData(Card card)
    {
        return new CardData()
        {
            energy = card.energy,
            level = card.level,
            rarity = card.rarity,
            picture = resources.GetPictureForCard(card.assetId),
            frame = resources.GetFrameForRarity(card.rarity),
            mask = resources.GetMaskForRarity(card.rarity)
        };
    }

    CardData[] GetCardsData(ulong[] ids)
    {
        var data = new CardData[ids.Length];
        for (int i = 0; i < ids.Length; i++)
            data[i] = GetCardDataById(ids[i]);

        return data;
    }

    CardData GetCardDataById(ulong id)
    {
        var uiCards = decks[player.GetActiveDeckIndex()];
        if (uiCards.TryGetValue(id, out CardData cardData))
            return cardData;
        return null;
    }

    CardData[] GetCardCollectionData(ulong[] deckCardsIDs)
    {
        var uiCards = decks[player.GetActiveDeckIndex()];
        var tempUICards = new Dictionary<ulong, CardData>(uiCards);

        foreach (var removalCardsId in deckCardsIDs)
            tempUICards.Remove(removalCardsId);

        var data = new CardData[tempUICards.Count];
        var dataIndex = 0;

        foreach (var collectionCard in tempUICards.Values)
        {
            data[dataIndex] = collectionCard;
            dataIndex++;
        }

        return data;
    }

    public ulong GetCardDataIDByValue(CardData cardData)
    {
        var uiCards = decks[player.GetActiveDeckIndex()];
        ulong keyValue = 0;
        foreach (var item in uiCards.Keys)
        {
            if (uiCards[item].Equals(cardData))
            {
                keyValue = item;
                break;
            }
        }

        return keyValue;
    }

    
}