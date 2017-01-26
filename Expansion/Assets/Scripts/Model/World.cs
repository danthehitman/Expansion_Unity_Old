﻿using System.ComponentModel;
using System.Linq;
using UnityEngine;

public class World : INotifyPropertyChanged
{
    private const int HOURS_IN_DAY = 24;
    private BaseTile[,] tiles;
    private int width;
    private int height;
    private int second;
    private int minute;
    private int hour;
    private int day;
    private int month;
    private int year;

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

    public const string DayPropertyName = "Day";
    public int Day
    {
        get
        {
            return day;
        }

        set
        {
            day = value;
            OnPropertyChanged(DayPropertyName);
        }
    }

    public const string MonthPropertyName = "Month";
    public int Month
    {
        get
        {
            return month;
        }

        set
        {
            month = value;
            OnPropertyChanged(MonthPropertyName);
        }
    }

    public const string YearPropertyName = "Year";
    public int Year
    {
        get
        {
            return year;
        }

        set
        {
            year = value;
            OnPropertyChanged(YearPropertyName);
        }
    }

    public const string SecondPropertyName = "Second";
    public int Second
    {
        get
        {
            return second;
        }

        set
        {
            second = value;
            OnPropertyChanged(SecondPropertyName);
        }
    }

    public const string HourPropertyName = "Hour";
    public int Hour
    {
        get
        {
            return hour;
        }

        set
        {
            hour = value;
            OnPropertyChanged(HourPropertyName);
        }
    }

    public const string MinutePropertyName = "Minute";
    public int Minute
    {
        get
        {
            return minute;
        }

        set
        {
            minute = value;
            OnPropertyChanged(MinutePropertyName);
        }
    }

    public void AddMinute()
    {
        if (Minute < 60 * HOURS_IN_DAY)
            Minute++;
        else
            Minute = 0;
    }

    public World(int width = 100, int height = 100)
    {
        this.width = width;
        this.height = height;
    }

    public void InitializeWorld()
    {
        tiles = new BaseTile[width, height];
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                var tile = new GardenTile(this, x, y);

                tile.RiverTileConnection = GetRiverTileConnection(x, y);
                tiles[x, y] = tile;
            }
        }

        Debug.Log("World created with " + width * height + " tiles.");
    }

    private TileConnection GetRiverTileConnection(int x, int y)
    {
        bool hasRiver = false;
        bool connectUp = false;
        bool connectDown = false; ;
        bool connectLeft = false; ;
        bool connectRight = false; ;

        var tileUp = GetTileAtDirection(x, y, TileDirectionEnum.Up);
        var tileDown = GetTileAtDirection(x, y, TileDirectionEnum.Down);
        var tileLeft = GetTileAtDirection(x, y, TileDirectionEnum.Left);
        var tileRight = GetTileAtDirection(x, y, TileDirectionEnum.Right);

        //Are there any tiles around us with rivers and do they have connections?
        var riverUpConnection = false;
        var riverDownConnection = false;
        var riverLeftConnection = false;
        var riverRightConnection = false;

        var riverUp = tileUp != null && tileUp.RiverTileConnection != null;
        if (riverUp) riverUpConnection = tileUp.RiverTileConnection.ConnectedDown;
        var riverDown = tileDown != null && tileDown.RiverTileConnection != null;
        if (riverDown) riverDownConnection = tileDown.RiverTileConnection.ConnectedUp;
        var riverLeft = tileLeft != null && tileLeft.RiverTileConnection != null;
        if (riverLeft) riverLeftConnection = tileLeft.RiverTileConnection.ConnectedRight;
        var riverRight = tileRight != null && tileRight.RiverTileConnection != null;
        if (riverRight) riverRightConnection = tileRight.RiverTileConnection.ConnectedLeft;

        if (riverUpConnection || riverDownConnection || riverLeftConnection || riverRightConnection)
        {
            //If so we will connect to them some percentage of the time.
            if (Random.value <= 0.75f)
            {
                connectUp = riverUpConnection;
                connectDown = riverDownConnection;
                connectLeft = riverLeftConnection;
                connectRight = riverRightConnection;
                if (tileUp == null || tileDown == null || tileLeft == null || tileRight == null)
                {
                    //Continue the river some precentage of the time.
                    var randomRange = Enumerable.Range(0, 4).OrderBy(r => Random.value);
                    var connectPercent = 0.75f;
                    var reducePercent = 0.2f;
                    foreach (int i in randomRange)
                    {
                        if (i == 0 && tileUp == null && Random.value <= connectPercent)
                        {
                            connectUp = true;
                            connectPercent -= reducePercent;
                        }
                        else if (i == 1 && tileDown == null && Random.value <= connectPercent)
                        {
                            connectDown = true;
                            connectPercent -= reducePercent;
                        }
                        else if (i == 2 && tileLeft == null && Random.value <= connectPercent)
                        {
                            connectLeft = true;
                            connectPercent -= reducePercent;
                        }
                        else if (i == 3 && tileRight == null && Random.value <= connectPercent)
                        {
                            connectRight = true;
                            connectPercent -= reducePercent;
                        }
                    }
                }
                hasRiver = true;
            }
        }
        //I there are no tiles around us with connections we will create a new river with connections some percentage of the time.
        else if (Random.value <= 0.05f)
        {
            hasRiver = true;
            if (tileUp == null || tileDown == null || tileLeft == null || tileRight == null)
            {
                //Continue the river some precentage of the time.
                var randomRange = Enumerable.Range(0, 4).OrderBy(r => Random.value);
                var connectPercent = 0.75f;
                var reducePercent = 0.2f;
                foreach (int i in randomRange)
                {
                    if (i == 0 && tileUp == null && Random.value <= connectPercent)
                    {
                        connectUp = true;
                        connectPercent -= reducePercent;
                    }
                    else if (i == 1 && tileDown == null && Random.value <= connectPercent)
                    {
                        connectDown = true;
                        connectPercent -= reducePercent;
                    }
                    else if (i == 2 && tileLeft == null && Random.value <= connectPercent)
                    {
                        connectLeft = true;
                        connectPercent -= reducePercent;
                    }
                    else if (i == 3 && tileRight == null && Random.value <= connectPercent)
                    {
                        connectRight = true;
                        connectPercent -= reducePercent;
                    }
                }
            }
        }
        return hasRiver == false
            ? null
            : new TileConnection()
            {
                ConnectedUp = connectUp,
                ConnectedLeft = connectLeft,
                ConnectedDown = connectDown,
                ConnectedRight = connectRight
            };
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
