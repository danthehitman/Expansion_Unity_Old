using UnityEngine;

public class WorldController : MonoBehaviour
{
    public static WorldController Instance;

    //Properties
    public int Width = 100;
    public int Height = 100;

    //References
    private World World;
    private TileView[,] tileViews;

    //State Stuff
    private TileView HighlightedTile = null;
    private TileView ActivatedTile = null;

    void Start ()
    {
        //Hacky singleton within monobehaviour.  Not sure I love this.
        Instance = this;

        World = new World(Width, Height);
        tileViews = new TileView[Width, Height];


        //TODO: World initialization will need to be done elsewhere and differently.
        for (int x = 0; x < World.Width; x++)
        {
            for (int y = 0; y < World.Height; y++)
            {
                var tileData = World.GetTileAt(x, y);
                SetViewAsWorldChild(CreateTileView(tileData));
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    private TileView CreateTileView(BaseTile tileArg)
    {
        TileView tileView = null;
        if (tileArg is GardenTile)
        {
            tileView = new GardenTileView((GardenTile)tileArg);
        }

        tileViews[tileArg.X, tileArg.Y] = tileView;
        return tileView;
    }

    private void DestroyTileViewAt(int x, int y)
    {
        var tileView = GetTileViewAt(x, y);
        if (tileView != null)
            Destroy(tileView.BaseLayer);
    }

    private void SetViewAsWorldChild(TileView view)
    {
        view.BaseLayer.transform.SetParent(this.transform);
    }

    public void OnMouseOverWorldCoordinateChanged(int newX, int newY)
    {
        if (HighlightedTile != null)
            HighlightedTile.IsHighlighted = false;
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
        var doubleClickTile = World.GetTileAt(X, Y);
        ((GardenTile)doubleClickTile).IsIrrigated = false;
    }

    private TileView GetTileViewAt(int x, int y)
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
}
