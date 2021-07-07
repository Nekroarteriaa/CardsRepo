using UnityEngine;

// Initialize the test
public class TestInitialization : MonoBehaviour
{
    [SerializeField]
    Sprite[] cards;

    void Start()
    {
        var model = new PlayerModel(cards);
        //var resources = GetComponent<GraphicResources>();
        var resources = GetComponent<GraphicResources>();
        var view = FindObjectOfType<CardSelectionView>();
        var battleDeckPresenter = new CardSelectionPresenter(view, model, resources);
        battleDeckPresenter.Present();


        //var view = FindObjectOfType<BattleDeckView>();
        //var battleDeckPresenter = new BattlePresenter(view, model, resources);
        //ActivatePresenter(battleDeckPresenter);

        //var deckBarView = FindObjectOfType<DeckBarView>();
        //var deckPresenter = new DeckBarPresenter(deckBarView, model, battleDeckPresenter);
        //ActivatePresenter(deckPresenter);
    }
}