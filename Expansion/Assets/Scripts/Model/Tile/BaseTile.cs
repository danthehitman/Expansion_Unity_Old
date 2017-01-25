using System;
using System.ComponentModel;

public abstract class BaseTile : INotifyPropertyChanged
{
    private bool hasRiver;

    public EventHandler TileDataChanged;

    public event PropertyChangedEventHandler PropertyChanged;

    public World World { get; set; }
    public int X { get; set; }
    public int Y { get; set; }

    public const string HasRiverPropertyName = "HasRiver";
    public bool HasRiver
    {
        get
        {
            return hasRiver;
        }

        set
        {
            hasRiver = value;
            OnPropertyChanged(HasRiverPropertyName);
        }
    }

    public BaseTile(World world, int x, int y)
    {
        World = world;
        X = x;
        Y = y;
    }

    protected void OnPropertyChanged(PropertyChangedEventArgs e)
    {
        PropertyChangedEventHandler handler = PropertyChanged;
        if (handler != null)
            handler(this, e);
    }

    protected void OnPropertyChanged(string propertyName)
    {
        OnPropertyChanged(new PropertyChangedEventArgs(propertyName));
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