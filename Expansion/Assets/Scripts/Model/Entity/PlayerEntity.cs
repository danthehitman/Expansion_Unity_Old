﻿public class PlayerEntity : BaseEntity
{
    private int health;
    private int hunger;
    private int thirst;
    private int fatigue;

    private float gardeningSkill;
    private float foragingSkill;
    private float huntingSkill;
    private float buildingSkill;    

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

    public const string GardeningSkillPropertyName = "GardeningSkill";
    public float GardeningSkill
    {
        get
        {
            return gardeningSkill;
        }

        set
        {
            gardeningSkill = value;
            OnPropertyChanged(GardeningSkillPropertyName);
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

    public PlayerEntity()
    {
        X = 1;
        Y = 1;
        Health = 100;
        Hunger = 0;
        Thirst = 0;
        Fatigue = 0;
    }

    public void EntityToTile(BaseTile tile)
    {
        SetPosition(tile.X, tile.Y);
    }
}
