using UnityEngine.UI;
using UnityEngine;

// Extend as requiered
public class CardWidget : MonoBehaviour
{
    [SerializeField]
    Image cardPicture;
    [SerializeField]
    Image cardFrame;
    [SerializeField]
    Image cardMask;
    [SerializeField]
    TMPro.TMP_Text level;
    [SerializeField]
    TMPro.TMP_Text energy;

    public void SetCardData (CardData cardData)
    {
        level.text  = string.Format("Level {0:N0}", cardData.level);
        energy.text = cardData.energy.ToString("N0");
        cardPicture.sprite = cardData.picture;
        cardMask.sprite = cardData.mask;
        cardFrame.sprite = cardData.frame;
    }
}