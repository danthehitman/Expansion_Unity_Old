using UnityEngine;

public static class ViewUtilities
{
    public static GameObject GenerateViewObject(string spriteName, string objectName, int x, int y, int sortOrder, string sortingLayerName,
        bool spriteEnabled = false)
    {
        var NewObject = new GameObject();
        var renderer = NewObject.AddComponent<SpriteRenderer>();
        renderer.enabled = spriteEnabled;
        renderer.sortingOrder = sortOrder;
        renderer.sortingLayerName = sortingLayerName;
        NewObject.name = objectName;
        renderer.sprite = SpriteManager.Instance.GetSpriteByName(spriteName);
        NewObject.transform.position = new Vector3(x, y, 0);
        return NewObject;
    }

    public static GameObject GenerateViewObject(string spriteName, string objectName, GameObject parent, int sortOrder, string sortingLayerName,
        bool spriteEnabled = false)
    {
        var NewObject = new GameObject();
        var renderer = NewObject.AddComponent<SpriteRenderer>();
        renderer.enabled = spriteEnabled;
        renderer.sortingOrder = sortOrder;
        if(sortingLayerName != null)
            renderer.sortingLayerName = sortingLayerName;
        NewObject.name = objectName;
        renderer.sprite = SpriteManager.Instance.GetSpriteByName(spriteName);
        NewObject.transform.parent = parent.transform;
        NewObject.transform.localPosition = Vector3.zero;
        return NewObject;
    }

    public static GameObject GenerateViewObject(string spriteName, string objectName, GameObject parent, int sortOrder, string sortingLayerName,
        Vector3 position, bool spriteEnabled = false)
    {
        var NewObject = GenerateViewObject(spriteName, objectName, parent, sortOrder, sortingLayerName, spriteEnabled);
        NewObject.transform.localPosition = position;
        return NewObject;
    }

    public static GameObject GenerateContainerViewObject(string objectName, int x, int y, string sortingLayerName, GameObject parent = null)
    {
        var NewObject = new GameObject();
        var renderer = NewObject.AddComponent<SpriteRenderer>();
        renderer.enabled = false;
        renderer.sortingLayerName = sortingLayerName;
        NewObject.name = objectName;
        NewObject.transform.position = new Vector3(x, y, 0);
        return NewObject;
    }
}
