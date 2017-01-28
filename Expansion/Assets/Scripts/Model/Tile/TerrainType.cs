using UnityEngine;

public class TerrainType
{
    public string Name;
    public bool Navigable;
    public Sprite[] Tiles;
    public bool IsAnimated;

    public Sprite GetTile(float x, float y, int key)
    {
        return Tiles[RandomHelper.Range(x, y, key, Tiles.Length)];
    }
}
