using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public interface IBattleDeckView
{
    void AssignDataToBattleDeck(CardData[] cardsData);
    void AnimateDeckOnEditMode();
    void StopAnimationOnDeckOnEditMode();
}

public interface IDeckBarView<T>
{
    void SwitchDeck(T deckIndex);
    int BarButtonsCount { get; }
}

public interface IDeckButton<T> : IElementClicked<T>
{
    void DeckState(bool state);
}

public interface ICardButton<T> : IElementClicked<T>
{
    event Action<T> onInfoButtonClicked;
    event Action<T> onSelectButtonClicked;
    bool IsDeckCard { get; }
    CardData CardWidgetData{get;}
}

public interface ICardCollectionView
{
    void CreateCardCollection(CardData[] obj);
    List<CardWidget> GetCardCollection { get; }

    void ReloadCardCollection(CardData[] newCollection);

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
    event Action onElementCliked;
}


public interface ICardSelectionView : IViewController<uint>, IDeckBarButtonHandler<uint>, ICardClicked<CardWidget>, IDropCard<CardWidget>
{
    void ReloadView();
}

public interface IViewController<T>
{
    IBattleDeckView DeckView { get; set; }
    IDeckBarView<T> BarView { get; set; }
    ICardCollectionView CollectionView { get; set; }
    IEditModeView EditModeView { get; set; }
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

public interface IEditModeElement
{
    CardWidget SelectedCardCollectionElement { get; }
    void SetSelectedElementTransform(CardWidget selectedElementTransform);
}

public interface IDropCard<T>
{
    event Action<T> onCardDroppedInEditMode;
}

public interface IDraggingBehaviour: IBeginDragHandler,IEndDragHandler,IDragHandler
{
    RectTransform DraggedElementRectTransform { get; }
    CanvasGroup DraggedElementCanvasGroup { get;}
    void SetDraggedElementToOriginalPosition();
}

public interface IDroppingBehaviour<T> : IDropHandler
{
    event Action<T> onCardDropped;
}

//public interface ICardClicked<T>
//{
//    event Action<T> onCardClicked;

//    event Action<T> onSelectedButtonClicked;
//    void SetClickListenerToCards();
//    void RemoveClickListenerToCards();
//}