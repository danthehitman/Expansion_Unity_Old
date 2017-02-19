public class HumanEntity : BaseEntity
{
    //Stats
    private int health;
    private int hunger;
    private int thirst;
    private int fatigue;
    private int morale;

    //Skills
    private float agricultureSkill;
    private float foragingSkill;
    private float huntingSkill;
    private float buildingSkill;
    private float fishingSkill;
    private float fightingSkill;
    private float miningSkill;
    private float husbandrySkill;
    private float survivalSkill;
    private float camoSkill;

    private float adjustedAgricultureSkill;
    private float adjustedForagingSkill;
    private float adjustedHuntingSkill;
    private float adjustedFishingSkill;
    private float adjustedFightingSkill;
    private float adjustedMininSkill;
    private float adjustedHusbandrySkill;
    private float adjustedSurvivalSkill;
    private float adjustedCamoSkill;

    //Traits
    private float constitutionTrait;  

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

    public const string AgricultureSkillPropertyName = "AgricultureSkill";
    public float AgricultureSkill
    {
        get
        {
            return agricultureSkill;
        }

        set
        {
            agricultureSkill = value;
            OnPropertyChanged(AgricultureSkillPropertyName);
        }
    }

    public const string ForagingSkillPropertyName = "ForagingSkill";
    public float ForagingSkill
    {
        get
        {
            return foragingSkill;
        }

        set
        {
            foragingSkill = value;
            OnPropertyChanged(ForagingSkillPropertyName);
        }
    }

    public const string HuntingSkillPropertyName = "HuntingSkill";
    public float HuntingSkill
    {
        get
        {
            return huntingSkill;
        }

        set
        {
            huntingSkill = value;
            OnPropertyChanged(HuntingSkillPropertyName);
        }
    }

    public const string BuildingSkillPropertyName = "BuildingSkill";
    public float BuildingSkill
    {
        get
        {
            return buildingSkill;
        }

        set
        {
            buildingSkill = value;
            OnPropertyChanged(BuildingSkillPropertyName);
        }
    }

    public const string FishingSkilllPropertyName = "FishingSkill";
    public float FishingSkill
    {
        get
        {
            return fishingSkill;
        }

        set
        {
            fishingSkill = value;
            OnPropertyChanged(FishingSkilllPropertyName);
        }
    }

    public const string FightingSkillPropertyName = "FightingSkill";
    public float FightingSkill
    {
        get
        {
            return fightingSkill;
        }

        set
        {
            fightingSkill = value;
            OnPropertyChanged(FightingSkillPropertyName);
        }
    }

    public const string MiningSkillPropertyName = "MiningSkill";
    public float MiningSkill
    {
        get
        {
            return miningSkill;
        }

        set
        {
            miningSkill = value;
            OnPropertyChanged(MiningSkillPropertyName);
        }
    }

    public const string HusbandrySkillPropertyName = "HusbandrySkill";
    public float HusbandrySkill
    {
        get
        {
            return husbandrySkill;
        }

        set
        {
            husbandrySkill = value;
            OnPropertyChanged(HusbandrySkillPropertyName);
        }
    }

    public const string SurvivalSkillPropertyName = "SurvivalSkill";
    public float SurvivalSkill
    {
        get
        {
            return survivalSkill;
        }

        set
        {
            survivalSkill = value;
            OnPropertyChanged(SurvivalSkillPropertyName);
        }
    }

    public const string CamoSkillPropertyName = "CamoSkill";
    public float CamoSkill
    {
        get
        {
            return camoSkill;
        }

        set
        {
            camoSkill = value;
            OnPropertyChanged(CamoSkillPropertyName);
        }
    }

    public const string ConstitutionTraitPropertyName = "ConstitutionTrait";
    public float ConstitutionTrait
    {
        get
        {
            return constitutionTrait;
        }

        set
        {
            constitutionTrait = value;
            OnPropertyChanged(ConstitutionTraitPropertyName);
        }
    }

    public const string AAdjustedAgricultureSkillPropertyName = "AdjustedAgricultureSkill";
    public float AdjustedAgricultureSkill
    {
        get
        {
            return adjustedAgricultureSkill;
        }

        set
        {
            adjustedAgricultureSkill = value;
            OnPropertyChanged(AAdjustedAgricultureSkillPropertyName);
        }
    }

    public const string AdjustedForagingSkillPropertyName = "AdjustedForagingSkill";
    public float AdjustedForagingSkill
    {
        get
        {
            return adjustedForagingSkill;
        }

        set
        {
            adjustedForagingSkill = value;
            OnPropertyChanged(AdjustedForagingSkillPropertyName);
        }
    }

    public const string AdjustedHuntingSkillPropertyName = "AdjustedHuntingSkill";
    public float AdjustedHuntingSkill
    {
        get
        {
            return adjustedHuntingSkill;
        }

        set
        {
            adjustedHuntingSkill = value;
            OnPropertyChanged(AdjustedHuntingSkillPropertyName);
        }
    }

    public const string AdjustedFishingSkillPropertyName = "AdjustedFishingSkill";
    public float AdjustedFishingSkill
    {
        get
        {
            return adjustedFishingSkill;
        }

        set
        {
            adjustedFishingSkill = value;
            OnPropertyChanged(AdjustedFishingSkillPropertyName);
        }
    }

    public const string AdjustedFightingSkillPropertyName = "AdjustedFightingSkill";
    public float AdjustedFightingSkill
    {
        get
        {
            return adjustedFightingSkill;
        }

        set
        {
            adjustedFightingSkill = value;
            OnPropertyChanged(AdjustedFightingSkillPropertyName);
        }
    }

    public const string AdjustedMininSkillPropertyName = "AdjustedMininSkill";
    public float AdjustedMininSkill
    {
        get
        {
            return adjustedMininSkill;
        }

        set
        {
            adjustedMininSkill = value;
            OnPropertyChanged(AdjustedMininSkillPropertyName);
        }
    }

    public const string AdjustedHusbandrySkillPropertyName = "AdjustedHusbandrySkill";
    public float AdjustedHusbandrySkill
    {
        get
        {
            return adjustedHusbandrySkill;
        }

        set
        {
            adjustedHusbandrySkill = value;
            OnPropertyChanged(AdjustedHusbandrySkillPropertyName);
        }
    }

    public const string AdjustedSurvivalSkillPropertyName = "AdjustedSurvivalSkill";
    public float AdjustedSurvivalSkill
    {
        get
        {
            return adjustedSurvivalSkill;
        }

        set
        {
            adjustedSurvivalSkill = value;
            OnPropertyChanged(AdjustedSurvivalSkillPropertyName);
        }
    }

    public const string AdjustedCamoSkillPropertyName = "AdjustedCamoSkill";
    public float AdjustedCamoSkill
    {
        get
        {
            return adjustedCamoSkill;
        }

        set
        {
            adjustedCamoSkill = value;
            OnPropertyChanged(AdjustedCamoSkillPropertyName);
        }
    }

    public HumanEntity()
    {
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
