//using System.ComponentModel;
//using UnityEngine;

//public class MountainTileView : TileView
//{
//    private GameObject GrassLayer;

//    // Use this for initialization
//    public MountainTileView(MountainTile tile) : base(tile)
//    {
//        HighlightLayer.name = "MountainTile_" + tile.X + "_" + tile.Y;

//        GrassLayer = ViewUtilities.GenerateViewObject(Constants.TILE_MOUNTAIN, Constants.TILE_MOUNTAIN, HighlightLayer, 0, null, true);
//        //GrassLayer.GetComponent<SpriteRenderer>().transform.Rotate(0, 0, Random.Range(0, 360));

//        OnTileModelDataChanged(tile, new PropertyChangedEventArgs("All"));
//    }

//    public override void OnTileModelDataChanged(object sender, PropertyChangedEventArgs e)
//    {
//        base.OnTileModelDataChanged(sender, e);
//        MountainTile gTile = sender as MountainTile;
//        if (gTile == null)
//        {
//            throw new System.Exception("Tile mismatch for MountainTile.  Tile was: " + sender.GetType());
//        }
//    }
//}