
using UnityEngine;

public class TileExplorer
{
    public static TileExplorationResults ExploreTile(HumanEntity entity, BaseTile tile)
    {
        var results = new TileExplorationResults();
        results.ElapsedTimeHours = GetBaseExplorationTime(tile);
        //Determine tile properties
        RevealTile(entity, tile);
        //Determine foraging haul
        var inventory = ForageTile(entity, tile);
        //Determine events (lost, special, etc...)

        //Determine effects of events
        return results;
    }

    private static void DetermineExplorationEvents(HumanEntity entity, BaseTile tile,
        TileExplorationResults explorationResults)
    {
        var roll = 0f;
        //Do we get lost?
        roll = Random.Range(0f, .5f);
        roll = roll + DetermineTileLostAndSpecialDiscoveryModifier(tile);
        if (roll > entity.AdjustedNaviationSkill)
        {
            //Lost, what happened to us based on our survival skill?
        }
        roll = Random.Range(0f, .5f);
        roll = roll + DetermineTileLostAndSpecialDiscoveryModifier(tile, true);
        if (roll < entity.ExplorationSkill)
        {
            //We made a special discovery
        }
        //Do we suffer a catastrophe?
        //
    }

    private static float DetermineTileLostAndSpecialDiscoveryModifier(BaseTile tile, bool ignoreNeighborModifiers = false)
    {
        var biomeType = tile.TerrainData.BiomeType;
        var result = 0f;
        switch (biomeType)
        {
            case BiomeType.Desert:
                result = .3f;
                break;
            case BiomeType.Savanna:
                result = .1f;
                break;
            case BiomeType.TropicalRainforest:
                result = .5f;
                break;
            case BiomeType.Grassland:
                result = .1f;
                break;
            case BiomeType.Woodland:
                result = .2f;
                break;
            case BiomeType.SeasonalForest:
                result = .4f;
                break;
            case BiomeType.TemperateRainforest:
                result = .5f;
                break;
            case BiomeType.BorealForest:
                result = .7f;
                break;
            case BiomeType.Tundra:
                result = .4f;
                break;
            case BiomeType.Ice:
                result = .5f;
                break;
        }
        if (!ignoreNeighborModifiers && (tile.HasRiverNeighbor() || tile.TerrainData.HeightType == HeightType.Shore))
            result = result - 0.3f;
        
        return result;
    }

    private static float GetBaseExplorationTime(BaseTile tile)
    {
        var biomeType = tile.TerrainData.BiomeType;
        var result = 0f;
        switch (biomeType)
        {
            case BiomeType.Desert:
                result = 8f;
                break;
            case BiomeType.Savanna:
                result = 12f;
                break;
            case BiomeType.TropicalRainforest:
                result = 24f;
                break;
            case BiomeType.Grassland:
                result = 10f;
                break;
            case BiomeType.Woodland:
                result = 10f;
                break;
            case BiomeType.SeasonalForest:
                result = 24f;
                break;
            case BiomeType.TemperateRainforest:
                result = 30f;
                break;
            case BiomeType.BorealForest:
                result = 20f;
                break;
            case BiomeType.Tundra:
                result = 12f;
                break;
            case BiomeType.Ice:
                result = 12f;
                break;
        }

        return result;
    }

