public class TileStore : IHasInventory
{
    private Inventory inventory;

    public void AddItemToInventory(Item item)
    {
        inventory.Items.Add(item);
    }

    public void AddMaterialToInventory(Material material)
    {
        inventory.Materials.Add(material);
    }

    public Inventory GetInventory()
    {
        return inventory;
    }
}
