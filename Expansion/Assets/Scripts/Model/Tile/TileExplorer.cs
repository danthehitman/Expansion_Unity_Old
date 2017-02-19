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
        var roll = rand.Next(0, 70);

        //Foraging Skills
        roll = rand.Next(0, 100);
        if (roll + 10 <= entity.AdjustedForagingSkill)
            results.LoseFur = rand.Next(1, 1);

        roll = rand.Next(0, 100);
        var smallCarcasses = rand.Next(0,1);
        if (roll <= entity.AdjustedForagingSkill)
        {
            var carcasses = rand.Next(1, 3);
            results.SmallHide = rand.Next(1, 1) * carcasses;
            results.SmallAnimalBones = rand.Next(1, 2) * carcasses;
            results.SmallAnimalTeeth = rand.Next(1, 2) * carcasses;
            results.SmallAnimalMeat = rand.Next(1, 2) * carcasses;
        }

        roll = rand.Next(0, 100);
        if (roll <= entity.AdjustedForagingSkill)
        {
            var carcasses = rand.Next(1, 3);
            results.SmallHide = rand.Next(1, 1) * carcasses;
            results.SmallAnimalBones = rand.Next(1, 2) * carcasses;
            results.SmallAnimalTeeth = rand.Next(1, 2) * carcasses;
            results.SmallAnimalMeat = rand.Next(1, 2) * carcasses;
        }

        //Guaranteeds
        if (roll <= entity.AdjustedForagingSkill)
            results.Sticks = rand.Next(2, 3);
        else
            results.Sticks = 1;

        roll = rand.Next(0, 70);
        if (roll + 10 <= entity.AdjustedForagingSkill)
            results.Wood = rand.Next(2, 2);
        else
            results.Wood = 1;

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
        public int Cattails { get; set; }
        public int Seeds { get; set; }

        public int BirdEggs { get; set; }
    }
}
