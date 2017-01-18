﻿using UnityEngine;

public class World {

    Tile[,] tiles;

    int width;
    int height;

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

        tiles = new Tile[width, height];

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                tiles[x, y] = new Tile(this, x, y);
            }
        }

        Debug.Log("World created with " + width * height + " tiles.");
    }

    public Tile GetTileAt(int x, int y)
    {
        if (x > width || y > height || x < 0 || y < 0)
            throw new System.Exception("Coordinates are out of bounds.");
        return tiles[x, y];
    }
}
