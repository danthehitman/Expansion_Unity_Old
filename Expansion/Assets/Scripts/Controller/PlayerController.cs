using System.Collections.Generic;
using UnityEngine;

public class PlayerController
{
    private PlayerEntity PlayerE;
    private PlayerView PlayerV;
    private World world;

    public PlayerController(PlayerEntity player, World world)
    {
        PlayerE = player;
        PlayerV = new PlayerView(PlayerE);
        this.world = world;
    }

    public void MovePlayer(List<TileDirectionEnum> directions)
    {
        if (directions.Count < 1)
            return;

        float x = PlayerE.X;
        int absX = Mathf.FloorToInt(x);
        float y = PlayerE.Y;
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
            PlayerE.SetPosition(x, y);
    }
}
