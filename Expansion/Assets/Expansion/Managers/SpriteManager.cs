using System.Linq;
using UnityEngine;

public class SpriteManager
{
    Sprite[] sprites;

    public SpriteManager()
    {
        sprites = Resources.LoadAll<Sprite>("Sprites");
    }

    public Sprite GetSpriteByName(string name)
    {
        return sprites.FirstOrDefault(s => s.name == name);
    }
}
