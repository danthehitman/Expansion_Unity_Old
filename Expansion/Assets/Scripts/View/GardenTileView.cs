using System;
using UnityEngine;

public class GardenTileView : TileView
{
    private GameObject FenceLayer;
    private GameObject WaterLayer;
    private GameObject RowLayer;
    private GameObject SeedLayer;

    // Use this for initialization
    public GardenTileView (GardenTile tile): base(tile)
    {
        var baseRenderer = BaseLayer.GetComponent<SpriteRenderer>();
        BaseLayer.name = "GardenTile_" + tile.X + "_" + tile.Y;
        baseRenderer.sprite = SpriteManager.Instance.GetSpriteByName(Constants.GRASS_SPRITE);
        BaseLayer.transform.position = new Vector3(tile.X, tile.Y, 0);

        WaterLayer = new GameObject();
        var waterRenderer = WaterLayer.AddComponent<SpriteRenderer>();
        waterRenderer.enabled = false;
        waterRenderer.sprite = SpriteManager.Instance.GetSpriteByName(Constants.IRRIGATION_SPRITE);
        waterRenderer.sortingOrder = 1;
        WaterLayer.transform.parent = BaseLayer.transform;
        WaterLayer.transform.localPosition = Vector3.zero;

        FenceLayer = new GameObject();
        var fenceRender = FenceLayer.AddComponent<SpriteRenderer>();
        fenceRender.enabled = false;
        fenceRender.sprite = SpriteManager.Instance.GetSpriteByName(Constants.FENCE_SPRITE);
        fenceRender.sortingOrder = 2;
        FenceLayer.transform.parent = BaseLayer.transform;
        FenceLayer.transform.localPosition = Vector3.zero;

        OnTileModelDataChanged(tile, new EventArgs());
    }

    public override void OnTileModelDataChanged(object sender, EventArgs e)
    {
        GardenTile gTile = sender as GardenTile;
        if (gTile == null)
        {
            throw new System.Exception("Tile mismatch for GardenView.  Tile was: " + sender.GetType());
        }
        
        WaterLayer.GetComponent<SpriteRenderer>().enabled = gTile.IsIrrigated;
        FenceLayer.GetComponent<SpriteRenderer>().enabled = gTile.HasFence;
    }
}
