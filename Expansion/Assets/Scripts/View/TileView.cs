using System;
using UnityEngine;

public abstract class TileView {
    public GameObject BaseLayer;

    private bool isHighlighted;
    private bool isActivated;

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
        tile.RegisterForTileChanged(OnTileModelDataChanged);
    }

    public abstract void OnTileModelDataChanged(object sender, EventArgs e);

    public void OnTileHighlightChanged(bool highlighted)
    {
        if (!isActivated)
            BaseLayer.GetComponent<SpriteRenderer>().color = highlighted? Color.gray : Color.white;
    }

    public void OnTileActivationChanged(bool activated)
    {
        BaseLayer.GetComponent<SpriteRenderer>().color = activated ? Color.black : Color.white;
    }
}