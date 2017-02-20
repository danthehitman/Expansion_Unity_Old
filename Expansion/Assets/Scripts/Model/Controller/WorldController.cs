using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class WorldController : MonoBehaviour
{
    public float UpdateInterval = .1f;
    public int WorldSpeed = 1;
    private double lastInterval;
    private int frames = 0;
    private float fps;
    private TileInfoView tileInfo;

    public static WorldController Instance;
    public EntityController EntityController;

    //Properties
    public int Width = 100;
    public int Height = 100;
    public int Key = 1;

    public int RevealedWidth = 5;
    public int RevealedHeight = 5;

    public bool SuspendWorldMouseInteraction = false;

    //References
    public World World;
    private TileView[,] tileViews;

    //State Stuff
    private TileView HighlightedTile = null;
    private TileView ActivatedTile = null;

    private ViewManager viewMan = null;

    // Initialize the game world.
    void Start ()
    {
        viewMan = ViewManager.Instance;
        viewMan.MainDialogEnter += OnPointerEnterWindow;
        viewMan.MainDialogExit += OnPointerExitWindow;
        viewMan.MainDialogClosed += OnMainMenuClosed;

        tileInfo = GetComponent<TileInfoView>();

        lastInterval = Time.realtimeSinceStartup;
        frames = 0;

        //Hacky singleton within monobehaviour.  Not sure I love this.
        Instance = this;

        World = new World(Width, Height, Key);
        World.RevealedHeight = RevealedHeight;
        World.RevealedWidth = RevealedWidth;

        World.InitializeWorldComplex();

        //Find a random start tile.
        var startTile = World.GetRandomLandTile();

        tileViews = new TileView[Width, Height];
        
        //Make sure we can draw a revealed width/height from the start tile or go the other direction.
        var startX = startTile.X;
        var startY = startTile.Y;
        //if (startX + RevealedWidth > Width || startY + RevealedHeight > Height)
        //{
        //    if (startX + RevealedWidth > Width)
        //        startX = startX - RevealedWidth;
        //    if (startY + RevealedHeight > Height)
        //        startY = startY - RevealedHeight;
        //    //If we had to change the start coords change the start tile.
        //    startTile = World.GetTileAt(startX, startY);
        //}

        Debug.Log("StartX: " + startX + " StartY: " + startY + "startTile: " + startTile.X + "," + startTile.Y);
        
        var playerEntity = new HumanEntity();
        playerEntity.MoveEntityToTile(startTile);

        CenterCameraOnTile(startTile);

        EntityController = new EntityController(playerEntity, World);

        //Loop through all of the tiles in the world and create view tiles for them.
        //for (int x = startX; x < startX + RevealedWidth; x++)
        //{
        //    for (int y = startY; y < startY + RevealedHeight; y++)
        //    {
        //        var tileData = World.GetTileAt(x, y);
        //        SetTileViewAsWorldChild(CreateTileView(tileData));
        //    }
        //}
        for (int x = 0; x < Width; x++)
        {
            for (int y = 0; y < Height; y++)
            {
                var tileData = World.GetTileAt(x, y);
                SetTileViewAsWorldChild(CreateTileView(tileData));
            }
        }
    }

    public void OnPointerEnterWindow(PointerEventData ed)
    {
        SuspendWorldMouseInteraction = true;
        Debug.Log(ed.currentInputModule.ToString());
    }

    public void OnPointerExitWindow(PointerEventData ed)
    {
        SuspendWorldMouseInteraction = false;
        Debug.Log(ed.currentInputModule.ToString());
    }

    public void OnMainMenuClosed()
    {
        SuspendWorldMouseInteraction = false;
    }

    void OnGUI()
    {
        GUILayout.Label("FPS" + fps.ToString("f2"));
        GUILayout.Label("Minutes" + World.Minute.ToString("f2"));
        GUILayout.Label("Hours" + World.Hour.ToString("f2"));
        GUILayout.Label("Days" + World.Day.ToString("f2"));
    }

    void Update()
    {
        ++frames;
        float timeNow = Time.realtimeSinceStartup;
        if (timeNow > lastInterval + UpdateInterval)
        {
            World.AddMinute(WorldSpeed);
            fps = World.Minute;
            fps = (float)(frames / (timeNow - lastInterval));
            frames = 0;
            lastInterval = timeNow;
        }

        EntityController.UpdateEntities();
    }

    private TileView CreateTileView(BaseTile tileArg)
    {
        TileView tileView = new TileView(tileArg);        

        tileViews[tileArg.X, tileArg.Y] = tileView;
        return tileView;
    }

    //private TileView CreateTileView(BaseTile tileArg)
    //{
    //    TileView tileView = null;
    //    if (tileArg is GardenTile)
    //    {
    //        tileView = new GardenTileView((GardenTile)tileArg);
    //    }
    //    else if (tileArg is DesertTile)
    //    {
    //        tileView = new DesertTileView((DesertTile)tileArg);
    //    }
    //    else if (tileArg is MountainTile)
    //    {
    //        tileView = new MountainTileView((MountainTile)tileArg);
    //    }

    //    tileViews[tileArg.X, tileArg.Y] = tileView;
    //    return tileView;
    //}

    private void DestroyTileViewAt(int x, int y)
    {
        var tileView = GetTileViewAt(x, y);
        if (tileView != null)
            Destroy(tileView.HighlightLayer);
    }

    private void SetTileViewAsWorldChild(TileView view)
    {
        view.HighlightLayer.transform.SetParent(this.transform);
    }

    public void OnMouseOverWorldCoordinateChanged(int newX, int newY)
    {
        if (!SuspendWorldMouseInteraction)
        {
            //If we have a highlighted tile remove the highlight.
            if (HighlightedTile != null)
                HighlightedTile.IsHighlighted = false;
            //Try to get a tile view at the coordinates and if we find one we highlight it.
            HighlightedTile = GetTileViewAt(newX, newY);
            if (HighlightedTile != null)
                HighlightedTile.IsHighlighted = true;
            HighlightedTileChanged(HighlightedTile == null ? null : HighlightedTile.BaseTile);
        }
    }

    private void HighlightedTileChanged(BaseTile tile)
    {
        if (tileInfo != null)
            tileInfo.UpdateInfo(tile);
    }

    public void OnWorldCoordinateDoubleClick(int x, int y)
    {
        if (!SuspendWorldMouseInteraction)
        {
            var currentActiveTile = ActivatedTile;
            //Deactivate the current active tile if there is one.  Either we are clicking on an already active tile or a new active tile.
            if (ActivatedTile != null)
                ActivatedTile.IsActivated = false;
            //Get the new active tile.
            ActivatedTile = GetTileViewAt(x, y);
            if (ActivatedTile != null)
            {
                //If this is not the same tile activate the new one.
                if (currentActiveTile != ActivatedTile)
                {
                    ActivatedTile.IsActivated = true;
                }
                //If it is the same tile we already deactivated it so now we just need to call highlight on this tile.
                else
                {
                    OnMouseOverWorldCoordinateChanged(x, y);
                }
                //EntityController.ActiveEntity.QueueJob(
                //    new Job()
                //    {
                //        Position = new Vector3(ActivatedTile.BaseTile.X, ActivatedTile.BaseTile.Y, 0),

                //    });
            }

            //if (ActivatedTile == null || ActivatedTile.BaseTile != tileContextMenu.Tile)
            if (ActivatedTile == null)
                HideTileMenu();
        }
    }

    public void OnWorldCoordinateActivated(int x, int y)
    {
        //Test code for both double click and testing the events on the models.
        //var doubleClickTile = World.GetTileAt(X, Y);
        //((GardenTile)doubleClickTile).HasRiver = !((GardenTile)doubleClickTile).HasRiver;
    }

    public void OnWorldCoordinateMenuClick(int x, int y)
    {
        if (!SuspendWorldMouseInteraction)
        {
            if (HighlightedTile != null && HighlightedTile.BaseTile.X == x && HighlightedTile.BaseTile.Y == y)
            {
                ShowTileMenu(HighlightedTile);
            }
        }
    }

    public void OnMapPanning()
    {
        HideTileMenu();
    }

    private void ShowTileMenu(TileView tileView)
    {
        viewMan.ShowTileContextMenuForEntity(EntityController.ActiveEntity, tileView);
    }

    private void HideTileMenu()
    {
        viewMan.HideContextMenu();
    }

    public void OnMovementKeyPressed(List<TileDirectionEnum> directions)
    {
        if (!SuspendWorldMouseInteraction)
        {
            if (directions.Count > 0)
                EntityController.MoveEntity(directions);
        }
    }

    public TileView GetTileViewAt(int x, int y)
    {
        try
        {
            return tileViews[x, y];
        }
        catch
        {
            return null;
        }
    }

    public void CenterCameraOnTile(BaseTile tile)
    {
        Camera.main.transform.Translate(new Vector3(tile.X, tile.Y, Camera.main.transform.position.z));
    }
}
