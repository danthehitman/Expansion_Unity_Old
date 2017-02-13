using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryView
{
    private GameObject parentGo;

    public int Height { get; set; }
    public int Width { get; set; }
    public GameObject Window { get; set; }
    public Inventory ViewInventory { get; set; }

    public InventoryView(GameObject parent, int height, int width, Inventory inventory)
    {
        ViewInventory = inventory;
        parentGo = parent;
        Height = height;
        Width = width;
        Window = BuildInventoryView();
    }

    public GameObject BuildInventoryView()
    {
        GameObject window = UIHelper.GetRectImageGameObject(Width, Height, new Color(.25f, .25f, .25f, .95f), "Inventory");
        var pointerEnter = window.AddComponent<EventTrigger>();
        window.transform.parent = parentGo.transform;
        window.transform.position = parentGo.transform.position;
        var canvas = window.AddComponent<CanvasRenderer>();

        //Create the ScrollRect
        var scrollRect = window.AddComponent<ScrollRect>();

        //Create vertical scrollbar
        var vertScrollbar = UIHelper.GetRectImageGameObject(Height, 20, new Color(1f, 1f, 1f, 1f), "Scroll Vert");
        var vertScrollbarRect = vertScrollbar.GetComponent<RectTransform>();
        vertScrollbar.transform.SetParent(scrollRect.transform);
        vertScrollbar.transform.position = scrollRect.transform.position;
        vertScrollbarRect.anchorMin = new Vector2(1,0);
        vertScrollbarRect.anchorMax = new Vector2(1,1);
        vertScrollbarRect.sizeDelta = new Vector2(1f, 20f);
        var scrollbar = vertScrollbar.AddComponent<Scrollbar>();
        var slideArea = new GameObject().AddComponent<RectTransform>();
        slideArea.gameObject.name = "Slide Area";
        slideArea.gameObject.transform.SetParent(vertScrollbar.transform);
        slideArea.offsetMax = new Vector2(0, 10);
        slideArea.offsetMin = new Vector2(0, -10);
        slideArea.anchorMin = new Vector2(.5f, .5f);
        slideArea.anchorMax = new Vector2(.5f, .5f);
        slideArea.sizeDelta = new Vector2(Height, 1f);
        var slideHandle = UIHelper.GetRectImageGameObject(32, 32, new Color(0f, 0f, 0f, .95f), "Slide Handle");
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

        GameObject viewPort = new GameObject();
        viewPort.transform.SetParent(scrollRect.transform);
        viewPort.transform.position = scrollRect.transform.position;
        var viewPortRect = viewPort.AddComponent<RectTransform>();
        viewPortRect.sizeDelta = new Vector2(Width, Height);
        var mask = viewPort.AddComponent<Mask>();
        mask.showMaskGraphic = false;
        var maskImage = viewPort.AddComponent<Image>();
        maskImage.sprite = SpriteManager.Instance.GetSpriteByName(Constants.MASK_SPRITE);

        GameObject contentList = new GameObject();
        contentList.transform.SetParent(viewPortRect.transform);
        contentList.transform.position = viewPortRect.transform.position;
        var contentRect = contentList.AddComponent<RectTransform>();
        var contentHeight = 100 * 25;
        contentRect.position = new Vector3(0, contentHeight / 2 * -1, 0);
        contentRect.sizeDelta = new Vector2(Width, contentHeight);
        scrollRect.viewport = viewPortRect;
        scrollRect.content = contentRect;
        var vertLayoutGroup = contentRect.gameObject.AddComponent<VerticalLayoutGroup>();
        vertLayoutGroup.spacing = 15;
        vertLayoutGroup.childForceExpandHeight = false;
        vertLayoutGroup.childForceExpandWidth = false;
        vertLayoutGroup.padding.left = 10;
        vertLayoutGroup.padding.top = 10;
        
        for (int i = 0; i < 100; i++)
        {
            var textObject = GetTextLine("some text for inventory " + i.ToString());
            textObject.transform.SetParent(contentRect.transform);
        }
        return window;
    }

    private GameObject GetTextLine(string text)
    {
        var display = new GameObject().AddComponent<Text>();
        display.horizontalOverflow = HorizontalWrapMode.Overflow;
        display.alignment = TextAnchor.MiddleLeft;
        display.text = text;
        Font ArialFont = (Font)Resources.GetBuiltinResource(typeof(Font), "Arial.ttf");
        display.font = ArialFont;
        display.color = Color.white;
        display.verticalOverflow = VerticalWrapMode.Overflow;
        var layoutElement = display.gameObject.AddComponent<LayoutElement>();
        layoutElement.minHeight = 10;
        //display.gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(300, 15);
        return display.gameObject;
    }

    
}