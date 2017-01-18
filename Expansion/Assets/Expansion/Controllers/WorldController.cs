using UnityEngine;

public class WorldController : MonoBehaviour {

    public int Width = 100;
    public int Height = 100;

    World world;
    SpriteManager spriteMan;

    // Use this for initialization
    void Start () {
        spriteMan = new SpriteManager();
        world = new World(Width, Height);

        for (int x = 0; x < world.Width; x++)
        {
            for (int y = 0; y < world.Height; y++)
            {
                var tileData = world.GetTileAt(x, y);
                GameObject tileObject = new GameObject();
                tileObject.name = "Tile" + x + "_" + y;
                tileObject.transform.position = new Vector3(tileData.X, tileData.Y, 0);
                var tileController = tileObject.AddComponent<TileController>();
                tileController.Tile = tileData;

                GameObject waterObject = new GameObject();
                waterObject.AddComponent<SpriteRenderer>().sprite = spriteMan.GetSpriteByName(Contants.IRRIGATION_SPRITE);
                waterObject.transform.parent = tileObject.transform;
                waterObject.transform.localPosition = Vector3.zero;

                GameObject fenceObject = new GameObject();
                fenceObject.AddComponent<SpriteRenderer>().sprite = spriteMan.GetSpriteByName(Contants.FENCE_SPRITE);
                fenceObject.transform.parent = tileObject.transform;
                fenceObject.transform.localPosition = Vector3.zero;

                GameObject grassObject = new GameObject();
                grassObject.AddComponent<SpriteRenderer>().sprite = spriteMan.GetSpriteByName(Contants.GRASS_SPRITE);
                grassObject.transform.parent = tileObject.transform;
                grassObject.transform.localPosition = Vector3.zero;
            }
        }
    }
    
    // Update is called once per frame
    void Update () {
        
    }
}
