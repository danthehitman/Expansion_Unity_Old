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

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                var tile = new GardenTile(this, x, y);
                tile.HasFence = true;
                tile.IsIrrigated = true;
                tiles[x, y] = tile;
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
