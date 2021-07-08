using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditModePresenter : IEditModePresenter<CardWidget>
{
    private readonly IEditModeView editCardModeView;

    public EditModePresenter(IEditModeView editCardModeView)
    {
        this.editCardModeView = editCardModeView;
    }


    public void Present(CardWidget args)
    {
        editCardModeView.SetUpForEditMode();
        editCardModeView.SetEditModeCardData(args);
    }

    public void SwitchCardFromCollectionToDeck(CardWidget deckCard)
    {
        var dataTemp = deckCard.CardWidgetData;
        deckCard.SetCardData(editCardModeView.EditModeCard.SelectedCardCollectionElement.CardWidgetData, true);
        editCardModeView.EditModeCard.SelectedCardCollectionElement.SetCardData(dataTemp, false);
    }
    //public void SwitchCardUsingDrop(CardWidget deckCard)
    //{
    //    var dataTemp = deckCard.CardWidgetData;
    //    deckCard.SetCardData(editCardModeView.EditModeCard.SelectedCardCollectionElement.CardWidgetData, true);
    //    editCardModeView.EditModeCard.SelectedCardCollectionElement.SetCardData(dataTemp, false);
    //}
    public void ExitEditMode()
    {
        editCardModeView.SetUpForNormalMode();
    }
}
