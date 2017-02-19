using UnityEngine;

public class TileCacheView
{
    private GameObject parentGo;

    public int Height { get; set; }
    public int Width { get; set; }
    public GameObject Window { get; set; }
    public TileCache Cache { get; set; }

    public TileCacheView(GameObject parent, int width, int height, TileCache cache)
    {
        Height = height;
        Width = width;
        parentGo = parent;
        Cache = cache;
        BuildWindow();
    }

    private void BuildWindow()
    {
        //var windowGameObject = UIHelper.GetRectImageGameObject(Width, Height,
        //    new Color(.25f, .25f, .25f, .95f), "CacheView", parentGo.transform);
        //var pointerEnter = windowGameObject.AddComponent<EventTrigger>();

        //var layoutGroup = windowGameObject.AddComponent<VerticalLayoutGroup>();

        ////Build window header
        //var headerGo = UIHelper.GetRectImageGameObject(Width, 40,
        //    new Color(0f, 0f, 0f, .95f), "Header", windowGameObject.transform);

        //var textGo = UIHelper.GetRectTextGameObject(Width, 40, Color.white, "Tile Cache",
        //    "HeaderText", headerGo.transform);

        //Build the inventory panel
        var inventoryGo = new InventoryView(parentGo, Width, Height, Cache.CacheInventory);

        //return windowGameObject;
    }
}
