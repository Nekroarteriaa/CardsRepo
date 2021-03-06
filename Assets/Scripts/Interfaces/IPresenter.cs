using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPresenter
{
    void Present();
}

public interface IPresenter<T>
{
    void Present(T args);
}

public interface IDeckBarPresenter<T>: IPresenter
{
    void OnDeckButtonPressed(T indexButton);
    bool NeedsToReloadCardCollection { get; }

    void HideDeckBar();
    void ShowDeckBar();
}

public interface ICardCollectionBarPresenter<T>: IPresenter<T>
{
    void Present(ref CardData[] cardsData, SortTypes sortTypes);
    void HideCardCollectionBar();
    void ShowCardCollectionBar();
}

public interface IBattleDeckPresenter<T> : IPresenter<T>
{
    void PresentActiveDeck();
    CardData[] CardCollectionData ();
    void PresentEditModeOnDeckCards();
    void StopEditModeOnDeckCards();
    void SaveSelectedDeckChanges(ulong deckCarId, CardWidget deckCard, ulong collectionCardId, CardWidget collectionCard);
    ulong GetCardDataIDByValue(CardData cardData);

    void SaveSorttedCardCollectionOnDictionary(CardData[] sortedCardData);
}

public interface ICardSubMenuPresenter<T> : IPresenter<T>
{
    void HideSubMenu();
}
public interface ICardCollectionPresenter<T> : IPresenter<T>
{
    void ReloadCardCollection(T cardsData);
}

public interface ICardPickerPresenter<T>: IPresenter<T>
{
    void ShowSelectedCard(T selectedCard);
}

public interface IEditModePresenter<T> : IPresenter<T>
{
    void SwitchCardFromCollectionToDeck(T deckCard);
    void ExitEditMode();
}
