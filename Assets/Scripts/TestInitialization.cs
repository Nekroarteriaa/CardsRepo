using UnityEngine;

// Initialize the test
public class TestInitialization : MonoBehaviour
{
    [SerializeField]
    Sprite[] cards;

    void Start()
    {
        var model = new PlayerModel(cards);
        var resources = GetComponent<GraphicResources>();
        var view = FindObjectOfType<BattleDeckView>();
        var presenter = new BattlePresenter(view, model, resources);
        presenter.Present();
    }
}