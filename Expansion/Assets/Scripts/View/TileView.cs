using System;
using UnityEngine;

public abstract class TileView {
    public GameObject BaseLayer;

    private GameObject riverLayer;
    private SpriteRenderer riverRenderer;
    private bool isHighlighted;
    private bool isActivated;
    private float currentRiverSpriteRotation = 0;
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
        BaseLayer.AddComponent<SpriteRenderer>();
        SetRiverTile();
        tile.RegisterForTileChanged(OnTileModelDataChanged);
    }

    public virtual void OnTileModelDataChanged(object sender, EventArgs e)
    {
        SetRiverTile();
        NotifyNeighbors();
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
        bool upRiver = false;
        bool downRiver = false;
        bool leftRiver = false;
        bool rightRiver = false;

        if (baseTile != null)
        {
            if (baseTile.HasRiver)
            {
                CreateRiverObject();

                var upTile = baseTile.GetTileAtDirection(TileDirectionEnum.Up);
                upRiver = upTile != null ? upTile.HasRiver : false;
                var downTile = baseTile.GetTileAtDirection(TileDirectionEnum.Down);
                downRiver = downTile != null ? downTile.HasRiver : false;
                var leftTile = baseTile.GetTileAtDirection(TileDirectionEnum.Left);
                leftRiver = leftTile != null ? leftTile.HasRiver : false;
                var rightTile = baseTile.GetTileAtDirection(TileDirectionEnum.Right);
                rightRiver = rightTile != null ? rightTile.HasRiver : false;

                // T Rivers
                if (upRiver && downRiver && leftRiver && !rightRiver)
                    SetRiverTSprite(TileDirectionEnum.Left);
                else if (upRiver && downRiver && rightRiver && !leftRiver)
                    SetRiverTSprite(TileDirectionEnum.Right);
                else if (downRiver && rightRiver && leftRiver && !upRiver)
                    SetRiverTSprite(TileDirectionEnum.Down);
                else if (upRiver && rightRiver && leftRiver && !downRiver)
                    SetRiverTSprite(TileDirectionEnum.Up);

                //Straight rivers
                else if (upRiver && downRiver && !leftRiver && !rightRiver)
                {
                    SetRiverSprite(Constants.TILE_RIVER_VERT);
                    SetRotation(0);
                }
                else if (leftRiver && rightRiver && !upRiver && !downRiver)
                {
                    SetRiverSprite(Constants.TILE_RIVER_HOR);
                    SetRotation(0);
                }

                //Bends
                else if (upRiver && leftRiver && !rightRiver && !downRiver)
                    SetRiverBendSprite(TileDirectionEnum.Left, TileDirectionEnum.Up);
                else if (downRiver && leftRiver && !rightRiver && !upRiver)
                    SetRiverBendSprite(TileDirectionEnum.Left, TileDirectionEnum.Down);
                else if (upRiver && rightRiver && !leftRiver && !downRiver)
                    SetRiverBendSprite(TileDirectionEnum.Right, TileDirectionEnum.Up);
                else if (downRiver && rightRiver && !leftRiver && !upRiver)
                    SetRiverBendSprite(TileDirectionEnum.Right, TileDirectionEnum.Down);

                //Terminations
                else if (upRiver && !leftRiver && !rightRiver && !downRiver)
                    SetRiverTermination(TileDirectionEnum.Down);
                else if (downRiver && !upRiver && !rightRiver && !leftRiver)
                    SetRiverTermination(TileDirectionEnum.Up);
                else if (rightRiver && !upRiver && !leftRiver && !downRiver)
                    SetRiverTermination(TileDirectionEnum.Left);
                else if (leftRiver && !upRiver && !rightRiver && !downRiver)
                    SetRiverTermination(TileDirectionEnum.Right);

                else
                    SetRiverSprite(Constants.TILE_RIVER_CROSS);
            }
            else
            {
                if (riverRenderer != null)
                    riverRenderer.sprite = null;
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

    private void CreateRiverObject()
    {
        if (riverLayer != null)
            return;
        riverLayer = new GameObject();
        riverRenderer = riverLayer.AddComponent<SpriteRenderer>();
        riverRenderer.enabled = true;
        riverRenderer.sortingOrder = 2;
        riverRenderer.sortingLayerName = Constants.TILE_SORTING_LAYER;
        riverLayer.transform.parent = BaseLayer.transform;
        riverLayer.transform.localPosition = Vector3.zero;
    }

    private void SetRiverTSprite(TileDirectionEnum tDirection)
    {
        SetRiverSprite(Constants.TILE_RIVER_T);
        switch (tDirection)
        {
            case TileDirectionEnum.Left:
                SetRotation(90);
                break;
            case TileDirectionEnum.Right:
                SetRotation(-90);
                break;
            case TileDirectionEnum.Up:
                SetRotation(0);
                break;
            case TileDirectionEnum.Down:
                SetRotation(180);
                break;
        }
    }

    private void SetRiverTermination(TileDirectionEnum direction)
    {
        switch (direction)
        {
            case TileDirectionEnum.Left:
                SetRiverSprite(Constants.TILE_RIVER_TERM_HOR);
                SetRotation(180);
                break;
            case TileDirectionEnum.Right:
                SetRiverSprite(Constants.TILE_RIVER_TERM_HOR);
                SetRotation(0);
                break;
            case TileDirectionEnum.Up:
                SetRiverSprite(Constants.TILE_RIVER_TERM_VERT);
                SetRotation(0);
                break;
            case TileDirectionEnum.Down:
                SetRiverSprite(Constants.TILE_RIVER_TERM_VERT);
                SetRotation(180);
                break;
        }
    }

    //Start is left right and end is top bottom
    private void SetRiverBendSprite(TileDirectionEnum start, TileDirectionEnum end)
    {
        SetRiverSprite(Constants.TILE_RIVER_BEND);
        if (start == TileDirectionEnum.Left || end == TileDirectionEnum.Left)
        {
            if (end == TileDirectionEnum.Up)
            {
                SetRotation(180);
            }
            else
            {
                SetRotation(-90);
            }
        }
        else if (start == TileDirectionEnum.Right || end == TileDirectionEnum.Right)
        {
            if (end == TileDirectionEnum.Up)
            {
                SetRotation(90);
            }
            else
            {
                SetRotation(0);
            }
        }
    }

    private void SetRiverSprite(string spriteName)
    {
        if (riverLayer == null)
            CreateRiverObject();
        riverRenderer.sprite = SpriteManager.Instance.GetSpriteByName(spriteName);
    }

    private void SetRotation(int rot)
    {
        if (currentRiverSpriteRotation != rot)
        {
            riverRenderer.transform.Rotate(0, 0, rot - currentRiverSpriteRotation);
            currentRiverSpriteRotation = rot;
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