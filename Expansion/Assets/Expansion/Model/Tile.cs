public class Tile {

    public bool HasFence { get; set; }
    public bool IsIrrigated { get; set; }
    public bool IsPlanted { get; set; }
    public World World { get; set; }
    public int X { get; set; }
    public int Y { get; set; }

    public Tile(World world, int X, int Y)
    {
        this.World = World;
        this.X = X;
        this.Y = Y;
    }

}
