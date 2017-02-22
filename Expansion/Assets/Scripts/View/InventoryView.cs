using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class InventoryView
{
    private GameObject parentGo;
    private RectTransform contentContainer;

    public int Height { get; set; }
    public int Width { get; set; }
    public GameObject InventoryWindow { get; set; }
    public Inventory ViewInventory { get; set; }

    public InventoryView(GameObject parent, int width, int height, Inventory inventory)
    {
        ViewInventory = inventory;
        parentGo = parent;
        Height = height;
        Width = width;
        InventoryWindow = BuildInventoryView();
    }

    public GameObject BuildInventoryView()
    {
        //Create the main container
        GameObject inventoryGameObject = UIHelper.GetRectImageGameObject(Width, Height,
            new Color(.25f, .25f, .25f, .95f), "Inventory", parentGo.transform);
        
        var scrollRect = CreateScrollRect(inventoryGameObject);

        foreach (var item in ViewInventory.InventoryObjects.GroupBy(i => i.GetDisplayText())
        .ToDictionary(gdc => gdc.Key, gdc => gdc.ToList()))
        {
            var textObject = GetInventoryObjectContainer(item.Key + " (" + item.Value.Count + ")",
                SpriteManager.Instance.GetSpriteByName(item.Value.First().GetInventorySprite()), 100, 50);
            textObject.transform.SetParent(contentContainer.transform);
            textObject.transform.position = contentContainer.position;
        }
        return inventoryGameObject;
    }

    private GameObject GetInventoryObjectContainer(string text, Sprite sprite, int width, int height)
    {
        var container = new GameObject();
        var rect = container.AddComponent<RectTransform>();
        rect.sizeDelta = new Vector2(width, height);
        var vertLayoutGroup = rect.gameObject.AddComponent<VerticalLayoutGroup>();
        vertLayoutGroup.spacing = 2;
        vertLayoutGroup.childForceExpandHeight = false;
        vertLayoutGroup.childForceExpandWidth = false;
        vertLayoutGroup.childControlHeight = false;
        vertLayoutGroup.childControlWidth = false;

        if (text != null)
        {
            var display = new GameObject().AddComponent<Text>();
            display.horizontalOverflow = HorizontalWrapMode.Overflow;
            display.alignment = TextAnchor.UpperLeft;
            display.text = text;
            Font ArialFont = (Font)Resources.GetBuiltinResource(typeof(Font), "Arial.ttf");
            display.font = ArialFont;
            display.color = Color.white;
            display.verticalOverflow = VerticalWrapMode.Overflow;
            var layoutElement = display.gameObject.AddComponent<LayoutElement>();
            layoutElement.minHeight = 10;
            display.gameObject.transform.SetParent(container.transform);
            display.gameObject.transform.position = container.transform.position;
            display.GetComponent<RectTransform>().sizeDelta = new Vector2(width, 18);
        }
        if (sprite != null)
        {
            var image = new GameObject().AddComponent<Image>();
            image.gameObject.transform.SetParent(container.transform);
            image.gameObject.transform.position = container.transform.position;
            var imageRect = image.GetComponent<RectTransform>();
            imageRect.sizeDelta = new Vector2(32, 32);
            image.sprite = sprite;
        }
        return container;
    }  

    private ScrollRect CreateScrollRect(GameObject parent)
    {
        //Create the ScrollRect
        var scrollRect = parent.AddComponent<ScrollRect>();
        //Create vertical scrollbar
        var vertScrollbar = UIHelper.GetRectImageGameObject(Height, 20, new Color(1f, 1f, 1f, 1f), "Scroll Vert", scrollRect.transform);
        var vertScrollbarRect = vertScrollbar.GetComponent<RectTransform>();
        //Attempt to anchor the scrollbar on the right with the correct size.  I dont understand this at all but it works.
        vertScrollbarRect.anchorMin = new Vector2(1, 0);
        vertScrollbarRect.anchorMax = new Vector2(1, 1);
        vertScrollbarRect.sizeDelta = new Vector2(1f, 20f);

        var scrollbar = vertScrollbar.AddComponent<Scrollbar>();

        //Scrollbar needs a slide area, a slideHandle and some other junk.  This should be easier.
        var slideArea = new GameObject().AddComponent<RectTransform>();
        slideArea.gameObject.name = "Slide Area";
        slideArea.gameObject.transform.SetParent(vertScrollbar.transform);
        slideArea.offsetMax = new Vector2(0, 10);
        slideArea.offsetMin = new Vector2(0, -10);
        slideArea.anchorMin = new Vector2(.5f, .5f);
        slideArea.anchorMax = new Vector2(.5f, .5f);
        slideArea.sizeDelta = new Vector2(Height, 1f);
        var slideHandle = UIHelper.GetRectImageGameObject(32, 32, new Color(.1f, .1f, .1f, .95f), "Slide Handle");
        var slideHandleRect = slideHandle.GetComponent<RectTransform>();
        slideHandle.transform.SetParent(slideArea.transform);
        slideHandle.transform.position = slideArea.transform.position;
        //offsetMin bottom/left
        slideHandleRect.offsetMin = new Vector2(-10, -10);
        //offsetMax top/right
        slideHandleRect.offsetMax = new Vector2(10, 10);
        scrollbar.targetGraphic = slideHandle.GetComponent<Image>();
        scrollbar.handleRect = slideHandle.GetComponent<RectTransform>();
        scrollbar.SetDirection(Scrollbar.Direction.BottomToTop, true);
        scrollRect.verticalScrollbar = scrollbar;
        scrollRect.scrollSensitivity = 50;

        GameObject viewPort = UIHelper.GetRectImageGameObject(Width, Height,
            null, "Viewport", scrollRect.transform);
        var viewPortRect = viewPort.GetComponent<RectTransform>();
        var mask = viewPort.AddComponent<Mask>();
        mask.showMaskGraphic = false;
        var maskImage = viewPort.AddComponent<Image>();
        maskImage.sprite = SpriteManager.Instance.GetSpriteByName(Constants.MASK_SPRITE);

        var contentList = UIHelper.GetRectImageGameObject(Width, Height,
            null, "Content", viewPortRect.transform);

        //Set the content container
        contentContainer = contentList.GetComponent<RectTransform>();
        var contentHeight = ViewInventory.InventoryObjects.Count * 11.5f;
        contentContainer.position = new Vector3(0, contentHeight / 2 * -1, 0);
        contentContainer.sizeDelta = new Vector2(Width, contentHeight);

        scrollRect.viewport = viewPortRect;
        scrollRect.content = contentContainer;
        var gridLayoutGroup = contentContainer.gameObject.AddComponent<GridLayoutGroup>();
        gridLayoutGroup.cellSize = new Vector2(100, 50);
        gridLayoutGroup.spacing = new Vector2(5, 5);
        gridLayoutGroup.padding.left = 10;
        gridLayoutGroup.padding.right = 10;

        return scrollRect;
    }
}