using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TileCacheView
{
    private GameObject parentGo;

    public int Height { get; set; }
    public int Width { get; set; }
    public GameObject Window { get; set; }
    public Inventory CacheInventory { get; set; }

    public TileCacheView(GameObject parent, int width, int height, Inventory inventory)
    {
        Height = height;
        Width = width;
        parentGo = parent;
        CacheInventory = inventory;
        Window = BuildWindow();
    }

    private GameObject BuildWindow()
    {
        GameObject windowGameObject = UIHelper.GetRectImageGameObject(Width, Height,
            new Color(.25f, .25f, .25f, .95f), "CacheView", parentGo.transform);
        var pointerEnter = windowGameObject.AddComponent<EventTrigger>();

        var layoutGroup = windowGameObject.AddComponent<VerticalLayoutGroup>();

        //Build window header
        GameObject headerGo = UIHelper.GetRectImageGameObject(Width, 20,
            new Color(0f, 0f, 0f, .95f), "Header", windowGameObject.transform);

        var inventoryGo = new InventoryView(windowGameObject, Width, Height -20, CacheInventory);

        return windowGameObject;
    }
}
