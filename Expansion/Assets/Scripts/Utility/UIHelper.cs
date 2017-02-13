using UnityEngine;
using UnityEngine.UI;

public class UIHelper
{
    public static GameObject GetRectImageGameObject(int width, int height, Color color, string name)
    {
        var go = new GameObject();
        go.name = name;
        var rect = go.AddComponent<RectTransform>();
        rect.sizeDelta = new Vector2(width, height);
        AddSolidColorImageToGameObject(go, width, height, color);
        return go;
    }

    public static Image AddSolidColorImageToGameObject(GameObject go, int width, int height, Color color)
    {
        var image = go.AddComponent<Image>();
        image.sprite = Sprite.Create(UIHelper.GetBackgroundTexture(width, height, color),
            new Rect(0, 0, width, height), new Vector2(0, 0f), 1f);
        return image;
    }

    public static Texture2D GetBackgroundTexture(int width, int height, Color color)
    {
        var texture = new Texture2D(width, height);
        texture.filterMode = FilterMode.Point;
        var pixels = new Color[width * height];

        for (var x = 0; x < width; x++)
        {
            for (var y = 0; y < height; y++)
            {
                pixels[x + y * width] = color;
            }
        }

        texture.SetPixels(pixels);
        texture.wrapMode = TextureWrapMode.Clamp;
        texture.Apply();
        return texture;
    }
}