    private static Inventory ForageTile(HumanEntity entity, BaseTile tile)
    {
        var inventory = new Inventory();
        var resourceHaul = 0;
        //Bark
        resourceHaul = CalculateResourceHaul(12f, tile.TileResourceData.Bark, entity.AdjustedForagingSkill);
        for (int i = 0; i < resourceHaul; i++)
        {
            inventory.AddMaterial(new Bark());
        }
        //Berries
        resourceHaul = CalculateResourceHaul(24f, tile.TileResourceData.Berries, entity.AdjustedForagingSkill);
        for (int i = 0; i < resourceHaul; i++)
        {
            inventory.AddMaterial(new Berries());
        }
        //Bone
        resourceHaul = CalculateResourceHaul(8f, tile.TileResourceData.SmallAnimals, entity.AdjustedForagingSkill);
        for (int i = 0; i < resourceHaul; i++)
        {
            inventory.AddMaterial(new Bone() {Size = Resource.ResourceSize.Small });
        }
        resourceHaul = CalculateResourceHaul(8f, tile.TileResourceData.LargeAnimals, entity.AdjustedForagingSkill);
        for (int i = 0; i < resourceHaul; i++)
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
        resourceHaul = CalculateResourceHaul(6f, tile.TileResourceData.Birds, entity.AdjustedForagingSkill);
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
        resourceHaul = CalculateResourceHaul(24f, tile.TileResourceData.Grasses, entity.AdjustedForagingSkill);
        for (int i = 0; i < resourceHaul; i++)
        {
            inventory.AddMaterial(new Grass());
        }
        //Hide
        resourceHaul = CalculateResourceHaul(2f, tile.TileResourceData.SmallAnimals, entity.AdjustedForagingSkill);
        for (int i = 0; i < resourceHaul; i++)
        {
            inventory.AddMaterial(new Hide() { Size = Resource.ResourceSize.Small });
        }
        resourceHaul = CalculateResourceHaul(2f, tile.TileResourceData.LargeAnimals, entity.AdjustedForagingSkill);
        for (int i = 0; i < resourceHaul; i++)
        {
            inventory.AddMaterial(new Hide() { Size = Resource.ResourceSize.Large });
        }
        //Broad Leaf
        resourceHaul = CalculateResourceHaul(24f, tile.TileResourceData.BroadLeaf, entity.AdjustedForagingSkill);
        for (int i = 0; i < resourceHaul; i++)
        {
            inventory.AddMaterial(new BroadLeaf());
        }
        //Long Leaf
        resourceHaul = CalculateResourceHaul(24f, tile.TileResourceData.LongLeaf, entity.AdjustedForagingSkill);
        for (int i = 0; i < resourceHaul; i++)
        {
            inventory.AddMaterial(new LongLeaf());
        }
        //Small Leaf
        resourceHaul = CalculateResourceHaul(24f, tile.TileResourceData.SmallLeaf, entity.AdjustedForagingSkill);
        for (int i = 0; i < resourceHaul; i++)
        {
            inventory.AddMaterial(new SmallLeaf());
        }
        //Lose Fur
        resourceHaul = CalculateResourceHaul(4f, tile.TileResourceData.LargeAnimals, entity.AdjustedForagingSkill);
        for (int i = 0; i < resourceHaul; i++)
        {
            inventory.AddMaterial(new LoseFur());
        }
        //Meat
        resourceHaul = CalculateResourceHaul(2f, tile.TileResourceData.SmallAnimals, entity.AdjustedForagingSkill);
        for (int i = 0; i < resourceHaul; i++)
        {
            inventory.AddMaterial(new Meat() { Size = Resource.ResourceSize.Small });
        }
        resourceHaul = CalculateResourceHaul(4f, tile.TileResourceData.LargeAnimals, entity.AdjustedForagingSkill);
        for (int i = 0; i < resourceHaul; i++)
        {
            inventory.AddMaterial(new Meat() { Size = Resource.ResourceSize.Large });
        }
        //Moss
        resourceHaul = CalculateResourceHaul(6f, tile.TileResourceData.Mosses, entity.AdjustedForagingSkill);
        for (int i = 0; i < resourceHaul; i++)
        {
            inventory.AddMaterial(new Moss());
        }
        //Nuts
        resourceHaul = CalculateResourceHaul(18f, tile.TileResourceData.Nuts, entity.AdjustedForagingSkill);
        for (int i = 0; i < resourceHaul; i++)
        {
            inventory.AddMaterial(new Nuts());
        }
        //Rocks
        resourceHaul = CalculateResourceHaul(24f, tile.TileResourceData.SmallRocks, entity.AdjustedForagingSkill);
        for (int i = 0; i < resourceHaul; i++)
        {
            inventory.AddMaterial(new Rock() { Size = Resource.ResourceSize.Small });
        }
        resourceHaul = CalculateResourceHaul(12f, tile.TileResourceData.LargeRocks, entity.AdjustedForagingSkill);
        for (int i = 0; i < resourceHaul; i++)
        {
            inventory.AddMaterial(new Rock() { Size = Resource.ResourceSize.Large });
        }
        //Roots
        resourceHaul = CalculateResourceHaul(24f, tile.TileResourceData.Roots, entity.AdjustedForagingSkill);
        for (int i = 0; i < resourceHaul; i++)
        {
            inventory.AddMaterial(new Roots());
        }
        //Seeds
        resourceHaul = CalculateResourceHaul(24f, tile.TileResourceData.Seeds, entity.AdjustedForagingSkill);
        for (int i = 0; i < resourceHaul; i++)
        {
            inventory.AddMaterial(new Seeds());
        }
        //Sticks
        resourceHaul = CalculateResourceHaul(24f, tile.TileResourceData.Sticks, entity.AdjustedForagingSkill);
        for (int i = 0; i < resourceHaul; i++)
        {
            inventory.AddMaterial(new Stick());
        }
        //Teeth
        resourceHaul = CalculateResourceHaul(12f, tile.TileResourceData.SmallAnimals, entity.AdjustedForagingSkill);
        for (int i = 0; i < resourceHaul; i++)
        {
            inventory.AddMaterial(new Teeth() { Size = Resource.ResourceSize.Small });
        }
        resourceHaul = CalculateResourceHaul(12f, tile.TileResourceData.LargeAnimals, entity.AdjustedForagingSkill);
        for (int i = 0; i < resourceHaul; i++)
        {
            inventory.AddMaterial(new Teeth() { Size = Resource.ResourceSize.Large });
        }
        //Weeds
        resourceHaul = CalculateResourceHaul(24f, tile.TileResourceData.Weeds, entity.AdjustedForagingSkill);
        for (int i = 0; i < resourceHaul; i++)
        {
            inventory.AddMaterial(new Weed());
        }
        //Wood
        resourceHaul = CalculateResourceHaul(12f, tile.TileResourceData.Wood, entity.AdjustedForagingSkill);
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
                RevealSavanna(tile, entity);
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
        tile.TileResourceData.Bark = CalculateResourceAvailabilityMultiplier(
            0.5f);
        tile.TileResourceData.Berries = CalculateResourceAvailabilityMultiplier(
            1f);
        tile.TileResourceData.SmallAnimals = CalculateResourceAvailabilityMultiplier(
            1.25f);
        tile.TileResourceData.LargeAnimals = CalculateResourceAvailabilityMultiplier(
            0.75f);
        tile.TileResourceData.CactusStuffs = CalculateResourceAvailabilityMultiplier(
            2f);
        tile.TileResourceData.Birds = CalculateResourceAvailabilityMultiplier(
            0.75f);
        tile.TileResourceData.Fruit = CalculateResourceAvailabilityMultiplier(
            0.75f);
        tile.TileResourceData.Grasses = CalculateResourceAvailabilityMultiplier(
            0.75f);
        tile.TileResourceData.LongLeaf = CalculateResourceAvailabilityMultiplier(
            0.5f);
        tile.TileResourceData.SmallLeaf = CalculateResourceAvailabilityMultiplier(
            0.75f);
        tile.TileResourceData.BroadLeaf = CalculateResourceAvailabilityMultiplier(
            0f);
        tile.TileResourceData.Mosses = CalculateResourceAvailabilityMultiplier(
            0f);
        tile.TileResourceData.Nuts = CalculateResourceAvailabilityMultiplier(
            0.1f);
        tile.TileResourceData.LargeRocks = CalculateResourceAvailabilityMultiplier(
            1f);
        tile.TileResourceData.SmallRocks = CalculateResourceAvailabilityMultiplier(
            0.5f);
        tile.TileResourceData.Seeds = CalculateResourceAvailabilityMultiplier(
            0f);
        tile.TileResourceData.Sticks = CalculateResourceAvailabilityMultiplier(
            0.75f);
        tile.TileResourceData.Weeds = CalculateResourceAvailabilityMultiplier(
            0.5f);
        tile.TileResourceData.Wood = CalculateResourceAvailabilityMultiplier(
            0.25f);
    }

