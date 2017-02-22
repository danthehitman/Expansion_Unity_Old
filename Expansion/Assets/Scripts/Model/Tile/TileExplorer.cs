

using UnityEngine;

public class TileExplorer
{
    public static Inventory ExploreTile(HumanEntity entity, BaseTile tile)
    {
        var rand = new Random();
        //Determine tile properties
        RevealTile(entity, tile);
        //Determine foraging haul
        var inventory = ForageTile(entity, tile);
        //Determine events (lost, special, etc...)
        //Determine effects of events
        return inventory;
    }

    private static Inventory ForageTile(HumanEntity entity, BaseTile tile)
    {
        var inventory = new Inventory();
        var resourceHaul = 0;
        //Bark
        resourceHaul = CalculateResourceHaul(2f, tile.TileResourceData.Bark, entity.AdjustedForagingSkill);
        for (int i = 0; i < resourceHaul; i++)
        {
            inventory.AddMaterial(new Bark());
        }
        //Berries
        resourceHaul = CalculateResourceHaul(6f, tile.TileResourceData.Berries, entity.AdjustedForagingSkill);
        for (int i = 0; i < resourceHaul; i++)
        {
            inventory.AddMaterial(new Berries());
        }
        //Bone
        resourceHaul = CalculateResourceHaul(4f, tile.TileResourceData.SmallAnimals, entity.AdjustedForagingSkill);
        for (int i = 0; i < CalculateResourceHaul(4f, tile.TileResourceData.SmallAnimals, entity.AdjustedForagingSkill); i++)
        {
            inventory.AddMaterial(new Bone() {Size = Resource.ResourceSize.Small });
        }
        resourceHaul = CalculateResourceHaul(4f, tile.TileResourceData.LargeAnimals, entity.AdjustedForagingSkill);
        for (int i = 0; i < CalculateResourceHaul(4f, tile.TileResourceData.LargeAnimals, entity.AdjustedForagingSkill); i++)
        {
            inventory.AddMaterial(new Bone() { Size = Resource.ResourceSize.Large });
        }
        //Cactus
        resourceHaul = CalculateResourceHaul(12f, tile.TileResourceData.CactusStuffs, entity.AdjustedForagingSkill);
        for (int i = 0; i < resourceHaul; i++)
        {
            inventory.AddMaterial(new Cactus());
        }
        //Egg
        resourceHaul = CalculateResourceHaul(1f, tile.TileResourceData.Birds, entity.AdjustedForagingSkill);
        for (int i = 0; i < resourceHaul; i++)
        {
            inventory.AddMaterial(new Egg());
        }
        //Fruit
        resourceHaul = CalculateResourceHaul(12f, tile.TileResourceData.Fruit, entity.AdjustedForagingSkill);
        for (int i = 0; i < resourceHaul; i++)
        {
            inventory.AddMaterial(new Fruit());
        }
        //Grass
        resourceHaul = CalculateResourceHaul(1f, tile.TileResourceData.Grasses, entity.AdjustedForagingSkill);
        for (int i = 0; i < resourceHaul; i++)
        {
            inventory.AddMaterial(new Grass());
        }
        //Hide
        resourceHaul = CalculateResourceHaul(1f, tile.TileResourceData.SmallAnimals, entity.AdjustedForagingSkill);
        for (int i = 0; i < resourceHaul; i++)
        {
            inventory.AddMaterial(new Hide() { Size = Resource.ResourceSize.Small });
        }
        resourceHaul = CalculateResourceHaul(1f, tile.TileResourceData.LargeAnimals, entity.AdjustedForagingSkill);
        for (int i = 0; i < resourceHaul; i++)
        {
            inventory.AddMaterial(new Hide() { Size = Resource.ResourceSize.Large });
        }
        //Broad Leaf
        resourceHaul = CalculateResourceHaul(2f, tile.TileResourceData.BroadLeaf, entity.AdjustedForagingSkill);
        for (int i = 0; i < resourceHaul; i++)
        {
            inventory.AddMaterial(new BroadLeaf());
        }
        //Long Leaf
        resourceHaul = CalculateResourceHaul(2f, tile.TileResourceData.LongLeaf, entity.AdjustedForagingSkill);
        for (int i = 0; i < resourceHaul; i++)
        {
            inventory.AddMaterial(new LongLeaf());
        }
        //Small Leaf
        resourceHaul = CalculateResourceHaul(6f, tile.TileResourceData.SmallLeaf, entity.AdjustedForagingSkill);
        for (int i = 0; i < resourceHaul; i++)
        {
            inventory.AddMaterial(new SmallLeaf());
        }
        //Lose Fur
        resourceHaul = CalculateResourceHaul(1f, tile.TileResourceData.LargeAnimals, entity.AdjustedForagingSkill);
        for (int i = 0; i < resourceHaul; i++)
        {
            inventory.AddMaterial(new LoseFur());
        }
        //Meat
        resourceHaul = CalculateResourceHaul(1f, tile.TileResourceData.SmallAnimals, entity.AdjustedForagingSkill);
        for (int i = 0; i < resourceHaul; i++)
        {
            inventory.AddMaterial(new Meat() { Size = Resource.ResourceSize.Small });
        }
        resourceHaul = CalculateResourceHaul(2f, tile.TileResourceData.LargeAnimals, entity.AdjustedForagingSkill);
        for (int i = 0; i < resourceHaul; i++)
        {
            inventory.AddMaterial(new Meat() { Size = Resource.ResourceSize.Large });
        }
        //Moss
        resourceHaul = CalculateResourceHaul(2f, tile.TileResourceData.Mosses, entity.AdjustedForagingSkill);
        for (int i = 0; i < resourceHaul; i++)
        {
            inventory.AddMaterial(new Moss());
        }
        //Nuts
        resourceHaul = CalculateResourceHaul(1f, tile.TileResourceData.Nuts, entity.AdjustedForagingSkill);
        for (int i = 0; i < resourceHaul; i++)
        {
            inventory.AddMaterial(new Nuts());
        }
        //Rocks
        resourceHaul = CalculateResourceHaul(12f, tile.TileResourceData.SmallRocks, entity.AdjustedForagingSkill);
        for (int i = 0; i < resourceHaul; i++)
        {
            inventory.AddMaterial(new Rock() { Size = Resource.ResourceSize.Small });
        }
        resourceHaul = CalculateResourceHaul(6f, tile.TileResourceData.LargeRocks, entity.AdjustedForagingSkill);
        for (int i = 0; i < resourceHaul; i++)
        {
            inventory.AddMaterial(new Rock() { Size = Resource.ResourceSize.Large });
        }
        //Roots
        resourceHaul = CalculateResourceHaul(4f, tile.TileResourceData.Roots, entity.AdjustedForagingSkill);
        for (int i = 0; i < resourceHaul; i++)
        {
            inventory.AddMaterial(new Roots());
        }
        //Seeds
        resourceHaul = CalculateResourceHaul(12f, tile.TileResourceData.Seeds, entity.AdjustedForagingSkill);
        for (int i = 0; i < resourceHaul; i++)
        {
            inventory.AddMaterial(new Seeds());
        }
        //Sticks
        resourceHaul = CalculateResourceHaul(12f, tile.TileResourceData.Sticks, entity.AdjustedForagingSkill);
        for (int i = 0; i < resourceHaul; i++)
        {
            inventory.AddMaterial(new Stick());
        }
        //Teeth
        resourceHaul = CalculateResourceHaul(8f, tile.TileResourceData.SmallAnimals, entity.AdjustedForagingSkill);
        for (int i = 0; i < resourceHaul; i++)
        {
            inventory.AddMaterial(new Teeth() { Size = Resource.ResourceSize.Small });
        }
        resourceHaul = CalculateResourceHaul(8f, tile.TileResourceData.LargeAnimals, entity.AdjustedForagingSkill);
        for (int i = 0; i < resourceHaul; i++)
        {
            inventory.AddMaterial(new Teeth() { Size = Resource.ResourceSize.Large });
        }
        //Weeds
        resourceHaul = CalculateResourceHaul(6f, tile.TileResourceData.Weeds, entity.AdjustedForagingSkill);
        for (int i = 0; i < resourceHaul; i++)
        {
            inventory.AddMaterial(new Weed());
        }
        //Wood
        resourceHaul = CalculateResourceHaul(4f, tile.TileResourceData.Wood, entity.AdjustedForagingSkill);
        for (int i = 0; i < resourceHaul; i++)
        {
            inventory.AddMaterial(new Wood());
        }

        return inventory;
    }

