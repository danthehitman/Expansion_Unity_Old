using System.ComponentModel;
using UnityEngine;

public class TileView
{
    public GameObject HighlightLayer;
    private bool isHighlighted;
    private bool isActivated;
    private BaseTile baseTile;

    public bool IsHighlighted
    {
        get
        {
            return isHighlighted;
        }

        set
        {
            isHighlighted = value;
            OnTileHighlightChanged(value);
        }
    }

    public bool IsActivated
    {
        get
        {
            return isActivated;
        }

        set
        {
            isActivated = value;
            OnTileActivationChanged(value);
        }
    }

    public TileView(BaseTile tile)
    {
        baseTile = tile;
        HighlightLayer = new GameObject();
        HighlightLayer.transform.position = new Vector3(tile.X, tile.Y, 0);
        HighlightLayer.AddComponent<SpriteRenderer>();
        var baseRenderer = HighlightLayer.GetComponent<SpriteRenderer>();
        baseRenderer.sortingLayerName = Constants.TILE_SORTING_LAYER;
        baseRenderer.sortingOrder = 1;
        baseRenderer.sprite = ViewUtilities.GetTileSprite(tile);
        baseRenderer.name = baseRenderer.sprite.name;

        //TODO: Need something other than the grasstile here for this.  Right now the bas tile is used for highlighting so maybe just
        // an empty sprite or figure out a different way to do it.
        //baseRenderer.sprite = SpriteManager.Instance.GetSpriteByName(Constants.TILE_GRASSLAND);
        //baseRenderer.color = new Color(1f, 1f, 1f, 0.0f);

        tile.PropertyChanged += OnTileModelDataChanged;
    }

    public virtual void OnTileModelDataChanged(object sender, PropertyChangedEventArgs e)
    {
    }

    public void OnTileHighlightChanged(bool highlighted)
    {
        if (!isActivated && HighlightLayer != null)
            HighlightLayer.GetComponent<SpriteRenderer>().color = highlighted? Color.gray : Color.white;
    }

    public void OnTileActivationChanged(bool activated)
    {
        if (HighlightLayer != null)
            HighlightLayer.GetComponent<SpriteRenderer>().color = activated ? Color.black : Color.white;
    }

    public TileView GetTileViewAtDirection(TileDirectionEnum direction)
    {
        switch (direction)
        {
            case TileDirectionEnum.Left:
                return WorldController.Instance.GetTileViewAt(baseTile.X - 1, baseTile.Y);
            case TileDirectionEnum.Right:
                return WorldController.Instance.GetTileViewAt(baseTile.X + 1, baseTile.Y);
            case TileDirectionEnum.Up:
                return WorldController.Instance.GetTileViewAt(baseTile.X, baseTile.Y + 1);
            case TileDirectionEnum.Down:
                return WorldController.Instance.GetTileViewAt(baseTile.X, baseTile.Y - 1);
            default:
                return null;
        }
    }
}