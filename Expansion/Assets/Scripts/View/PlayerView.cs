using System;
using UnityEngine;

public class PlayerView
{
    private GameObject PlayerObject;

    // Use this for initialization
    public PlayerView(PlayerEntity player)
    {
        PlayerObject = ViewUtilities.GenerateContainerViewObject("Player", Mathf.FloorToInt(player.X), Mathf.FloorToInt(player.Y), Constants.PLAYER_SORTING_LAYER);
        ViewUtilities.GenerateViewObject(Constants.PLAYER_TORSO_1, Constants.PLAYER_TORSO_1, PlayerObject, 0, Constants.PLAYER_SORTING_LAYER, true);
        ViewUtilities.GenerateViewObject(Constants.PLAYER_ARMS_1, Constants.PLAYER_ARMS_1, PlayerObject, 0, Constants.PLAYER_SORTING_LAYER, true);
        ViewUtilities.GenerateViewObject(Constants.PLAYER_LEGS, Constants.PLAYER_LEGS, PlayerObject, 0, Constants.PLAYER_SORTING_LAYER, true);
        ViewUtilities.GenerateViewObject(Constants.PLAYER_HEAD, Constants.PLAYER_HEAD, PlayerObject, 1, Constants.PLAYER_SORTING_LAYER, true);
        ViewUtilities.GenerateViewObject(Constants.PLAYER_FACE_1, Constants.PLAYER_FACE_1, PlayerObject, 2, Constants.PLAYER_SORTING_LAYER, true);
        ViewUtilities.GenerateViewObject(Constants.PLAYER_SHORT_HAIR, Constants.PLAYER_SHORT_HAIR, PlayerObject, 3, Constants.PLAYER_SORTING_LAYER, true);

        player.RegisterForEntityChanged(OnEntityModelDataChanged);

        OnEntityModelDataChanged(player, new EventArgs());
    }

    public void OnEntityModelDataChanged(object sender, EventArgs e)
    {
        var player = sender as PlayerEntity;
        if (player != null)
        {
            PlayerObject.transform.position = new Vector3(player.X, player.Y);
        }
    }
}