    private static int CalculateResourceHaul(float resourceBase, float tileMultiplier, float entityMultiplier)
    {
        var resourceFinal = (int)System.Math.Round((resourceBase * tileMultiplier) * entityMultiplier);
        return resourceFinal;
    }

    private static void RevealTile(HumanEntity entity, BaseTile tile)
    {
        var biomeType = tile.TerrainData.BiomeType;
        switch (biomeType)
        {
            case BiomeType.Desert:
                RevealDesert(tile, entity);
                break;
            case BiomeType.Savanna:
                break;
            case BiomeType.TropicalRainforest:
                break;
            case BiomeType.Grassland:
                break;
            case BiomeType.Woodland:
                break;
            case BiomeType.SeasonalForest:
                break;
            case BiomeType.TemperateRainforest:
                break;
            case BiomeType.BorealForest:
                break;
            case BiomeType.Tundra:
                break;
            case BiomeType.Ice:
                break;
        }
    }

    private static void RevealDesert(BaseTile tile, HumanEntity entity)
    {
        tile.TileResourceData.Bark = CalculateResourceAvailabilityMultiplier(2f,
            entity.AdjustedSurvivalSkill);
        tile.TileResourceData.Berries = CalculateResourceAvailabilityMultiplier(12f,
            entity.AdjustedSurvivalSkill);
        tile.TileResourceData.SmallAnimals = CalculateResourceAvailabilityMultiplier(2f,
            entity.AdjustedExplorationSkill);
        tile.TileResourceData.LargeAnimals = CalculateResourceAvailabilityMultiplier(1f,
            entity.AdjustedExplorationSkill);
        tile.TileResourceData.BroadLeaf = CalculateResourceAvailabilityMultiplier(0f,
            entity.AdjustedExplorationSkill);
        tile.TileResourceData.CactusStuffs = CalculateResourceAvailabilityMultiplier(0f,
            entity.AdjustedExplorationSkill);
        tile.TileResourceData.Birds = CalculateResourceAvailabilityMultiplier(.75f,
            entity.AdjustedSurvivalSkill);
        tile.TileResourceData.Fruit = CalculateResourceAvailabilityMultiplier(.75f,
            entity.AdjustedSurvivalSkill);
    }

    private static float CalculateResourceAvailabilityMultiplier(float resourceBase, float entityMultiplier)
    {
        var roll = Random.Range(0.5f, 2f);
        var resourceFinal = (float)(resourceBase * roll) * entityMultiplier;
        return resourceFinal;
    }
}
