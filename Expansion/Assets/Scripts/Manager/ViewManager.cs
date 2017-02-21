using System;
using System.Collections.Generic;
using System.Linq;
using RektTransform;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ViewManager
{
    private static ViewManager instance = null;

    public GameObject UIOverlay = null;
    public ContextMenuView ContextView = null;
    public GameObject MainWindow = null;
    private GameObject WindowContent = null;

    public Action<PointerEventData> MainDialogEnter;
    public Action<PointerEventData> MainDialogExit;
    public Action MainDialogClosed;

    private ViewManager()
    {
        UIOverlay = GameObject.Find("WorldController/UIOverlay").gameObject;
        ContextView = new ContextMenuView(UIOverlay);
        BuildMainWindow();
        MainWindow.SetActive(false);
    }

    public static ViewManager Instance
    {
        get
        {
            if (instance == null)
                instance = new ViewManager();
            return instance;
        }
    }

    protected virtual void OnMainDialogEnter(PointerEventData e)
    {
        var handler = MainDialogEnter;
        if (handler != null)
        {
            handler(e);
        }
    }

    protected virtual void OnMainDialogExit(PointerEventData e)
    {
        var handler = MainDialogExit;
        if (handler != null)
        {
            handler(e);
        }
    }

    protected virtual void OnMainDialogClose()
    {
        MainDialogClosed();
    }

    public void ShowTileContextMenuForEntity(BaseEntity entity, TileView tile)
    {
        var position = Camera.main.WorldToScreenPoint(new Vector3(tile.BaseTile.X, tile.BaseTile.Y, 0));
        ContextView.ShowMenu(GetTileActions(entity, tile.BaseTile).ToList(), position);
    }

    public IEnumerable<ContextAction> GetTileActions(BaseEntity entity, BaseTile tile)
    {
        IEnumerable<ContextAction> result = null;
        if (entity is HumanEntity)
            result = GetActionsForEntity(entity, tile);
        return result;
    }

    public List<ContextAction> GetActionsForEntity(BaseEntity entity, BaseTile tile)
    {
        var actions = new List<ContextAction>()
        {
            new ContextAction("Explore", () => { CreateExploreTileJob(entity, tile); }),
            new ContextAction("View Tile Cache", () => { ShowTileCacheView(tile); })
        };

        return actions;
    }

    private void CreateExploreTileJob(BaseEntity entity, BaseTile tile)
    {
        if (entity != null)
        {
            entity.QueueJob(new Job()
            {
                Position = new Vector3(tile.X, tile.Y, 0),
                JobAction = tile.ExploreTile
            });
        }
    }

    internal void HideContextMenu()
    {
        ContextView.HideMenu();
    }

    public void BuildMainWindow()
    {
        MainWindow = UIHelper.GetRectImageGameObject(100, 100,
            new Color(.25f, .25f, .25f, .95f), "MainWindow", UIOverlay.transform);

        var vertLayout = MainWindow.AddComponent<VerticalLayoutGroup>();
        vertLayout.childControlHeight = false;
        vertLayout.childControlWidth = true;
        vertLayout.childForceExpandHeight = true;
        vertLayout.childForceExpandWidth = false;
        vertLayout.childAlignment = TextAnchor.UpperCenter;

        //Build window header
        var headerGo = UIHelper.GetRectImageGameObject(100, 40,
            new Color(0f, 0f, 0f, .95f), "Header", MainWindow.transform);
        var headerRect = headerGo.GetComponent<RectTransform>();

        var textGo = UIHelper.GetRectTextGameObject(100, 40, Color.white, "Header Text",
            "HeaderText", headerGo.transform);
        var textRect = textGo.GetComponent<RectTransform>();
        textRect.Left(0f);
        textRect.Right(50f);

        var closeButton = UIHelper.CreateButton("CloseButton", headerGo.transform, "Close", HideMainWindow, 60, 40);
        var closeRect = closeButton.GetComponent<RectTransform>();
        closeRect.anchorMin = new Vector2(1f, .5f);
        closeRect.anchorMax = new Vector2(1f, .5f);
        closeRect.pivot = new Vector2(1f, .5f);


        WindowContent = UIHelper.GetRectImageGameObject(100, 60, null, "WindowContent", MainWindow.transform);
        var contentRect = WindowContent.GetComponent<RectTransform>();
        contentRect.anchorMin = new Vector2(0, 0);
        contentRect.anchorMax = new Vector2(1, 1);
        contentRect.offsetMin = contentRect.offsetMax = new Vector2(0, 0);

        var trigger = MainWindow.AddComponent<EventTrigger>();
        var entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerEnter;
        entry.callback.AddListener((data) => { OnMainDialogEnter((PointerEventData)data); });
        trigger.triggers.Add(entry);
        var exit = new EventTrigger.Entry();
        exit.eventID = EventTriggerType.PointerExit;
        exit.callback.AddListener((data) => { OnMainDialogExit((PointerEventData)data); });
        trigger.triggers.Add(exit);
    }

    public void ShowTileCacheView(BaseTile tile)
    {
        var inventory = new Inventory();
        for (int i = 0; i < 200; i++)
        {
            inventory.AddItem(new Item("Some item " + i));
        }
        //tile.Cache = new TileCache() { CacheInventory = inventory };
        
        TileCacheView iv = new TileCacheView(WindowContent, 600, 560, tile.Cache);
        ShowMainWindow(600, 600, "Tile Cache");
    }

    public void ShowMainWindow(int width, int height, string text)
    {
        var headerText = MainWindow.transform.Find("Header/HeaderText").GetComponent<Text>();
        headerText.text = text;
        MainWindow.SetActive(true);
        MainWindow.GetComponent<RectTransform>().sizeDelta = new Vector2(width, height);
    }

    public void HideMainWindow()
    {
        foreach (Transform child in WindowContent.transform)
            GameObject.Destroy(child.gameObject);
        MainWindow.SetActive(false);
        OnMainDialogClose();
    }
}
