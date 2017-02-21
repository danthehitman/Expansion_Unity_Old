using System;

public class TileExplorer
{
    public static Inventory ExploreTile(HumanEntity entity, BaseTile tile)
    {
        var rand = new Random();
        //Determine tile properties
        RevealTile(entity, tile, rand);
        //Determine foraging haul
        var inventory = ForageTile(entity, tile, rand);
        //Determine events (lost, special, etc...)
        //Determine effects of events
        return inventory;
    }

    private static Inventory ForageTile(HumanEntity entity, BaseTile tile, Random rand)
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
        resourceHaul = CalculateResourceHaul(12f, tile.TileResourceData.Berries, entity.AdjustedForagingSkill);
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

        return inventory;
    }

    private static int CalculateResourceHaul(float resourceBase, float tileMultiplier, float entityMultiplier)
    {
        var resourceFinal = (int)Math.Round((resourceBase * tileMultiplier) * entityMultiplier);
        return resourceFinal;
    }

    private static void RevealTile(HumanEntity entity, BaseTile tile, Random rand)
    {
        var biomeType = tile.TerrainData.BiomeType;
        switch (biomeType)
        {
            case BiomeType.Desert:
                RevealDesert(rand, tile, entity);
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

    private static void RevealDesert(Random rand, BaseTile tile, HumanEntity entity)
    {
        tile.TileResourceData.Bark = CalculateResourceAvailabilityMultiplier(2f,
            entity.AdjustedSurvivalSkill, rand);
        tile.TileResourceData.Berries = CalculateResourceAvailabilityMultiplier(12f,
            entity.AdjustedSurvivalSkill, rand);
        tile.TileResourceData.SmallAnimals = CalculateResourceAvailabilityMultiplier(2f,
            entity.AdjustedExplorationSkill, rand);
        tile.TileResourceData.LargeAnimals = CalculateResourceAvailabilityMultiplier(1f,
            entity.AdjustedExplorationSkill, rand);
    }

    private static float CalculateResourceAvailabilityMultiplier(float resourceBase, float entityMultiplier, Random rand)
    {
        var roll = rand.NextDouble();
        var resourceFinal = (float)(resourceBase * roll) * entityMultiplier;
        return resourceFinal;
    }
}
