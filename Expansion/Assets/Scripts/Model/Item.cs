public class Item : IInventoryObject
{
    public string Name { get; set; }
    public int Id { get; set; }

    public Item(string name, int id)
    {
        Name = name;
        Id = id;
    }
    enum ItemType
    {

    }

    public string GetDisplayText()
    {
        return string.Format("Name:{0}", Id);
    }

    public string GetInventorySprite()
    {
        return Constants.TILE_GRASSLAND;
    }
}
