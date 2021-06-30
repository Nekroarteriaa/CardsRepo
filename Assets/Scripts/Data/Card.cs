
// A card a player can set into his deck
public class Card
{
    public string assetId; // identifier to find the find the assets
    public ulong id; // unique card identifier
    public uint level; /// the level of the card
    public uint energy; // energy consumed by placing the card
    public Rarity rarity; // card rarity
};