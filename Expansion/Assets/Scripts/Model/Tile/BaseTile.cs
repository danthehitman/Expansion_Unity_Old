using System;

public abstract class BaseTile
{
    private bool hasRiver;

    public EventHandler TileDataChanged;

    public World World { get; set; }
    public int X { get; set; }
    public int Y { get; set; }

    public bool HasRiver
    {
        get
        {
            return hasRiver;
        }

        set
        {
            hasRiver = value;
            OnTileDataChanged();
        }
    }

    public BaseTile(World world, int x, int y)
    {
        World = World;
        X = x;
        Y = y;
    }

    public void RegisterForTileChanged(EventHandler onTileChangedHandler)
    {
        TileDataChanged += onTileChangedHandler;
    }

    public void UnRegisterForTileChanged(EventHandler onTileChangedHandler)
    {
        TileDataChanged -= onTileChangedHandler;
    }

    protected void OnTileDataChanged()
    {
        var handler = TileDataChanged;
        if (handler != null)
            handler(this, new EventArgs());
    }
}