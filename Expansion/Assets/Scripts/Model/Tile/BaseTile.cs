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
        World = world;
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

    public void OnNeighborTileDataChanged()
    {
        OnTileDataChanged();
    }

    public BaseTile GetTileAtDirection(TileDirectionEnum direction)
    {
        switch (direction)
        {
            case TileDirectionEnum.Left:
                return World.GetTileAt(X - 1, Y);
            case TileDirectionEnum.Right:
                return World.GetTileAt(X + 1, Y);
            case TileDirectionEnum.Up:
                return World.GetTileAt(X, Y + 1);
            case TileDirectionEnum.Down:
                return World.GetTileAt(X, Y - 1);
            default:
                return null;
        }
    }
}