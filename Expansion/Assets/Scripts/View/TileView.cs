using UnityEngine;

public abstract class TileView {
    public GameObject BaseLayer;

    private bool highlighted = false;
    private bool activated = false;

    public TileView(BaseTile tile)
    {
        tile.RegisterForTileChanged(OnTileChanged);
        tile.RegisterForTileHighlightChanged(OnTileHighlightChanged);
        tile.RegisterForTileActivationChanged(OnTileActivationChanged);
    }

    public abstract void OnTileChanged(BaseTile tile);

    public void OnTileHighlightChanged(bool highlighted)
    {
        this.highlighted = highlighted;
        if (!activated)
            BaseLayer.GetComponent<SpriteRenderer>().color = highlighted? Color.gray : Color.white;
    }

    public void OnTileActivationChanged(bool activated)
    {
        this.activated = activated;
        BaseLayer.GetComponent<SpriteRenderer>().color = activated ? Color.black : Color.white;
    }
}