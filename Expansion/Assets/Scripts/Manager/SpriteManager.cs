using System.Collections.Generic;
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

    public Dictionary<int, List<Sprite>> GetSpritesByKey(string key)
    {
        var result = new Dictionary<int, List<Sprite>>();

        var keySprites = sprites.Where(s => s.name.StartsWith(key)).ToList();

        result = keySprites.GroupBy(i => int.Parse(i.name.Substring(i.name.LastIndexOf('_') + 1)))
        .ToDictionary(gdc => gdc.Key, gdc => gdc.ToList());

        return result;
    }
}