    private static void RevealSavanna(BaseTile tile, HumanEntity entity)
    {
        tile.TileResourceData.Bark = CalculateResourceAvailabilityMultiplier(
            1f);
        tile.TileResourceData.Berries = CalculateResourceAvailabilityMultiplier(
            1f);
        tile.TileResourceData.SmallAnimals = CalculateResourceAvailabilityMultiplier(
            1.5f);
        tile.TileResourceData.LargeAnimals = CalculateResourceAvailabilityMultiplier(
            1.5f);
        tile.TileResourceData.CactusStuffs = CalculateResourceAvailabilityMultiplier(
            1.5f);
        tile.TileResourceData.Birds = CalculateResourceAvailabilityMultiplier(
            1.25f);
        tile.TileResourceData.Fruit = CalculateResourceAvailabilityMultiplier(
            0.75f);
        tile.TileResourceData.Grasses = CalculateResourceAvailabilityMultiplier(
            1.5f);
        tile.TileResourceData.LongLeaf = CalculateResourceAvailabilityMultiplier(
            0.75f);
        tile.TileResourceData.SmallLeaf = CalculateResourceAvailabilityMultiplier(
            1f);
        tile.TileResourceData.BroadLeaf = CalculateResourceAvailabilityMultiplier(
            0.25f);
        tile.TileResourceData.Mosses = CalculateResourceAvailabilityMultiplier(
            0.25f);
        tile.TileResourceData.Nuts = CalculateResourceAvailabilityMultiplier(
            .75f);
        tile.TileResourceData.LargeRocks = CalculateResourceAvailabilityMultiplier(
            1f);
        tile.TileResourceData.SmallRocks = CalculateResourceAvailabilityMultiplier(
            1f);
        tile.TileResourceData.Seeds = CalculateResourceAvailabilityMultiplier(
            .2f);
        tile.TileResourceData.Sticks = CalculateResourceAvailabilityMultiplier(
            1f);
        tile.TileResourceData.Weeds = CalculateResourceAvailabilityMultiplier(
            1f);
        tile.TileResourceData.Wood = CalculateResourceAvailabilityMultiplier(
            1f);
    }

    private static float CalculateResourceAvailabilityMultiplier(float resourceBase)
    {
        var roll = Random.Range(0.5f, 2f);
        var resourceFinal = (float)(resourceBase * roll);
        return resourceFinal;
    }
}
