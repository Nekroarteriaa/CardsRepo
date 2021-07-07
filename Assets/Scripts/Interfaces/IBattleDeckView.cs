using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public interface IBattleDeckView
{
    void AssignDataToBattleDeck(CardData[] cardsData);
}

public interface IDeckBarView<T>
{
    void SwitchDeck(T deckIndex);
}

public interface IDeckButton<T> : IElementClicked<T>
{
    void DeckState(bool state);
}

public interface ICardButton<T> : IElementClicked<T>
{
    //void s(bool state);
    event Action<T> onInfoButtonClicked;
    event Action<T> onSelectButtonClicked;
    bool IsDeckCard { get; }
    CardData CardWidgetData{get;}
}

public interface IEditModeElement
{
    CardWidget SelectedCardCollectionElement { get; }
    void SetSelectedElementTransform(CardWidget selectedElementTransform);
    //int GetSelectedElementSibilingNumber();
}

public interface ICardCollectionView
{
    void CreateCardCollection(CardData[] obj);
    List<CardWidget> GetCardCollection { get; }
    
}

public interface IEditModeView
{
    CardWidget EditModeCard { get; }
    bool IsInEditMode { get; }
    void SetUpForEditMode();
    void SetEditModeCardData(CardWidget selectedCard);
    void SetUpForNormalMode();
}


public interface IElementClicked<T>
{
    event Action<T> onElementClicked;
}

public interface IElementClicked
{
    Action onElementCliked { get; }
}


public interface ICardSelectionView : IViewController<uint>, IDeckBarButtonHandler<uint>, ICardClicked<CardWidget>, IDropCard<CardWidget>
{

    void ReloadView();
    //event Action

}

public interface IViewController<T>
{
    IBattleDeckView DeckView { get; set; }
    IDeckBarView<T> BarView { get; set; }
    ICardCollectionView CollectionView { get; set; }
    IEditModeView EditModeView { get; set; }

    //void SetClickListenerToCollectionCards(IViewController view);
    //event Action

}


// Pasar a el script de models
public interface IGraphicResources
{
    Sprite GetPictureForCard(string assetId);
    Sprite GetFrameForRarity(Rarity rarity);
    Sprite GetMaskForRarity(Rarity rarity);

}

public interface IDeckBarButtonHandler<T>
{
    event Action<T> onDeckBarButtonClicked;
}

public interface ICardClicked<T>
{
    event Action<T> onCardClicked;

    event Action<T> onSelectedButtonClicked;

    event Action onExitEditModeButtonClicked;
    void SetClickListenerToCards();
    void RemoveClickListenerToCards();
}

public interface IDropCard<T>
{
    event Action<T> onDropCard;
}

public interface IDraggingBehaviour: IPointerDownHandler,IBeginDragHandler,IEndDragHandler,IDragHandler
{

}

public interface IDroppingBehaviour<T> : IDropHandler
{

}

//public interface ICardClicked<T>
//{
//    event Action<T> onCardClicked;

//    event Action<T> onSelectedButtonClicked;
//    void SetClickListenerToCards();
//    void RemoveClickListenerToCards();
//}