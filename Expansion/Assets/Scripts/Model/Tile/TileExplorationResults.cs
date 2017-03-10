using System.Collections.Generic;

public class TileExplorationResults
{
    public TileExplorationResults()
    {
        ExplorationEntries = new List<ExplorationStoryEntry>();
    }
    public Inventory ExplorationInventory { get; set; }
    public float ElapsedTimeHours { get; set; }
    public List<ExplorationStoryEntry> ExplorationEntries { get; set; }

}
