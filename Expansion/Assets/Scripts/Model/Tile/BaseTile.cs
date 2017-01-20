using System;

public abstract class BaseTile : IDisposable
{
    public Action<BaseTile> TileDataChanged;
    public Action<bool> TileHighlightChanged;
    public Action<bool> TileActivationChanged;
    public Action TileDisposed;

    public World World { get; set; }
    public int X { get; set; }
    public int Y { get; set; }
    public bool IsActivated { get; private set; }
    public bool IsHighlighted { get; private set; }

    public void SetActive(bool active)
    {
        IsActivated = active;
        TileActivationChanged(active);
    }

    public void SetHighlighted(bool highlighted)
    {
        IsHighlighted = highlighted;
        TileHighlightChanged(highlighted);
    }

    public void RegisterForTileChanged(Action<BaseTile> onTileChangedHandler)
    {
        TileDataChanged += onTileChangedHandler;
    }

    public void UnRegisterForTileChanged(Action<BaseTile> onTileChangedHandler)
    {
        TileDataChanged -= onTileChangedHandler;
    }

    public void RegisterForTileDisposed(Action onTileDisposedHandler)
    {
        TileDisposed += onTileDisposedHandler;
    }

    public void RegisterForTileHighlightChanged(Action<bool> handler)
    {
        TileHighlightChanged += handler;
    }

    public void RegisterForTileActivationChanged(Action<bool> handler)
    {
        TileActivationChanged += handler;
    }

    public void Dispose()
    {
        TileDisposed();
    }
}