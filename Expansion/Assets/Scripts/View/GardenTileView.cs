using System.ComponentModel;
using UnityEngine;

public class GardenTileView : TileView
{
    private GameObject FenceLayer;
    private GameObject WaterLayer;
    private GameObject RowLayer;
    private GameObject SeedLayer;
    private GameObject GrassLayer;

    // Use this for initialization
    public GardenTileView (GardenTile tile): base(tile)
    {
        HighlightLayer.name = "GardenTile_" + tile.X + "_" + tile.Y;

        GrassLayer = ViewUtilities.GenerateViewObject(Constants.TILE_GRASSLAND, Constants.TILE_GRASSLAND, HighlightLayer, 0, null, true);
        GrassLayer.GetComponent<SpriteRenderer>().transform.Rotate(0, 0, Random.Range(0, 360));
        WaterLayer = ViewUtilities.GenerateViewObject(Constants.IRRIGATION_SPRITE, Constants.IRRIGATION_SPRITE, HighlightLayer, 1, null, false);
        FenceLayer = ViewUtilities.GenerateViewObject(Constants.FENCE_SPRITE, Constants.FENCE_SPRITE, HighlightLayer, 2, null, false);

        OnTileModelDataChanged(tile, new PropertyChangedEventArgs("All"));
    }

    public override void OnTileModelDataChanged(object sender, PropertyChangedEventArgs e)
    {
        base.OnTileModelDataChanged(sender, e);
        GardenTile gTile = sender as GardenTile;
        if (gTile == null)
        {
            throw new System.Exception("Tile mismatch for GardenView.  Tile was: " + sender.GetType());
        }
        if (e.PropertyName == GardenTile.IsIrrigatedPropertyName || e.PropertyName == Constants.ALL_PROPERTIES_PROPERTY_NAME)
            WaterLayer.GetComponent<SpriteRenderer>().enabled = gTile.IsIrrigated;
        if (e.PropertyName == GardenTile.HasFencePropertyName || e.PropertyName == Constants.ALL_PROPERTIES_PROPERTY_NAME)
            FenceLayer.GetComponent<SpriteRenderer>().enabled = gTile.HasFence;
    }
}
