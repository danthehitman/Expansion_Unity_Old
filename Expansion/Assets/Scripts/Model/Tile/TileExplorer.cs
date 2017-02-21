using System;

public class TileExplorer
{
    public static Inventory ExploreTile(HumanEntity entity, BaseTile tile)
    {
        //Determine tile properties
        //Determine foraging haul
        //Determine events (lost, special, etc...)
        //Determine effects of events
        var inventory = RevealTile(entity, tile);
        return inventory;
    }

    private static Inventory RevealTile(HumanEntity entity, BaseTile tile)
    {
        var inventory = new Inventory();
        var rand = new Random();
        var biomeType = tile.TerrainData.BiomeType;

        ForageResults forageResults = null;
        switch (biomeType)
        {
            case BiomeType.Desert:
                forageResults = RevealDesert(rand, tile, entity);
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

    private static ForageResults RevealDesert(Random rand, BaseTile tile,  HumanEntity entity)
    {
        var results = new ForageResults();
        

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
        public int ThinLeaf { get; set; }
        public int Moss { get; set; }
        public int Weeds { get; set; }
        public int Grass { get; set; }

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
        public int Cactus { get; set; }
        public int Seeds { get; set; }

        public int BirdEggs { get; set; }
    }
}
