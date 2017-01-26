using System;
using System.ComponentModel;

public abstract class BaseTile : INotifyPropertyChanged
{
    private TileConnection riverTileConnection;

    public EventHandler TileDataChanged;

    public event PropertyChangedEventHandler PropertyChanged;

    public World World { get; set; }
    public int X { get; set; }
    public int Y { get; set; }

    public const string RiverTileConectionName = "RiverTileConnection";
    public TileConnection RiverTileConnection
    {
        get
        {
            return riverTileConnection;
        }

        set
        {
            riverTileConnection = value;
            OnPropertyChanged(RiverTileConectionName);
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
        return World.GetTileAtDirection(X, Y, direction);
    }
}