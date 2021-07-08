using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardCollectionBarPresenter : ICardCollectionBarPresenter<uint>
{
    public ICardCollectionBarView CardCollectionBarView { get; }
    public CardCollectionBarPresenter(ICardCollectionBarView cardCollectionBarView)
    {
        CardCollectionBarView = cardCollectionBarView;
    }


    public void Present(uint args)
    {
        throw new System.NotImplementedException();
    }
}
