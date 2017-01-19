using UnityEngine;

public class WorldController : MonoBehaviour
{
    public static WorldController Instance;

    public int Width = 100;
    public int Height = 100;

    World World;

    private BaseTile HighlightedTile = null;
    private BaseTile ActivatedTile = null;

    // Use this for initialization
    void Start () {
        //Hacky singleton within monobehaviour.  Not sure I love this.
        Instance = this;

        World = new World(Width, Height);

        for (int x = 0; x < World.Width; x++)
        {
            for (int y = 0; y < World.Height; y++)
            {
                var tileData = World.GetTileAt(x, y);
                SetViewAsWorldChild(CreateTileView(tileData));
            }
        }

        for (int x = 0; x < World.Width; x++)
        {
            for (int y = 0; y < World.Height; y++)
            {
                var tileData = World.GetTileAt(x, y);
                SetViewAsWorldChild(CreateTileView(tileData));
            }
        }
    }

    private TileView CreateTileView(BaseTile tileArg)
    {
        TileView tileView = null;
        if (tileArg is GardenTile)
        {
            tileView = new GardenView((GardenTile)tileArg);
        }

        tileArg.RegisterForTileDisposed(() =>
        {
            OnTileDisposed(tileView);
        });

        return tileView;
    }
    
    // Update is called once per frame
    void Update () {
        
    }

    private void SetViewAsWorldChild(TileView view)
    {
        view.BaseLayer.transform.SetParent(this.transform);
    }

    private void OnTileDisposed(TileView tileView)
    {
        Destroy(tileView.BaseLayer);
    }

    public void OnMouseOverWorldCoordinateChanged(int newX, int newY)
    {
        if (HighlightedTile != null)
            HighlightedTile.SetHighlighted(false);
        HighlightedTile = World.GetTileAt(newX, newY);
        if (HighlightedTile != null)
            HighlightedTile.SetHighlighted(true);
    }

    public void OnWorldCoordinateActivated(int X, int Y)
    {
        var currentActiveTile = ActivatedTile;
        //Deactivate the current active tile if ther is one.  Either we are clicking on an already active tile or a new active tile.
        if (ActivatedTile != null)
            ActivatedTile.SetActive(false);
        //Get the new active tile.
        ActivatedTile = World.GetTileAt(X, Y);
        if (ActivatedTile != null)
        {
            //If this is not the same tile activate the new one.
            if (currentActiveTile != ActivatedTile)
            {
                ActivatedTile.SetActive(true);
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
        doubleClickTile.TileDataChanged(doubleClickTile);
    }
}
