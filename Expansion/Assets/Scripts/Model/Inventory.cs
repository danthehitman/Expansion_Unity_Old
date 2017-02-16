using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

public class Inventory : INotifyPropertyChanged
{
    private List<Item> items;
    private List<Material> materials;
    private int size;

    public const string ItemsPropertyName = "Items";
    public List<Item> Items
    {
        get
        {
            return items;
        }

        set
        {
            items = value;
            OnPropertyChanged(ItemsPropertyName);
            OnPropertyChanged(InventoryObjectsPropertyName);
        }
    }

    public const string MaterialsPropertyName = "Materials";
    public List<Material> Materials
    {
        get
        {
            return materials;
        }

        set
        {
            materials = value;
            OnPropertyChanged(MaterialsPropertyName);
            OnPropertyChanged(InventoryObjectsPropertyName);
        }
    }

    public bool AddItem(Item item)
    {
        if (Items.Count + Materials.Count < size)
        {
            Items.Add(item);
            OnPropertyChanged(ItemsPropertyName);
            OnPropertyChanged(InventoryObjectsPropertyName);
            return true;
        }
        return false;
    }

    public bool AddMaterial(Material material)
    {
        if (Items.Count + Materials.Count < size)
        {
            Materials.Add(material);
            OnPropertyChanged(MaterialsPropertyName);
            OnPropertyChanged(InventoryObjectsPropertyName);
            return true;
        }
        return false;
    }

    public const string InventoryObjectsPropertyName = "InventoryObjects";
    public List<IInventoryObject> InventoryObjects
    {
        get
        {
            return Items.Cast<IInventoryObject>().Concat(Materials.Cast<IInventoryObject>()).ToList();
        }
    }

    public Inventory(int sizeArg)
    {
        items = new List<Item>();
        materials = new List<Material>();
        size = sizeArg;
    }

    public event PropertyChangedEventHandler PropertyChanged;

    protected void OnPropertyChanged(PropertyChangedEventArgs e)
    {
        PropertyChangedEventHandler handler = PropertyChanged;
        if (handler != null)
            handler(this, e);
    }

    protected void OnPropertyChanged(string propertyName)
    {
        OnPropertyChanged(new PropertyChangedEventArgs(propertyName));
    }
}
