using System.ComponentModel;
using UnityEngine;

public abstract class TileView {
    public GameObject BaseLayer;

    private NeighborOrientedTileSprite riverSprite;
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
        BaseLayer = new GameObject();
        BaseLayer.transform.position = new Vector3(tile.X, tile.Y, 0);
        BaseLayer.AddComponent<SpriteRenderer>();
        SetRiverTile();
        tile.PropertyChanged += OnTileModelDataChanged;
    }

    public virtual void OnTileModelDataChanged(object sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == BaseTile.RiverTileConectionName || e.PropertyName == Constants.ALL_PROPERTIES_PROPERTY_NAME)
        {
            SetRiverTile();
            NotifyNeighbors();
        }
    }

    private void NotifyNeighbors()
    {
        var neighbor = GetTileViewAtDirection(TileDirectionEnum.Left);
        if (neighbor != null)
            neighbor.SetRiverTile();
        neighbor = GetTileViewAtDirection(TileDirectionEnum.Right);
        if (neighbor != null)
            neighbor.SetRiverTile();
        neighbor = GetTileViewAtDirection(TileDirectionEnum.Up);
        if (neighbor != null)
            neighbor.SetRiverTile();
        neighbor = GetTileViewAtDirection(TileDirectionEnum.Down);
        if (neighbor != null)
            neighbor.SetRiverTile();
    }

    public void SetRiverTile()
    {
        if (baseTile != null)
        {
            if (baseTile.RiverTileConnection != null)
            {
                CreateRiverSprite();

                var upTile = baseTile.GetTileAtDirection(TileDirectionEnum.Up);
                var upRiver = upTile != null && upTile.RiverTileConnection != null &&
                    upTile.RiverTileConnection.ConnectedDown && baseTile.RiverTileConnection.ConnectedUp;
                var downTile = baseTile.GetTileAtDirection(TileDirectionEnum.Down);
                var downRiver = downTile != null && downTile.RiverTileConnection != null &&
                    downTile.RiverTileConnection.ConnectedUp && baseTile.RiverTileConnection.ConnectedDown;
                var leftTile = baseTile.GetTileAtDirection(TileDirectionEnum.Left);
                var leftRiver = leftTile != null && leftTile.RiverTileConnection != null &&
                    leftTile.RiverTileConnection.ConnectedRight && baseTile.RiverTileConnection.ConnectedLeft;
                var rightTile = baseTile.GetTileAtDirection(TileDirectionEnum.Right);
                var rightRiver = rightTile != null && rightTile.RiverTileConnection != null &&
                    rightTile.RiverTileConnection.ConnectedLeft && baseTile.RiverTileConnection.ConnectedRight;
                
                //TODO: Useful?
                var connectionCount = 0;
                if (upRiver) connectionCount++;
                if (downRiver) connectionCount++;
                if (leftRiver) connectionCount++;
                if (rightRiver) connectionCount++;

                //Check if we are on a boundary and if we want to connect that direction.
                if (upTile == null && baseTile.RiverTileConnection.ConnectedUp)
                    upRiver = true;
                else if (leftTile == null && baseTile.RiverTileConnection.ConnectedLeft)
                    leftRiver = true;
                else if (rightTile == null && baseTile.RiverTileConnection.ConnectedRight)
                    rightRiver = true;
                else if (downTile == null && baseTile.RiverTileConnection.ConnectedDown)
                    downRiver = true;

                riverSprite.SetOrientedSprite(leftRiver, rightRiver, upRiver, downRiver);
            }
            else
            {
                if (riverSprite != null)
                    riverSprite.ClearSprite();
            }
        }
    }

    public void OnTileHighlightChanged(bool highlighted)
    {
        if (!isActivated && BaseLayer != null)
            BaseLayer.GetComponent<SpriteRenderer>().color = highlighted? Color.gray : Color.white;
    }

    public void OnTileActivationChanged(bool activated)
    {
        if (BaseLayer != null)
            BaseLayer.GetComponent<SpriteRenderer>().color = activated ? Color.black : Color.white;
    }

    private void CreateRiverSprite()
    {
        if (riverSprite == null)
        {
            riverSprite = new NeighborOrientedTileSprite(
                new OrientedSpriteNames()
                {
                    BendSprite = Constants.TILE_RIVER_BEND,
                    CrossSprite = Constants.TILE_RIVER_CROSS,
                    HorSprite = Constants.TILE_RIVER_HOR,
                    HorTermSprite = Constants.TILE_RIVER_TERM_HOR,
                    SingleSprite = Constants.TILE_RIVER_SINGLE,
                    TSprite = Constants.TILE_RIVER_T,
                    VertSprite = Constants.TILE_RIVER_VERT,
                    VertTermSprite = Constants.TILE_RIVER_TERM_VERT
                },
                2, BaseLayer);
        }
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