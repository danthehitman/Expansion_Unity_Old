using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryView
{
    private GameObject parentGo;

    public int Height { get; set; }
    public int Width { get; set; }

    public InventoryView(GameObject parent, int height, int width)
    {
        parentGo = parent;
        Height = height;
        Width = width;
    }

    public void BuildInventoryView()
    {
        GameObject window = new GameObject();
        window.transform.parent = parentGo.transform;
        window.AddComponent<RectTransform>();

        window.transform.position = parentGo.transform.position;

        var rect = window.GetComponent<RectTransform>();
        rect.sizeDelta = new Vector2(200, 200);
        var image = window.AddComponent<Image>();
        image.sprite = Sprite.Create(GetBackgroundTexture(), new Rect(0, 0, Width, Height), new Vector2(0.5f, 0.5f), 32f);
    }

    private Texture2D GetBackgroundTexture()
    {
        var texture = new Texture2D(Width, Height);
        texture.filterMode = FilterMode.Point;
        var pixels = new Color[Width * Height];

        for (var x = 0; x < Width; x++)
        {
            for (var y = 0; y < Height; y++)
            {
                pixels[x + y * Width] = Color.black;
            }
        }

        texture.SetPixels(pixels);
        texture.wrapMode = TextureWrapMode.Clamp;
        texture.Apply();
        return texture;
    }
}