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

    public void MovePlayer(List<MoveDirectionEnum> directions)
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
                case MoveDirectionEnum.Left:
                    x -= 0.01f;
                    absX = Mathf.FloorToInt(x);
                    break;
                case MoveDirectionEnum.Right:
                    x += 0.01f;
                    absX = Mathf.CeilToInt(x);
                    break;
                case MoveDirectionEnum.Up:
                    y += 0.01f;
                    absY = Mathf.CeilToInt(y);
                    break;
                case MoveDirectionEnum.Down:
                    y -= 0.01f;
                    absY = Mathf.FloorToInt(y);
                    break;
            }
        }

        if (world.GetTileAt(absX, absY) != null)
            PlayerE.SetPosition(x, y);
    }
}
