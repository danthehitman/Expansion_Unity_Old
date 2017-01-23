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

        tiles = new BaseTile[width, height];

        List<int[]> riverTiles = new List<int[]>();
        riverTiles.Add(new int[] { 0, 0 });
        riverTiles.Add(new int[] { 0, 1 });
        riverTiles.Add(new int[] { 0, 2 });
        riverTiles.Add(new int[] { 1, 0 });
        riverTiles.Add(new int[] { 2, 0 });
        riverTiles.Add(new int[] { 2, 1 });
        riverTiles.Add(new int[] { 2, 2 });
        riverTiles.Add(new int[] { 0, 0 });
        riverTiles.Add(new int[] { 1, 2 });
        riverTiles.Add(new int[] { 3, 3 });

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                var tile = new GardenTile(this, x, y);
                //tile.HasFence = true;
                //tile.IsIrrigated = true;
                tiles[x, y] = tile;
                if (riverTiles.Contains(new int[] {x,y}))
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
