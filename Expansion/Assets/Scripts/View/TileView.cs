using System.ComponentModel;
using UnityEngine;

public class TileView
{
    public GameObject HighlightLayer;
    private bool isHighlighted;
    private bool isActivated;
    private float tileGreyscale;
    private System.Random rng;

    public BaseTile BaseTile { get; set; }

    public bool IsHighlighted
    {
        get
        {
            return isHighlighted;
        }

        set
        {
            isHighlighted = value;
            OnTileHighlightChanged(value);
        }
    }

    public bool IsActivated
    {
        get
        {
            return isActivated;
        }

        set
        {
            isActivated = value;
            OnTileActivationChanged(value);
        }
    }

    public TileView(BaseTile tile, System.Random rand)
    {
        rng = rand;
        BaseTile = tile;
        HighlightLayer = new GameObject();
        HighlightLayer.transform.position = new Vector3(tile.X, tile.Y, 0);
        HighlightLayer.AddComponent<SpriteRenderer>();
        var baseRenderer = HighlightLayer.GetComponent<SpriteRenderer>();
        baseRenderer.sortingLayerName = Constants.TILE_SORTING_LAYER;
        baseRenderer.sortingOrder = 1;
        baseRenderer.sprite = ViewUtilities.GetTileSprite(tile, rng);
        tileGreyscale = GetGreyscaleForTile();
        baseRenderer.name = baseRenderer.sprite.name;
        if (tile.TerrainData.HeightType != HeightType.River)
        {
            if (tile.TerrainData.HeightType > HeightType.ShallowWater)
            {
                var BorderLayer = new GameObject();
                BorderLayer.transform.position = new Vector3(tile.X, tile.Y, 0);
                BorderLayer.AddComponent<SpriteRenderer>();
                var borderRenderer = BorderLayer.GetComponent<SpriteRenderer>();
                borderRenderer.sortingLayerName = Constants.TILE_SORTING_LAYER;
                borderRenderer.sortingOrder = 5;
                var texture = GetBorderTexture(BaseTile.TerrainData.HeightValue);
                Sprite borderSprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f), 32f);
                borderRenderer.sprite = borderSprite;
            }
            else
            {
                HighlightLayer.GetComponent<SpriteRenderer>().color = new Color(tileGreyscale, tileGreyscale, tileGreyscale);
            }
            //float hoursPercent = (float)(world.GetMinuteInDay() - 300) / (float)(1*/200 - 300) * 100.0f;
            //float shadowValue = (float)(0 - 220) * hoursPercent / 100.0f + 0.0f;
            //HighlightLayer.GetComponent<SpriteRenderer>().color = new Color(tileGreyscale, tileGreyscale, tileGreyscale);
        }

        //TODO: Need something other than the grasstile here for this.  Right now the bas tile is used for highlighting so maybe just
        // an empty sprite or figure out a different way to do it.
        //baseRenderer.sprite = SpriteManager.Instance.GetSpriteByName(Constants.TILE_GRASSLAND);
        //baseRenderer.color = new Color(1f, 1f, 1f, 0.0f);

        tile.PropertyChanged += OnTileModelDataChanged;
    }

    public virtual void OnTileModelDataChanged(object sender, PropertyChangedEventArgs e)
    {
    }

    public void OnTileHighlightChanged(bool highlighted)
    {
        if (!isActivated && HighlightLayer != null)
        {
            if (BaseTile.TerrainData.HeightType > HeightType.ShallowWater)
            {
                HighlightLayer.GetComponent<SpriteRenderer>().color = highlighted ? Color.gray : Color.white;
            }
            else
            {
                HighlightLayer.GetComponent<SpriteRenderer>().color = HighlightLayer.GetComponent<SpriteRenderer>().color = highlighted ? Color.gray :
                    new Color(tileGreyscale, tileGreyscale, tileGreyscale);
            }
        }

    }

    public void OnTileActivationChanged(bool activated)
    {
        if (HighlightLayer != null)
            HighlightLayer.GetComponent<SpriteRenderer>().color = activated ? Color.black : Color.white;
    }

    public TileView GetTileViewAtDirection(TileDirectionEnum direction)
    {
        switch (direction)
        {
            case TileDirectionEnum.Left:
                return WorldController.Instance.GetTileViewAt(BaseTile.X - 1, BaseTile.Y);
            case TileDirectionEnum.Right:
                return WorldController.Instance.GetTileViewAt(BaseTile.X + 1, BaseTile.Y);
            case TileDirectionEnum.Up:
                return WorldController.Instance.GetTileViewAt(BaseTile.X, BaseTile.Y + 1);
            case TileDirectionEnum.Down:
                return WorldController.Instance.GetTileViewAt(BaseTile.X, BaseTile.Y - 1);
            default:
                return null;
        }
    }

    private float GetGreyscaleForTile()
    {
        float result = 1.0f;
        if (BaseTile.TerrainData.HeightType > HeightType.ShallowWater)
        { 
            result = (((float)(BaseTile.TerrainData.HeightValue - Generator.ShallowWater) / (float)(1.0f - Generator.ShallowWater))) * 100.0f;
            result = 1.0f - ((float)(0.25f - 1.0f) * result / 100.0f + 0.25f);
        }
        else
        {
            result = ((float)(BaseTile.TerrainData.HeightValue - 0f) / (float)(Generator.ShallowWater - 0f));
        }
        return result;
    }

    private Texture2D GetBorderTexture(float greyScaleVal)
    {
        int width = 32, height = 32;
        var texture = new Texture2D(width, height);
        texture.filterMode = FilterMode.Point;
        var pixels = new Color[width * height];

        for (var x = 0; x < width; x++)
        {
            for (var y = 0; y < height; y++)
            {
                if (x == 0 || y == 0 || x == width -1 || y == width -1)
                {
                    pixels[x + y * width] = new Color(greyScaleVal, greyScaleVal, greyScaleVal, 1.0f);
                }
                else
                {
                    pixels[x + y * width] = new Color(0, 0, 0, 0);
                }
            }
        }

        texture.SetPixels(pixels);
        texture.wrapMode = TextureWrapMode.Clamp;
        texture.Apply();
        return texture;
    }
}