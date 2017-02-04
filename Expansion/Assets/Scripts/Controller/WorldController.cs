using System.Collections.Generic;
using UnityEngine;

public class WorldController : MonoBehaviour
{
    public float UpdateInterval = .1f;
    public int WorldSpeed = 1;
    private double lastInterval;
    private int frames = 0;
    private float fps;

    public static WorldController Instance;
    public PlayerController PlayerController;

    //Properties
    public int Width = 100;
    public int Height = 100;
    public int Key = 1;

    //References
    public World World;
    private TileView[,] tileViews;

    //State Stuff
    private TileView HighlightedTile = null;
    private TileView ActivatedTile = null;

    // Initialize the game world.
    void Start ()
    {
        lastInterval = Time.realtimeSinceStartup;
        frames = 0;

        //Hacky singleton within monobehaviour.  Not sure I love this.
        Instance = this;

        World = new World(Width, Height, Key);
        World.InitializeWorldComplex();

        var startTile = World.GetRandomTile();

        var playerEntity = new PlayerEntity();
        playerEntity.EntityToTile(startTile);

        CenterCameraOnTile(startTile);

        PlayerController = new PlayerController(playerEntity, World);

        tileViews = new TileView[Width, Height];

        //Loop through all of the tiles in the world and create view tiles for them.
        for (int x = 0; x < World.Width; x++)
        {
            for (int y = 0; y < World.Height; y++)
            {
                var tileData = World.GetTileAt(x, y);
                SetTileViewAsWorldChild(CreateTileView(tileData));
            }
        }
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
        //If we have a highlighted tile remove the highlight.
        if (HighlightedTile != null)
            HighlightedTile.IsHighlighted = false;
        //Try to get a tile view at the coordinates and if we find one we highlight it.
        HighlightedTile = GetTileViewAt(newX, newY);
        if (HighlightedTile != null)
            HighlightedTile.IsHighlighted = true;
    }

    public void OnWorldCoordinateActivated(int X, int Y)
    {
        var currentActiveTile = ActivatedTile;
        //Deactivate the current active tile if ther is one.  Either we are clicking on an already active tile or a new active tile.
        if (ActivatedTile != null)
            ActivatedTile.IsActivated = false;
        //Get the new active tile.
        ActivatedTile = GetTileViewAt(X, Y);
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
                OnMouseOverWorldCoordinateChanged(X, Y);
            }
        }
    }

    public void OnWorldCoordinateDoubleClick(int X, int Y)
    {
        //Test code for both double click and testing the events on the models.
        //var doubleClickTile = World.GetTileAt(X, Y);
        //((GardenTile)doubleClickTile).HasRiver = !((GardenTile)doubleClickTile).HasRiver;
    }

    public void OnMovementKeyPressed(List<TileDirectionEnum> directions)
    {
        if (directions.Count > 0)
            PlayerController.MovePlayer(directions);
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
