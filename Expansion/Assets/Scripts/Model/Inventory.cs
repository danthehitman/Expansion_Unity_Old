using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class Inventory : INotifyPropertyChanged
{
    private List<Item> items;
    private List<Material> materials;

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
        }
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
