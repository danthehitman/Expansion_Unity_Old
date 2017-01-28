using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using UnityEngine;

public class World : INotifyPropertyChanged
{
    private const int HOURS_IN_DAY = 24;
    private const int MINUTES_IN_DAY = 1440;
    private BaseTile[,] tiles;
    private readonly int width;
    private readonly int height;
    private int minute;

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

    public void AddMinute()
    {
        Minute++;
    }

    public void AddHour()
    {
        Minute += 60;
    }

    public void AddDay()
    {
        Minute += (Minute*60)*HOURS_IN_DAY;
    }

    public World(int width = 100, int height = 100)
    {
        this.width = width;
        this.height = height;
    }

    public void InitializeWorld()
    {
        System.Random rng = new System.Random();

        var riversCount = rng.Next(1, 11);

        //var xRange = Enumerable.Range(0, width);
        //var yRange = Enumerable.Range(0, height);

        tiles = new BaseTile[width, height];

        //foreach (int x in xRange.Shuffle(rng))
        //{
        //    foreach (int y in yRange.Shuffle(rng))
        //    {
        //        var tile = new GardenTile(this, x, y);

        //        tile.RiverTileConnection = GetRiverTileConnection(x, y, rng);
        //        tiles[x, y] = tile;
        //    }
        //}
        Dictionary<string, TileConnection> riversDictionary = new Dictionary<string, TileConnection>();
        for (int i = 0; i < riversCount; i++)
        {
            riversDictionary = GenerateRiver(riversDictionary, rng);
        }

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                var tile = new GardenTile(this, x, y);

                //tile.RiverTileConnection = GetRiverTileConnection(x, y, rng);
                var xyString = x+","+y;
                tile.RiverTileConnection = riversDictionary.ContainsKey(xyString) ? riversDictionary[xyString] : null;
                tiles[x, y] = tile;
            }
        }

        Debug.Log("World created with " + width * height + " tiles.");
    }

    private Dictionary<string,TileConnection> GenerateRiver(Dictionary<string, TileConnection> rivers, System.Random rng)
    {
        var riverX = 0;
        var riverY = 0;
        var startingSide = rng.Next(0, 5);
        var direction = 0;
        var directionChangeChance = 0.0f;
        //determine the startig position and direction.
        switch (startingSide)
        {
            case 0:
                //left side
                riverX = 0;
                riverY = rng.Next(0, height -1);
                direction = 2;
                break;
            case 1:
                //top
                riverX = rng.Next(0, width -1);
                riverY = height;
                direction = 3;
                break;
            case 2:
                //right side
                riverX = width;
                riverY = rng.Next(0, height -1);
                direction = 0;
                break;
            case 3:
                //bottom
                riverX = rng.Next(0, width -1);
                riverY = 0;
                direction = 1;
                break;
            case 4:
                //center
                riverX = width / 2 + rng.Next(0, width / 6);
                riverY = height / 2 + rng.Next(0, height / 6);
                direction = rng.Next(0, 4);
                break;
            default:
                break;
        }

        var xyKey = riverX + "," + riverY;
        //var build = true;
        var previousDirection = direction;
        var max = 300;
        int current = 0;
        bool skipAdd = false;
        TileConnection currentConnection = null;
        while (current < max)
        {
            current++;
            xyKey = riverX + "," + riverY;

            if (rng.NextDouble() <= directionChangeChance)
            {
                direction = rng.Next(0, 4);
                directionChangeChance = 0.0f;
            }
            else
            {
                if (directionChangeChance < 1f)
                    directionChangeChance += 0.05f;
                else
                    directionChangeChance = 0.0f;
            }
            if (rivers.ContainsKey(xyKey))
            {
                skipAdd = true;
                currentConnection = rivers[xyKey];
                setConnectedFrom(rivers[xyKey], previousDirection);               
            }

            switch (direction)
            {
                case 0:
                    //left
                    if (!skipAdd)
                        rivers.Add(riverX + "," + riverY, setConnectedFrom(new TileConnection() { ConnectedLeft = true }, previousDirection));
                    else
                        currentConnection.ConnectedLeft = true;
                    riverX--;
                    break;
                case 1:
                    //up
                    if (!skipAdd)
                        rivers.Add(riverX + "," + riverY, setConnectedFrom(new TileConnection() { ConnectedUp = true }, previousDirection));
                    else
                        currentConnection.ConnectedUp = true;
                    riverY++;
                    break;
                case 2:
                    //right
                    if (!skipAdd)
                        rivers.Add(riverX + "," + riverY, setConnectedFrom(new TileConnection() { ConnectedRight = true }, previousDirection));
                    else
                        currentConnection.ConnectedRight = true;
                    riverX++;
                    break;
                case 3:
                    //down
                    if (!skipAdd)
                        rivers.Add(riverX + "," + riverY, setConnectedFrom(new TileConnection() { ConnectedDown = true }, previousDirection));
                    else
                        currentConnection.ConnectedDown = true;
                    riverY--;
                    break;
            }
            //if (riverX < 0 || riverX > width || riverY < 0 || riverY > height)
            //    break;
            previousDirection = direction;
            skipAdd = false;
        }

        return rivers;
    }

    private static TileConnection setConnectedFrom(TileConnection tileConnection, int direction)
    {
        switch (direction)
        {
            case 0:
                //left
                tileConnection.ConnectedRight = true;
                break;
            case 1:
                //up
                tileConnection.ConnectedDown = true;
                break;
            case 2:
                //right
                tileConnection.ConnectedLeft = true;
                break;
            case 3:
                //down
                tileConnection.ConnectedUp = true;
                break;
        }
        return tileConnection;
    }

    private TileConnection GetRiverTileConnection(int x, int y, System.Random rng)
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


        //If we have any connections around us
        if (riverUpConnection || riverDownConnection || riverLeftConnection || riverRightConnection)
        {
            //If so we will connect to at least one of them some percentage of the time.
            if (rng.NextDouble() <= 1f)
            {
                //connectUp = riverUpConnection;
                //connectDown = riverDownConnection;
                //connectLeft = riverLeftConnection;
                //connectRight = riverRightConnection;

                //if (!riverUp || !riverDown || !riverLeft || !riverRight)
                //{
                var connectionCount = 0;
                var randomRange = Enumerable.Range(0, 4);
                //If we are continuing on connect to the first one you find always.
                var connectPercent = 1f;
                //Connect to the second one less than half the time and then no more.
                var reducePercent = 9f;
                foreach (int i in randomRange.Shuffle(rng))
                {
                    if (i == 0 && riverUpConnection && rng.NextDouble() <= connectPercent)
                    {
                        connectUp = true;
                        connectionCount++;
                        connectPercent -= reducePercent;
                    }
                    else if (i == 1 && riverDownConnection && rng.NextDouble() <= connectPercent)
                    {
                        connectDown = true;
                        connectionCount++;
                        connectPercent -= reducePercent;
                    }
                    else if (i == 2 && riverLeftConnection && rng.NextDouble() <= connectPercent)
                    {
                        connectLeft = true;
                        connectionCount++;
                        connectPercent -= reducePercent;
                    }
                    else if (i == 3 && riverRightConnection && rng.NextDouble() <= connectPercent)
                    {
                        connectRight = true;
                        connectionCount++;
                        connectPercent -= reducePercent;
                    }
                }

                //If there is at least 1 ungenerated tile around us we may connect to it.
                if (tileUp == null || tileDown == null || tileLeft == null || tileRight == null)
                {
                    //Continue the river some precentage of the time.
                    randomRange = Enumerable.Range(0, 4);

                    //If we already have more than 1 connection we lower the chance of another.
                    if (connectionCount < 4)
                        connectPercent = 1f;
                    else
                        connectPercent = .3f;

                    //Make 1 or two more depending on how many we have to start with.  This should eliminate cross rivers.
                    reducePercent = .9f;
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
        //If there are no rivers around us we will spawn one some percentage of the time.
        else if ((!riverUp && !riverDown && !riverLeft && !riverRight) && rng.NextDouble() <= .02f)
        {
            hasRiver = true;
            //if ()
            //{
                //Always continue the river and sometimes add an additional connection.
                var randomRange = Enumerable.Range(1, 4);
                var connectPercent = 1f;
                var reducePercent = 0.5f;
                foreach (int i in randomRange.Shuffle(rng))
                {
                    if (i == 1 && tileUp == null && rng.NextDouble() <= connectPercent)
                    {
                        connectUp = true;
                        connectPercent -= reducePercent;
                    }
                    else if (i == 2 && tileDown == null && rng.NextDouble() <= connectPercent)
                    {
                        connectDown = true;
                        connectPercent -= reducePercent;
                    }
                    else if (i == 3 && tileLeft == null && rng.NextDouble() <= connectPercent)
                    {
                        connectLeft = true;
                        connectPercent -= reducePercent;
                    }
                    else if (i == 4 && tileRight == null && rng.NextDouble() <= connectPercent)
                    {
                        connectRight = true;
                        connectPercent -= reducePercent;
                    }
                }
            //}
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
