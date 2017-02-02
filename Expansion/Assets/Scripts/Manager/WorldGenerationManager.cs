using UnityEngine;

public class WorldGenerationManager
{
    private static WorldGenerationManager instance = null;

    private WorldGenerationManager()
    {
        AddTerrainTypes();
    }

    public static WorldGenerationManager Instance
    {
        get
        {
            if (instance == null)
                instance = new WorldGenerationManager();
            return instance;
        }
    }

    public TerrainType[] TerrainTypes { get; set; }

    private void AddTerrainTypes()
    {
        TerrainTypes = new TerrainType[3];
        TerrainTypes[0] =new TerrainType()
        {
            IsAnimated = false,
            Name = "Grassland",
            Navigable = true,
            Tiles = new Sprite[] { SpriteManager.Instance.GetSpriteByName(Constants.TILE_GRASSLAND) }
        };

        TerrainTypes[1] = new TerrainType()
        {
            IsAnimated = false,
            Name = "Desert",
            Navigable = true,
            Tiles = new Sprite[] { SpriteManager.Instance.GetSpriteByName(Constants.TILE_DESERT) }
        };

        TerrainTypes[2] = new TerrainType()
        {
            IsAnimated = false,
            Name = "Mountain",
            Navigable = true,
            Tiles = new Sprite[] { SpriteManager.Instance.GetSpriteByName(Constants.TILE_MOUNTAIN) }
        };
    }
}
