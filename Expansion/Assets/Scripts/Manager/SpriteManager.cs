using System.Linq;
using UnityEngine;

public sealed class SpriteManager
{
    private static SpriteManager instance = null;

    private SpriteManager()
    {
        sprites = Resources.LoadAll<Sprite>("Sprites");
    }

    public static SpriteManager Instance
    {
        get
        {
            if (instance == null)
                instance = new SpriteManager();
            return instance;
        }
    }

    Sprite[] sprites;

    public Sprite GetSpriteByName(string name)
    {
        return sprites.FirstOrDefault(s => s.name == name);
    }
}
