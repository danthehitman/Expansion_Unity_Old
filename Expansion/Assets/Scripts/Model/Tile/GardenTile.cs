public class GardenTile : BaseTile
{
    private bool hasFence;
    private bool isIrrigated;
    private bool isPlanted;

    public const string HasFencePropertyName = "HasFence";
    public bool HasFence
    {
        get
        {
            return hasFence;
        }

        set
        {
            hasFence = value;
            OnPropertyChanged(HasFencePropertyName);
        }
    }

    public const string IsIrrigatedPropertyName = "IsIrrigated";
    public bool IsIrrigated
    {
        get
        {
            return isIrrigated;
        }

        set
        {
            isIrrigated = value;
            OnPropertyChanged(IsIrrigatedPropertyName);
        }
    }

    public const string IsPlantedPropertyName = "IsPlanted";
    public bool IsPlanted
    {
        get
        {
            return isPlanted;
        }

        set
        {
            isPlanted = value;
            OnPropertyChanged(IsPlantedPropertyName);
        }
    }

    public GardenTile(World world, int x, int y): base(world,x,y)
    {
    }
}
