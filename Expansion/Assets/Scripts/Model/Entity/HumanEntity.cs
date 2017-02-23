using UnityEngine;

public class HumanEntity : BaseEntity
{
    //Stats
    private int health;
    private int hunger;
    private int thirst;
    private int fatigue;
    private int morale;

    public const string HealthPropertyName = "Health";
    public int Health
    {
        get
        {
            return health;
        }

        set
        {
            health = value;
            OnPropertyChanged(HealthPropertyName);
        }
    }

    public const string HungerPropertyName = "Hunger";
    public int Hunger
    {
        get
        {
            return hunger;
        }

        set
        {
            hunger = value;
            OnPropertyChanged(HungerPropertyName);
        }
    }

    public const string ThirstPropertyName = "Thirst";
    public int Thirst
    {
        get
        {
            return thirst;
        }

        set
        {
            thirst = value;
            OnPropertyChanged(ThirstPropertyName);
        }
    }

    public const string MoralePropertyName = "Morale";
    public int Morale
    {
        get
        {
            return morale;
        }

        set
        {
            morale = value;
            OnPropertyChanged(MoralePropertyName);
        }
    }

    public const string FatiguePropertyName = "Fatigue";
    public int Fatigue
    {
        get
        {
            return fatigue;
        }

        set
        {
            fatigue = value;
            OnPropertyChanged(FatiguePropertyName);
        }
    }



    //Skills
    public float AgricultureSkill { get; set; }
    public float ArchiologicalSkill { get; set; }
    public float ResearchSkill { get; set; }
    public float ExplorationSkill { get; set; }
    public float ForagingSkill { get; set; }
    public float HuntingSkill { get; set; }
    public float BuildingSkill { get; set; }
    public float FishingSkill { get; set; }
    public float FightingSkill { get; set; }
    public float MiningSkill { get; set; }
    public float HusbandrySkill { get; set; }
    public float SurvivalSkill { get; set; }
    public float CamoSkill { get; set; }
    public float NaviationSkill { get; set; }

    public float AdjustedAgricultureSkill { get; set; }
    public float AdjustedArchiologicalSkill { get; set; }
    public float AdjustedResearchSkill { get; set; }
    public float AdjustedExplorationSkill { get; set; }
    public float AdjustedForagingSkill { get; set; }
    public float AdjustedHuntingSkill { get; set; }
    public float AdjustedBuildingSkill { get; set; }
    public float AdjustedFishingSkill { get; set; }
    public float AdjustedFightingSkill { get; set; }
    public float AdjustedMiningSkill { get; set; }
    public float AdjustedHusbandrySkill { get; set; }
    public float AdjustedSurvivalSkill { get; set; }
    public float AdjustedCamoSkill { get; set; }
    public float AdjustedNaviationSkill { get; set; }

    //Traits
    public float ConstitutionTrait { get; set; }

    public HumanEntity()
    {
        AdjustedAgricultureSkill = Random.Range(0.1f, 0.5f);
        AdjustedArchiologicalSkill = Random.Range(0.1f, 0.5f);
        AdjustedResearchSkill = Random.Range(0.1f, 0.5f);
        AdjustedExplorationSkill = Random.Range(0.1f, 0.5f);
        AdjustedForagingSkill = Random.Range(0.1f, 0.5f);
        AdjustedHuntingSkill = Random.Range(0.1f, 0.5f);
        AdjustedBuildingSkill = Random.Range(0.1f, 0.5f);
        AdjustedFishingSkill = Random.Range(0.1f, 0.5f);
        AdjustedFightingSkill = Random.Range(0.1f, 0.5f);
        AdjustedMiningSkill = Random.Range(0.1f, 0.5f);
        AdjustedHusbandrySkill = Random.Range(0.1f, 0.5f);
        AdjustedSurvivalSkill = Random.Range(0.1f, 0.5f);
        AdjustedCamoSkill = Random.Range(0.1f, 0.5f);
        AdjustedNaviationSkill = Random.Range(0.1f, 0.5f);

        //AdjustedAgricultureSkill = Random.Range(1f, 2f);
        //AdjustedArchiologicalSkill = Random.Range(1f, 2f);
        //AdjustedResearchSkill = Random.Range(1f, 2f);
        //AdjustedExplorationSkill = Random.Range(1f, 2f);
        //AdjustedForagingSkill = Random.Range(1f, 2f);
        //AdjustedHuntingSkill = Random.Range(1f, 2f);
        //AdjustedBuildingSkill = Random.Range(1f, 2f);
        //AdjustedFishingSkill = Random.Range(1f, 2f);
        //AdjustedFightingSkill = Random.Range(1f, 2f);
        //AdjustedMiningSkill = Random.Range(1f, 2f);
        //AdjustedHusbandrySkill = Random.Range(1f, 2f);
        //AdjustedSurvivalSkill = Random.Range(1f, 2f);
        //AdjustedCamoSkill = Random.Range(1f, 2f);
        //AdjustedNaviationSkill = Random.Random.Range(1f, 2f);

        X = 1;
        Y = 1;
        Health = 100;
        Hunger = 0;
        Thirst = 0;
        Fatigue = 0;
    }

    public void MoveEntityToTile(BaseTile tile)
    {
        SetPosition(tile.X, tile.Y);
    }
}
