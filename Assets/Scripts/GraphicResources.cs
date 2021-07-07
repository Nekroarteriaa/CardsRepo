using UnityEngine;
using System.Collections.Generic;

// Simplify access to resources for the test
public class GraphicResources : MonoBehaviour, IGraphicResources
{
    [SerializeField] Sprite[] pictures;
    [SerializeField] Sprite[] frames;
    [SerializeField] Sprite[] mask;

    Dictionary<string, Sprite> cardPictures;

    void OnEnable()
    {
        cardPictures = new Dictionary<string, Sprite>();
        foreach( var p in pictures)
            cardPictures.Add(p.name, p);
    }

    public Sprite GetPictureForCard(string assetId)
    {
        if (!cardPictures.TryGetValue(assetId, out Sprite sprite))
            return null;
        return sprite;
    }

    public Sprite GetFrameForRarity(Rarity rarity)
    {
        var index = (int)rarity;
        if (index >= 0 && index < frames.Length)
            return frames[index];
        
        return null;
    }

    public Sprite GetMaskForRarity(Rarity rarity)
    {
        var index = (int)rarity;
        if (index >= 0 && index < mask.Length)
            return mask[index];
        
        return null;
    }


}

public class GraphicResourcesTest: IGraphicResources
{
    private readonly Sprite[] frames;
    private readonly Sprite[] mask;

    Dictionary<string, Sprite> cardPictures;

    public GraphicResourcesTest(Sprite[] pictures, Sprite[] frames, Sprite[] mask)
    {
        this.frames = frames;
        this.mask = mask;

        cardPictures = new Dictionary<string, Sprite>();
        foreach (var p in pictures)
            cardPictures.Add(p.name, p);
    }

    public Sprite GetPictureForCard(string assetId)
    {
        if (!cardPictures.TryGetValue(assetId, out Sprite sprite))
            return null;
        return sprite;
    }

    public Sprite GetFrameForRarity(Rarity rarity)
    {
        var index = (int)rarity;
        if (index >= 0 && index < frames.Length)
            return frames[index];

        return null;
    }

    public Sprite GetMaskForRarity(Rarity rarity)
    {
        var index = (int)rarity;
        if (index >= 0 && index < mask.Length)
            return mask[index];

        return null;
    }
}