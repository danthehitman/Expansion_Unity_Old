using UnityEngine;

public class NeighborOrientedTileSprite
{
    private SpriteRenderer renderer { get; set; }
    private int currentSpriteRotation = 0;
    private OrientedSpriteNames spriteNames = null;
    

    public GameObject TileGameObject { get; set; }

    public NeighborOrientedTileSprite(OrientedSpriteNames names, int sortOrder, string name, GameObject parentObject = null)
    {
        spriteNames = names;
        TileGameObject = new GameObject();
        TileGameObject.name = name;
        renderer = TileGameObject.AddComponent<SpriteRenderer>();
        renderer.enabled = true;
        renderer.sortingOrder = sortOrder;
        renderer.sortingLayerName = Constants.TILE_SORTING_LAYER;
        if (parentObject != null)
        {
            TileGameObject.transform.parent = parentObject.transform;
            TileGameObject.transform.localPosition = Vector3.zero;
        }
    }

    public void SetOrientedSprite(bool left, bool right, bool up, bool down)
    {
        // T Rivers
        if (up && down && left && !right)
            SetTSprite(TileDirectionEnum.Left);
        else if (up && down && right && !left)
            SetTSprite(TileDirectionEnum.Right);
        else if (down && right && left && !up)
            SetTSprite(TileDirectionEnum.Down);
        else if (up && right && left && !down)
            SetTSprite(TileDirectionEnum.Up);
        //Straight rivers
        else if (up && down && !left && !right)
            SetVertSprite();
        else if (left && right && !up && !down)
            SetHorSprite();
        //Bends
        else if (up && left && !right && !down)
            SetRiverBendSprite(TileDirectionEnum.Left, TileDirectionEnum.Up);
        else if (down && left && !right && !up)
            SetRiverBendSprite(TileDirectionEnum.Left, TileDirectionEnum.Down);
        else if (up && right && !left && !down)
            SetRiverBendSprite(TileDirectionEnum.Right, TileDirectionEnum.Up);
        else if (down && right && !left && !up)
            SetRiverBendSprite(TileDirectionEnum.Right, TileDirectionEnum.Down);
        //Terminations
        else if (up && !left && !right && !down)
            SetTerminationSprite(TileDirectionEnum.Down);
        else if (down && !up && !right && !left)
            SetTerminationSprite(TileDirectionEnum.Up);
        else if (right && !up && !left && !down)
            SetTerminationSprite(TileDirectionEnum.Left);
        else if (left && !up && !right && !down)
            SetTerminationSprite(TileDirectionEnum.Right);
        //Cross
        else if (left && right && up && down)
            SetCrossSprite();
        //Single
        else
            SetNoNeighborSprite();
    }

    public void SetTSprite(TileDirectionEnum direction)
    {
        SetSprite(spriteNames.TSprite);
        switch (direction)
        {
            case TileDirectionEnum.Left:
                SetRotation(90);
                break;
            case TileDirectionEnum.Right:
                SetRotation(-90);
                break;
            case TileDirectionEnum.Up:
                SetRotation(0);
                break;
            case TileDirectionEnum.Down:
                SetRotation(180);
                break;
        }
    }

    public void SetTerminationSprite(TileDirectionEnum direction)
    {
        switch (direction)
        {
            case TileDirectionEnum.Left:
                SetSprite(spriteNames.HorTermSprite);
                SetRotation(180);
                break;
            case TileDirectionEnum.Right:
                SetSprite(spriteNames.HorTermSprite);
                SetRotation(0);
                break;
            case TileDirectionEnum.Up:
                SetSprite(spriteNames.VertTermSprite);
                SetRotation(0);
                break;
            case TileDirectionEnum.Down:
                SetSprite(spriteNames.VertTermSprite);
                SetRotation(180);
                break;
        }
    }

    //Start is left right and end is top bottom
    public void SetRiverBendSprite(TileDirectionEnum start, TileDirectionEnum end)
    {
        SetSprite(spriteNames.BendSprite);
        if (start == TileDirectionEnum.Left || end == TileDirectionEnum.Left)
        {
            if (end == TileDirectionEnum.Up)
            {
                SetRotation(180);
            }
            else
            {
                SetRotation(-90);
            }
        }
        else if (start == TileDirectionEnum.Right || end == TileDirectionEnum.Right)
        {
            if (end == TileDirectionEnum.Up)
            {
                SetRotation(90);
            }
            else
            {
                SetRotation(0);
            }
        }
    }

    public void SetVertSprite()
    {
        SetSprite(spriteNames.VertSprite);
        SetRotation(0);
    }

    public void SetHorSprite()
    {
        SetSprite(spriteNames.HorSprite);
        SetRotation(0);
    }

    public void SetCrossSprite()
    {
        SetSprite(spriteNames.CrossSprite);
    } 

    public void SetNoNeighborSprite()
    {
        SetSprite(spriteNames.SingleSprite);
    }

    public void ClearSprite()
    {
        renderer.sprite = null;
    }

    private void SetSprite(string spriteName)
    {
        renderer.sprite = SpriteManager.Instance.GetSpriteByName(spriteName);
    }

    private void SetRotation(int rot)
    {
        if (currentSpriteRotation != rot)
        {
            renderer.transform.Rotate(0, 0, rot - currentSpriteRotation);
            currentSpriteRotation = rot;
        }
    }
}
