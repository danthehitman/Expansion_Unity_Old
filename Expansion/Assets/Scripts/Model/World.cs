using System.Collections.Generic;
using UnityEngine;

public class World
{
    private BaseTile[,] tiles;
    private int width;
    private int height;

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

    public World(int width = 100, int height = 100)
    {
        this.width = width;
        this.height = height;
    }

    public void InitializeWorld()
    {
        tiles = new BaseTile[width, height];

        List<string> riverTiles = new List<string>();
        riverTiles.Add("0,0");
        riverTiles.Add("0,1");
        riverTiles.Add("0,2");
        riverTiles.Add("1,0");
        riverTiles.Add("2,0");
        riverTiles.Add("2,1");
        riverTiles.Add("2,2");
        riverTiles.Add("1,2");

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                var tile = new GardenTile(this, x, y);
                //tile.HasFence = true;
                //tile.IsIrrigated = true;
                tiles[x, y] = tile;
                if (riverTiles.Contains(x + "," + y))
                {
                    tile.HasRiver = true;
                }
            }
        }

        Debug.Log("World created with " + width * height + " tiles.");
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
