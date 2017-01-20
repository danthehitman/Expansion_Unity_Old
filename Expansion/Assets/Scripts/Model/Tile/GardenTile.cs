public class GardenTile : BaseTile
{
    private bool hasFence;
    private bool isIrrigated;
    private bool isPlanted;

    public bool HasFence
    {
        get
        {
            return hasFence;
        }

        set
        {
            hasFence = value;
            OnTileDataChanged();
        }
    }

    public bool IsIrrigated
    {
        get
        {
            return isIrrigated;
        }

        set
        {
            isIrrigated = value;
            OnTileDataChanged();
        }
    }

    public bool IsPlanted
    {
        get
        {
            return isPlanted;
        }

        set
        {
            isPlanted = value;
            OnTileDataChanged();
        }
    }

    public GardenTile(World world, int x, int y): base(world,x,y)
    {
    }
}
