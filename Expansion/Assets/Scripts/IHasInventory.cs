using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHasInventory
{
    Inventory GetInventory();
    void AddItemToInventory(Item item);
    void AddMaterialToInventory(Material material);
}
