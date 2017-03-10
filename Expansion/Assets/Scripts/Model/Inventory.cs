using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

public class Inventory : INotifyPropertyChanged
{
    private List<Item> items;
    private List<Resource> materials;

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
    public List<Resource> Materials
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

    public void AddItem(Item item)
    {
        Items.Add(item);
        OnPropertyChanged(ItemsPropertyName);
        OnPropertyChanged(InventoryObjectsPropertyName);
    }

    public void AddMaterial(Resource material)
    {
        Materials.Add(material);
        OnPropertyChanged(MaterialsPropertyName);
        OnPropertyChanged(InventoryObjectsPropertyName);
    }

    public const string InventoryObjectsPropertyName = "InventoryObjects";
    public List<IInventoryObject> InventoryObjects
    {
        get
        {
            return Items.Cast<IInventoryObject>().Concat(Materials.Cast<IInventoryObject>()).ToList();
        }
    }

    public Inventory()
    {
        items = new List<Item>();
        materials = new List<Resource>();
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

    public IInventoryObject GetInventoryObjectOfType(Type type)
    {
        return InventoryObjects.FirstOrDefault(e => e.GetType() == type);
    }
}
