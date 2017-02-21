public class Resource : IInventoryObject
{    public enum ResourceSize
    {
        Small,
        Medium,
        Large
    }

    public string Name { get; set; }
    public string Description { get; set; }
    public string DisplayText { get; set; }
    public string InventorySpriteName { get; set; }

    public Resource(string name)
    {
        Name = name;
    }
    
    public Resource(string name, string displayText, string description, string inventorySprite)
    {
        Name = name;
        DisplayText = displayText;
        InventorySpriteName = inventorySprite;
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
