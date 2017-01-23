public class PlayerEntity : BaseEntity
{
    private float x;
    private float y;
    private int health;
    private int hunger;
    private int thirst;
    private int fatigue;

    private float gardeningSkill;
    private float foragingSkill;
    private float huntingSkill;
    private float BuildingSkill;

    public float X
    {
        get
        {
            return x;
        }

        set
        {
            x = value;
            OnEntityDataChanged();
        }
    }

    public float Y
    {
        get
        {
            return y;
        }

        set
        {
            y = value;
            OnEntityDataChanged();
        }
    }

    public int Health
    {
        get
        {
            return health;
        }

        set
        {
            health = value;
            OnEntityDataChanged();
        }
    }

    public int Hunger
    {
        get
        {
            return hunger;
        }

        set
        {
            hunger = value;
            OnEntityDataChanged();
        }
    }

    public int Thirst
    {
        get
        {
            return thirst;
        }

        set
        {
            thirst = value;
            OnEntityDataChanged();
        }
    }

    public int Fatigue
    {
        get
        {
            return fatigue;
        }

        set
        {
            fatigue = value;
            OnEntityDataChanged();
        }
    }

    public float GardeningSkill
    {
        get
        {
            return gardeningSkill;
        }

        set
        {
            gardeningSkill = value;
            OnEntityDataChanged();
        }
    }

    public float ForagingSkill
    {
        get
        {
            return foragingSkill;
        }

        set
        {
            foragingSkill = value;
            OnEntityDataChanged();
        }
    }

    public float HuntingSkill
    {
        get
        {
            return huntingSkill;
        }

        set
        {
            huntingSkill = value;
            OnEntityDataChanged();
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

    public void SetPosition(float x, float y)
    {
        this.x = x;
        this.y = y;
        OnEntityDataChanged();
    }
}
