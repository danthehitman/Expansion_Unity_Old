using System.Collections.Generic;
using UnityEngine;

public class EntityController
{
    public BaseEntity ActiveEntity { get; set; }

    private Dictionary<string, BaseEntity> entities;
    private PlayerView PlayerV;
    private World world;

    public EntityController(PlayerEntity player, World world)
    {
        ActiveEntity = player;
        entities = new Dictionary<string, BaseEntity>();
        entities.Add("ThePlayer", player);
        PlayerV = new PlayerView(player);
        this.world = world;
    }

    public void MoveEntity(List<TileDirectionEnum> directions)
    {
        if (directions.Count < 1)
            return;

        float x = ActiveEntity.X;
        int absX = Mathf.FloorToInt(x);
        float y = ActiveEntity.Y;
        int absY = Mathf.FloorToInt(y);

        foreach (var direction in directions)
        {
            switch (direction)
            {
                case TileDirectionEnum.Left:
                    x -= 0.01f;
                    absX = Mathf.FloorToInt(x +0.5f - 0.1f);
                    break;
                case TileDirectionEnum.Right:
                    x += 0.01f;
                    absX = Mathf.FloorToInt(x + 0.5f + 0.1f);
                    break;
                case TileDirectionEnum.Up:
                    y += 0.01f;
                    absY = Mathf.FloorToInt(y + 0.5f + 0.2f);
                    break;
                case TileDirectionEnum.Down:
                    y -= 0.01f;
                    absY = Mathf.FloorToInt(y + 0.5f - 0.2f);
                    break;
            }
        }

        if (world.GetTileAt(absX, absY) != null)
            ActiveEntity.SetPosition(x, y);
    }

    public void MoveEntityToward(int x, int y, BaseEntity entity)
    {
        MoveEntityToward(new Vector3(x, y, 0), entity);
    }

    public void MoveEntityToward(Vector3 target, BaseEntity entity)
    {
        var entityPosition = new Vector3(entity.X, entity.Y, 0);
        var direction = target - entityPosition;
        var newPosition = Vector3.MoveTowards(entityPosition, entityPosition + direction, entity.GetMaxSpeed() * Time.deltaTime);
        entity.SetPosition(newPosition.x, newPosition.y);
    }



    internal void UpdateEntities()
    {
        foreach (var entity in entities)
        {
            entity.Value.Update();
        }
    }
}
