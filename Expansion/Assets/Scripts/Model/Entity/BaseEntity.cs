using System;

public abstract class BaseEntity
{
    public EventHandler EntityDataChanged;

    public void RegisterForEntityChanged(EventHandler onEntityChangedHandler)
    {
        EntityDataChanged += onEntityChangedHandler;
    }

    public void UnRegisterForEntityChanged(EventHandler onEntityChangedHandler)
    {
        EntityDataChanged -= onEntityChangedHandler;
    }

    protected void OnEntityDataChanged()
    {
        var handler = EntityDataChanged;
        if (handler != null)
            handler(this, new EventArgs());
    }
}
