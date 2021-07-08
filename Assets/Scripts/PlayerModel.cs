using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// Hold the player data provided by the server
// and allow you to interact with the server to set decks
public class PlayerModel
{
    public const uint DECK_SIZE = 8;
    public const uint DECK_COLLECTION_SIZE = 5;

    DeckCollection deckCollection;
    Dictionary<uint, Card[]> cardsCollection = new Dictionary<uint, Card[]>();

    // Generate fake data on creation.
    public PlayerModel(Sprite[] availableCards)
    {
        deckCollection = GeneratePlayerData();
        var cardCollection = new Card[availableCards.Length];
        for (uint i = 0; i < cardCollection.Length; i++)
        {
            cardCollection[i] = GenerateCard(i, availableCards[i].name);
        }

        for (uint i = 0; i < DECK_COLLECTION_SIZE; i++)
        {
            cardsCollection.Add(i, cardCollection);
        }
    }

    public Card[] GetCards()
    {
        return cardsCollection[deckCollection.activeDeck];
    }

    public ulong[] GetActiveDeck()
    {
        return GetDeckAtIndex(deckCollection.activeDeck);
    }

    public uint GetActiveDeckIndex()
    {
        return deckCollection.activeDeck;
    }

    public ulong[] GetDeckAtIndex(uint deckIndex)
    {
        return deckCollection.decks[deckIndex].ids;
    }

    public void SetDeckAtIndex(uint deckIndex, ulong[] deck)
    {
        if (deckIndex >= DECK_COLLECTION_SIZE)
            throw new System.Exception("Deck index over the maximum size: " + (DECK_COLLECTION_SIZE - 1).ToString());

        if (deck.Length != DECK_SIZE)
            throw new System.Exception(
                string.Format("Invalid deck size, excpecting size of {0} but got {1}", DECK_SIZE, deck.Length));

        if (deck.Distinct().ToArray().Length != DECK_SIZE)
            throw new System.Exception("Deck contains duplicated cards");

        deckCollection.decks[deckIndex] = new Deck()
        {
            ids = deck
        };
    }

    public void SetActiveDeck(uint deckIndex)
    {
        if (deckIndex >= DECK_COLLECTION_SIZE)
            throw new System.Exception("Deck index over the maximum size: " + (DECK_COLLECTION_SIZE - 1).ToString());

        deckCollection.activeDeck = deckIndex;
    }

    DeckCollection GeneratePlayerData()
    {
        var defaultCards = new ulong[DECK_SIZE];
        for (uint i = 0; i < defaultCards.Length; i++)
            defaultCards[i] = i;

        Deck deck = new Deck()
        {
            ids = defaultCards
        };

        var collection = new DeckCollection()
        {
            decks = new Deck[DECK_COLLECTION_SIZE],
            activeDeck = 0
        };

        for (uint i = 0; i < DECK_COLLECTION_SIZE; i++)
            collection.decks[i] = deck;

        return collection;
    }

    Card GenerateCard(ulong id, string assetId)
    {
        return new Card()
        {
            assetId = assetId,
            id = id,
            level = (uint)Random.Range(1, 10),
            energy = (uint)Random.Range(1, 8),
            rarity = (Rarity)Random.Range(0, (int)Rarity.MAX)
        };
    }



}