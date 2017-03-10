public class Item : IInventoryObject
{
    public string Name { get; set; }
    public string DisplayText { get; set; }
    public string InventorySpriteName { get; set; }
    public float Quality { get; set; }

    public Item(string name)
    {
        Name = name;
    }

    public string GetDisplayText()
    {
        if (DisplayText == null)
            return Name;
        return DisplayText;
    }

    public string GetInventorySprite()
    {
        return Constants.TILE_GRASSLAND;
    }
}
