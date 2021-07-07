using System;
using System.Collections.Generic;
using UnityEngine;

// The presenter will be repsonsible for communicating with the "PlayerModel"
// and formating the Data provided by the "PlayerModel" for the view
public class BattleDeckPresenter : IBattleDeckPresenter
{
    private readonly IBattleDeckView view;
    private readonly PlayerModel player;
    private readonly IGraphicResources resources;

    // list of the cards the player has on its account.
    private Dictionary<ulong, CardData> uiCards;
    public CardData[] CardCollectionData => GetCardCollectionData(player.GetActiveDeck());


    public BattleDeckPresenter(IBattleDeckView view, PlayerModel player, IGraphicResources resources)
    {
        this.view = view;
        this.player = player;
        this.resources = resources;
    }

    // Entry point
    public void Present()
    {
        uiCards = GenerateUIData(player.GetCards());
        PresentActiveDeck();
    }


    public void PresentActiveDeck()
    {
        var cardIds = player.GetActiveDeck();
        view.AssignDataToBattleDeck(GetCardsData(cardIds));
    }


    void Setup()
    {
        


        // example
        // view.ActiveDeckIndex = player.GetActiveDeckIndex();
        // view.onSetCardInDeck += OnSetCardInDeck
        // ...
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
        if (uiCards.TryGetValue(id, out CardData cardData))
            return cardData;

        return null;
    }

    CardData[] GetCardCollectionData(ulong[] deckCardsIDs)
    {
        var tempUICards = new Dictionary<ulong, CardData>(uiCards);
        
        foreach (var item in deckCardsIDs)
            tempUICards.Remove(item);

        var data = new CardData[tempUICards.Count];
        var dataIndex = 0;

        foreach (var item in tempUICards.Values)
        {
            data[dataIndex] = item;
            dataIndex++;
        }

        return data;

    }
}