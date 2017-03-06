using System.Collections.Generic;
using UnityEngine;

public static class ViewUtilities
{
    public static GameObject GenerateViewObject(string spriteName, string objectName, int x, int y, int sortOrder, string sortingLayerName,
        bool spriteEnabled = false)
    {
        var NewObject = new GameObject();
        var renderer = NewObject.AddComponent<SpriteRenderer>();
        renderer.enabled = spriteEnabled;
        renderer.sortingOrder = sortOrder;
        renderer.sortingLayerName = sortingLayerName;
        NewObject.name = objectName;
        renderer.sprite = SpriteManager.Instance.GetSpriteByName(spriteName);
        NewObject.transform.position = new Vector3(x, y, 0);
        return NewObject;
    }

    public static GameObject GenerateViewObject(string spriteName, string objectName, GameObject parent, int sortOrder, string sortingLayerName,
        bool spriteEnabled = false)
    {
        var NewObject = new GameObject();
        var renderer = NewObject.AddComponent<SpriteRenderer>();
        renderer.enabled = spriteEnabled;
        renderer.sortingOrder = sortOrder;
        if(sortingLayerName != null)
            renderer.sortingLayerName = sortingLayerName;
        NewObject.name = objectName;
        renderer.sprite = SpriteManager.Instance.GetSpriteByName(spriteName);
        NewObject.transform.parent = parent.transform;
        NewObject.transform.localPosition = Vector3.zero;
        return NewObject;
    }

    public static GameObject GenerateViewObject(string spriteName, string objectName, GameObject parent, int sortOrder, string sortingLayerName,
        Vector3 position, bool spriteEnabled = false)
    {
        var NewObject = GenerateViewObject(spriteName, objectName, parent, sortOrder, sortingLayerName, spriteEnabled);
        NewObject.transform.localPosition = position;
        return NewObject;
    }

    public static GameObject GenerateContainerViewObject(string objectName, int x, int y, string sortingLayerName, GameObject parent = null)
    {
        var NewObject = new GameObject();
        var renderer = NewObject.AddComponent<SpriteRenderer>();
        renderer.enabled = false;
        renderer.sortingLayerName = sortingLayerName;
        NewObject.name = objectName;
        NewObject.transform.position = new Vector3(x, y, 0);
        return NewObject;
    }

    public static Sprite GetTileSprite(BaseTile tile, System.Random rand)
    {
        BiomeType value = tile.TerrainData.BiomeType;
        Sprite sprite = null;

        if (tile.TerrainData.HeightType == HeightType.DeepWater)
        {
            sprite = SpriteManager.Instance.GetSpriteByName(Constants.TILE_DEEP_WATER);
        }
        else if (tile.TerrainData.HeightType == HeightType.ShallowWater)
        {
            sprite = SpriteManager.Instance.GetSpriteByName(Constants.TILE_SHALLOW_WATER);
        }
        else if (tile.TerrainData.HeightType == HeightType.Rock)
        {
            sprite = SpriteManager.Instance.GetSpriteByName(Constants.TILE_MOUNTAIN);
        }
        else if (tile.TerrainData.HeightType == HeightType.River)
        {
            sprite = SpriteManager.Instance.GetSpriteByName(Constants.TILE_RIVER);
        }
        else
        {
            switch (value)
            {
                case BiomeType.Ice:
                    sprite = SpriteManager.Instance.GetSpriteByName(Constants.TILE_ICE);
                    break;
                case BiomeType.BorealForest:
                    sprite = SpriteManager.Instance.GetSpriteByName(Constants.TILE_BOREAL_FOREST);
                    break;
                case BiomeType.Desert:
                    sprite = GetRandomTileVarietySpriteByWeight(
                        SpriteManager.Instance.GetSpritesByKey(Constants.TILE_KEY_DESERT), rand);
                    break;
                case BiomeType.Grassland:
                    sprite = SpriteManager.Instance.GetSpriteByName(Constants.TILE_GRASSLAND);
                    break;
                case BiomeType.SeasonalForest:
                    sprite = SpriteManager.Instance.GetSpriteByName(Constants.TILE_SEASONAL_FOREST);
                    break;
                case BiomeType.Tundra:
                    sprite = SpriteManager.Instance.GetSpriteByName(Constants.TILE_TUNDRA);
                    break;
                case BiomeType.Savanna:
                    sprite = SpriteManager.Instance.GetSpriteByName(Constants.TILE_SAVANNA);
                    break;
                case BiomeType.TemperateRainforest:
                    sprite = SpriteManager.Instance.GetSpriteByName(Constants.TILE_TEMPERATE_RAINFOREST);
                    break;
                case BiomeType.TropicalRainforest:
                    sprite = SpriteManager.Instance.GetSpriteByName(Constants.TILE_TROPICAL_RAINFOREST);
                    break;
                case BiomeType.Woodland:
                    sprite = SpriteManager.Instance.GetSpriteByName(Constants.TILE_WOODLAND);
                    break;
            }
        }

        return sprite;
    }

    private static Sprite GetRandomTileVarietySpriteByWeight(Dictionary<int, List<Sprite>> sprites,
        System.Random rand)
    {
        Sprite result = null;
        float groups = sprites.Count;
        if (groups == 1)
            result = sprites[0][rand.Next(0, sprites[0].Count - 1)];
        else
        {
            var baseVariety = 0.8f;
            var leftOverVariety = 1f - baseVariety;
            var roll = rand.NextDouble();
            if (roll <= baseVariety)
                result = sprites[0][rand.Next(0, sprites[0].Count - 1)];
            else
            {
                var increment = leftOverVariety;
                for (int i = 1; i < groups; i++)
                {
                    increment = increment / (groups - 1f);
                    if (roll <= baseVariety + increment)
                    {
                        result = sprites[i][rand.Next(0, sprites[i].Count - 1)];
                        continue;
                    }
                    baseVariety += increment;
                }
            }
            if (result == null)
                result = sprites[0][rand.Next(0, sprites[0].Count - 1)];
        }
        return result;
    }
}
