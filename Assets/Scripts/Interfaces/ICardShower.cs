using System;

public interface ICardShower
{
    void ShowActiveDeck(CardData[] cardsData);



    //Action<T> OnActivateDeck();

    //void OnActivateDeck(uint deckPressed);
    //void OnActivateEditMode();
}

public interface IElementClicked<T>
{
    Action<T> ElementClicked { get;}
}

public interface IElementClicked
{
    Action ElementCliked { get; }
}
