using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class World : INotifyPropertyChanged
{
    private const int HOURS_IN_DAY = 24;
    private const int MINUTES_IN_DAY = 1440;
    private BaseTile[,] tiles;
    private readonly int width;
    private readonly int height;
    private int minute;
    private IEnumerable<Marker> _markers;
    private int key = 1;

    public event PropertyChangedEventHandler PropertyChanged;
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

    public int Width
    {
        get
        {
            return width;
        }
    }

    public int Height
    {
        get
        {
            return height;
        }
    }

    public int Day
    {
        get
        {
            return (Minute/60) / HOURS_IN_DAY;
        }
    }
    
    public int Month
    {
        get
        {
            return Day / 30;
        }
    }
    
    public int Year
    {
        get
        {
            return Month / 12;
        }
    }
    
    public int Hour
    {
        get
        {
            return Minute / 60;
        }
    }

    public const string MinutePropertyName = "Minute";
    public int Minute
    {
        get
        {
            return minute;
        }

        private set
        {
            minute = value;
            OnPropertyChanged(MinutePropertyName);
        }
    }

    public int GetMinuteInDay()
    {
        return Minute % MINUTES_IN_DAY;
    }

    public void AddMinute(int toAdd = 1)
    {
        Minute += toAdd;
    }

    public void AddHour()
    {
        Minute += 60;
    }

    public void AddDay()
    {
        Minute += (Minute*60)*HOURS_IN_DAY;
    }

    public World(int width = 100, int height = 100, int key = 1)
    {
        this.key = key;
        this.width = width;
        this.height = height;
    }

    public void InitializeWorldComplex()
    {
        System.Random rng = new System.Random();
        WrappingWorldGenerator worldGen = new WrappingWorldGenerator(this, height, width, this.key);
        tiles = worldGen.Tiles;
        Debug.Log("World created with " + width * height + " tiles.");
    }

    public BaseTile GetTileAtDirection(int x, int y, TileDirectionEnum direction)
    {
        switch (direction)
        {
            case TileDirectionEnum.Left:
                return GetTileAt(x- 1, y);
            case TileDirectionEnum.Right:
                return GetTileAt(x + 1, y);
            case TileDirectionEnum.Up:
                return GetTileAt(x, y + 1);
            case TileDirectionEnum.Down:
                return GetTileAt(x, y - 1);
            default:
                return null;
        }
    }

    public BaseTile GetTileAt(int x, int y)
    {
        try
        {
            return tiles[x, y];
        }
        catch
        {
            return null;
        }
    }
}
