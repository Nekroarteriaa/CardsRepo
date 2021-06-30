using System.Collections.Generic;

// The presenter will be repsonsible for communicating with the "PlayerModel"
// and formating the Data provided by the "PlayerModel" for the view
public class BattlePresenter: IPresenter
{
    private readonly BattleDeckView view;
    private readonly PlayerModel player;
    private readonly GraphicResources resources;

    // list of the cards the player has on its account.
    private Dictionary<ulong, CardData> uiCards;

    public BattlePresenter(BattleDeckView view, PlayerModel player, GraphicResources resources)
    {
        this.view = view;
        this.player = player;
        this.resources = resources;
    }

    // Entry point
    public void Present()
    {
        Setup();
    }

    // Initialize your view data and view callbacks
    void Setup()
    {
        uiCards = GenerateUIData(player.GetCards());

        var cardIds = player.GetActiveDeck();
        //view.ActiveDeck = GetCardsData(cardIds);
        view.ShowActiveDeck(GetCardsData(cardIds));

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
        return new  CardData()
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
}