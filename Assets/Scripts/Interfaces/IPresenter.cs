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
}

public interface IBattleDeckPresenter : IPresenter
{
    void PresentActiveDeck();
    CardData[] CardCollectionData{ get; }
}

public interface ICardSubMenuPresenter<T> : IPresenter<T>
{
    void HideSubMenu();
}
public interface ICardCollectionPresenter<T> : IPresenter<T>
{
    void PresentCardCollection(T cardsData);
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
