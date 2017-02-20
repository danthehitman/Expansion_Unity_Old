using System;

public class TileExplorer
{
    public static Inventory ExploreTile(HumanEntity entity, BaseTile tile)
    {
        var inventory = ExploreTileAsHuman(entity, tile);
        return inventory;
    }

    private static Inventory ExploreTileAsHuman(HumanEntity entity, BaseTile tile)
    {
        var inventory = new Inventory();
        var rand = new Random();
        var biomeType = tile.TerrainData.BiomeType;

        ForageResults forageResults = null;
        switch (biomeType)
        {
            case BiomeType.Desert:
                forageResults = ExploreDesert(rand, entity);
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

        return inventory;
    }

    private static ForageResults ExploreDesert(Random rand, HumanEntity entity)
    {
        var results = new ForageResults();
        var roll = 0;

        //Skills stuff
        roll = rand.Next(0, 100);
        if (roll + 10 <= entity.AdjustedSurvivalSkill)
            results.LoseFur = 1;

        roll = rand.Next(0, 100);
        if (roll + 10 <= entity.AdjustedSurvivalSkill)
            results.Wood = rand.Next(2, 2);

        roll = rand.Next(0, 100);
        if (roll - 20 <= entity.AdjustedForagingSkill)
            results.SmallBirdEggs = rand.Next(1, 2);

        roll = rand.Next(0, 100);
        if (roll - 30 <= entity.AdjustedForagingSkill)
            results.MediumBirdEggs = 1;

        roll = rand.Next(0, 100);
        if (roll - 40 <= entity.AdjustedForagingSkill)
            results.LargeBirdEggs = 1;

        roll = rand.Next(0, 100);
        if (roll - 20 <= entity.AdjustedForagingSkill)
            results.Wood = rand.Next(12, 24);

        // Chance there  are one or more small carcasses
        if (rand.NextDouble() > .6f)
        {
            if (roll <= entity.AdjustedForagingSkill)
            {
                var carcasses = rand.Next(1, 2);
                results.SmallHide = 1 * carcasses;
                results.SmallAnimalBones = rand.Next(1, 2) * carcasses;
                results.SmallAnimalTeeth = rand.Next(1, 2) * carcasses;
                results.SmallAnimalMeat = rand.Next(1, 2) * carcasses;
            }
        }

        // Chance there  are one or more large carcasses
        if (rand.NextDouble() > .8f)
        {
            if (roll <= entity.AdjustedForagingSkill)
            {
                var carcasses = rand.Next(1, 3);
                results.LargeHide = 1 * carcasses;
                results.LargeAnimalBones = rand.Next(1, 2) * carcasses;
                results.LargeAnimalTeeth = rand.Next(1, 2) * carcasses;
                results.LargeAnimalMeat = rand.Next(1, 2) * carcasses;
            }
        }

        //Guarantees
        roll = rand.Next(0, 100);
        if (roll -10 <= entity.AdjustedForagingSkill)
            results.Sticks = rand.Next(2, 3);
        else
            results.Sticks = 1;

        roll = rand.Next(0, 100);
        if (roll <= entity.AdjustedSurvivalSkill)
            results.Fruit = rand.Next(12, 24);
        else
            results.Fruit = rand.Next(6, 10);

        results.LargeRocks = rand.Next(1, 1);
        results.SmallRocks = rand.Next(2, 10);

        return results;
    }

    internal class ForageResults
    {
        public int Sticks { get; set; }
        public int Wood { get; set; }
        public int Bark { get; set; }
        public int Roots { get; set; }
        public int LoseFur { get; set; }
        public int LargeRocks { get; set; }
        public int SmallRocks { get; set; }
        public int BroadLeaf { get; set; }

        public int SmallHide { get; set; }
        public int SmallAnimalBones { get; set; }
        public int SmallAnimalTeeth { get; set; }
        public int SmallAnimalMeat { get; set; }
        public int LargeHide { get; set; }
        public int LargeAnimalBones { get; set; }
        public int LargeAnimalTeeth { get; set; }
        public int LargeAnimalMeat { get; set; }

        public int Nuts { get; set; }
        public int Berries { get; set; }
        public int Fruit { get; set; }
        public int Cattails { get; set; }
        public int Seeds { get; set; }

        public int SmallBirdEggs { get; set; }
        public int MediumBirdEggs { get; set; }
        public int LargeBirdEggs { get; set; }
    }
}
