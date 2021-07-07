using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using NSubstitute;
using UnityEngine.TestTools;
using System.Linq;

namespace Tests
{
    public class testingCardsView
    {
        // A Test behaves as an ordinary method
        [Test]
        public void testingCardsViewSimplePasses()
        {
            //Sprite[] cardsSprites = Resources.LoadAll<Sprite>("CardsResources/card_1");
            //Sprite[] frames = Resources.LoadAll<Sprite>("CardsResources/card_frame");
            //Sprite[] mask = Resources.LoadAll<Sprite>("CardsResources/card_mask");

            //GameObject go = new GameObject();
            //CardWidget cardPref = go.AddComponent<CardWidget>();

            //GameObject[] goArray = new GameObject[8];
            //List<CardWidget> cardDeck = new List<CardWidget>();

            //for (int i = 0; i < length; i++)
            //{

            //}



            //var graphicResources = new GraphicResourcesTest(cardsSprites, frames, mask);
            //var view = Substitute.For<IView>();

            //view.DeckCards.Returns(new CardWidget[8]);
            //view.DeckButtonWidgets.Returns(new DeckButtonWidget[5]);
            //view.CardPrefab.Returns(cardPref);

            //GameObject[] goArray = new GameObject[5];
            //List<DeckButtonWidget> buttonsComponents = new List<DeckButtonWidget>();
            //for (int i = 0; i < goArray.Length; i++)
            //{
            //    var btnWidget = goArray[i].AddComponent<DeckButtonWidget>();
            //    buttonsComponents.Add(btnWidget);
            //}

            //var player = new PlayerModel(new Sprite[35]);
            //var deckBarView = new DeckBarView(buttonsComponents.ToArray());

            // var deckBarPresenter = new DeckBarPresenter(deckBarView, player,)



           
            //var view = Substitute.For<IView>();

            ////view.DeckCards.Returns(new CardWidget[8]);
            ////view.DeckButtonWidgets.Returns(new DeckButtonWidget[5]);
            ////view.CardPrefab.Returns(cardPref);

            //GameObject[] goArray = new GameObject[5];
            //List<DeckButtonWidget> buttonsComponents = new List<DeckButtonWidget>();
            //for (int i = 0; i < goArray.Length; i++)
            //{
            //    var btnWidget = goArray[i].AddComponent<DeckButtonWidget>();
            //    buttonsComponents.Add(btnWidget);
            //}

            
            //var deckBarView = new DeckBarView(buttonsComponents.ToArray());

            //var deckBarPresenter = new DeckBarPresenter(deckBarView, player,)

            //GameObject[] goArray = new GameObject[5];
            //List<DeckButtonWidget> buttonsComponents = new List<DeckButtonWidget>();
            //for (int i = 0; i < goArray.Length; i++)
            //{
            //    var btnWidget = goArray[i].AddComponent<DeckButtonWidget>();
            //    buttonsComponents.Add(btnWidget);
            //}

            //var player = new PlayerModel(new Sprite[35]);
            //var deckBarView = new DeckBarView(buttonsComponents.ToArray());






















            //GameObject[] cardsgo = new GameObject[8];
            //List<CardWidget> cardsComponents = new List<CardWidget>();

            //for (int i = 0; i < cardsgo.Length; i++)
            //{
            //    var cardWidget = cardsgo[i].AddComponent<CardWidget>();
            //    cardsComponents.Add(cardWidget);
            //}

            //GameObject[] buttonsgo = new GameObject[5];
            //List<DeckButtonWidget> buttonsComponents = new List<DeckButtonWidget>();

            //for (int i = 0; i < buttonsgo.Length; i++)
            //{
            //    var cardWidget = buttonsgo[i].AddComponent<DeckButtonWidget>();
            //    buttonsComponents.Add(cardWidget);
            //}

            //GameObject gocollection = new GameObject();
            //var cardCollectionPrefab = gocollection.AddComponent<CardWidget>();
            //GameObject collectionContainer = new GameObject();

            //var battleDeckView = new BattleDeckView(cardsComponents.ToArray());
            //var barView = new DeckBarView(buttonsComponents.ToArray());
            //var collectionView = new CardCollectionView(cardCollectionPrefab, collectionContainer.transform);

            //view.DeckView.Returns(battleDeckView);
            //view.BarView.Returns(barView);
            //view.CollectionView.Returns(collectionView);

            //List<CardWidget> cardsComponents = new List<CardWidget>();
            //for (int i = 0; i < 8; i++)
            //{
            //    var cardWidget = Substitute.For<>
            //    cardsComponents.Add()
            //}

             Sprite[] cardsSprites = Resources.LoadAll<Sprite>("CardsResources/card_1");
            Sprite[] frames = Resources.LoadAll<Sprite>("CardsResources/card_frame");
            Sprite[] mask = Resources.LoadAll<Sprite>("CardsResources/card_mask");

            var graphicResources = new GraphicResourcesTest(cardsSprites, frames, mask);
            var player = new PlayerModel(cardsSprites);

            var go = new GameObject();
            var view = go.AddComponent<CardSelectionView>();


            var go2 = new GameObject();
            var cardWig = go2.AddComponent<CardWidget>();

            ICardSelectionView nView = Substitute.For<ICardSelectionView>();
            nView.DeckView = Substitute.For<IBattleDeckView>();
            nView.BarView = Substitute.For<IDeckBarView<uint>>();
            nView.CollectionView = Substitute.For<ICardCollectionView>();

            var cardSelectorPresenter = new CardSelectionPresenter(nView, player, graphicResources);

            //cardSelectorPresenter.BattleDeckPrsntr.Present();
            //cardSelectorPresenter.BattleDeckPrsntr.PresentActiveDeck();
            //cardSelectorPresenter.Present();


            //cardSelectorPresenter.CardCollectionPrsntr.Present(new CardData[0]);


            //cardWig.OnCardClicked(cardWig);


            ICardPickerPresenter<CardWidget> cardPicker = Substitute.For<ICardPickerPresenter<CardWidget>>();
            cardPicker.Received().Present(cardWig);


           // cardSelectorPresenter.Received().Present();

        }

        //// A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
        //// `yield return null;` to skip a frame.
        //[UnityTest]
        //public IEnumerator testingCardsViewWithEnumeratorPasses()
        //{
        //    // Use the Assert class to test conditions.
        //    // Use yield to skip a frame.
        //    yield return null;
        //}
    }
}
