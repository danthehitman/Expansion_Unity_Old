using System.ComponentModel;
using UnityEngine;

public class DesertTileView : TileView
{
    private GameObject GrassLayer;

    // Use this for initialization
    public DesertTileView(DesertTile tile) : base(tile)
    {
        HighlightLayer.name = "DesertTile_" + tile.X + "_" + tile.Y;

        GrassLayer = ViewUtilities.GenerateViewObject(Constants.TILE_SAND, Constants.TILE_SAND, HighlightLayer, 0, null, true);
        GrassLayer.GetComponent<SpriteRenderer>().transform.Rotate(0, 0, Random.Range(0, 360));

        OnTileModelDataChanged(tile, new PropertyChangedEventArgs("All"));
    }

    public override void OnTileModelDataChanged(object sender, PropertyChangedEventArgs e)
    {
        base.OnTileModelDataChanged(sender, e);
        DesertTile gTile = sender as DesertTile;
        if (gTile == null)
        {
            throw new System.Exception("Tile mismatch for DesertTile.  Tile was: " + sender.GetType());
        }
    }
}
