public class GardenTile : BaseTile {

    public bool HasFence { get; set; }
    public bool IsIrrigated { get; set; }
    public bool IsPlanted { get; set; }

    public GardenTile(World world, int X, int Y)
    {
        this.World = World;
        this.X = X;
        this.Y = Y;
    }
}
