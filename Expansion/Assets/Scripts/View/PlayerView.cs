using System.ComponentModel;
using UnityEngine;

public class PlayerView
{
    private GameObject PlayerObject;
    private GameObject ShadowObject;
    private SpriteRenderer ShadowRenderer;
    private PlayerEntity Player;

    // Use this for initialization
    public PlayerView(PlayerEntity player)
    {
        PlayerObject = ViewUtilities.GenerateContainerViewObject("Player", Mathf.FloorToInt(player.X), Mathf.FloorToInt(player.Y), Constants.PLAYER_SORTING_LAYER);
        ViewUtilities.GenerateViewObject(Constants.PLAYER_TORSO_1, Constants.PLAYER_TORSO_1, PlayerObject,
            0, Constants.PLAYER_SORTING_LAYER, true);
        ViewUtilities.GenerateViewObject(Constants.PLAYER_ARMS_1, Constants.PLAYER_ARMS_1, PlayerObject,
            0, Constants.PLAYER_SORTING_LAYER, true);
        ViewUtilities.GenerateViewObject(Constants.PLAYER_LEGS, Constants.PLAYER_LEGS, PlayerObject,
            0, Constants.PLAYER_SORTING_LAYER, true);
        ViewUtilities.GenerateViewObject(Constants.PLAYER_HEAD, Constants.PLAYER_HEAD, PlayerObject,
            1, Constants.PLAYER_SORTING_LAYER, true);
        ViewUtilities.GenerateViewObject(Constants.PLAYER_FACE_1, Constants.PLAYER_FACE_1, PlayerObject,
            2, Constants.PLAYER_SORTING_LAYER, true);
        ViewUtilities.GenerateViewObject(Constants.PLAYER_SHORT_HAIR, Constants.PLAYER_SHORT_HAIR, PlayerObject,
            3, Constants.PLAYER_SORTING_LAYER, true);
        ShadowObject = ViewUtilities.GenerateViewObject(Constants.PLAYER_SHADOW, Constants.PLAYER_SHADOW, PlayerObject,
            0, Constants.PLAYER_SORTING_LAYER, new Vector3(0, -.25f, 0), true);
        ShadowRenderer = ShadowObject.GetComponent<SpriteRenderer>();


        player.PropertyChanged += OnEntityModelDataChanged;
        WorldController.Instance.World.PropertyChanged += OnWorldModelDataChanged;

        OnEntityModelDataChanged(player, new PropertyChangedEventArgs(Constants.ALL_PROPERTIES_PROPERTY_NAME));
        OnWorldModelDataChanged(WorldController.Instance.World, new PropertyChangedEventArgs(Constants.ALL_PROPERTIES_PROPERTY_NAME));
    }

    public void OnEntityModelDataChanged(object sender, PropertyChangedEventArgs e)
    {
        var player = sender as PlayerEntity;
        if (player != null)
        {
            PlayerObject.transform.position = new Vector3(player.X, player.Y);
        }
    }

    public void OnWorldModelDataChanged(object sender, PropertyChangedEventArgs e)
    {
        var world = sender as World;
        if (e.PropertyName == World.MinutePropertyName || e.PropertyName == Constants.ALL_PROPERTIES_PROPERTY_NAME)
        {
            if (world.Minute <= 300 || world.Minute >= 1200)
            {
                ShadowRenderer.enabled = false;
            }
            else
            {
                float hoursPercent = (float)(world.Minute - 300) / (float)(1200 - 300) * 100.0f;
                float shadowValue = (float)(0 - 220) * hoursPercent / 100.0f + 0.0f;
                
                if (world.Minute > 300 && world.Minute < 720)
                {
                    var fadeValue = (float)(world.Minute - 300) / (float)(720 - 300);
                    ShadowRenderer.color = new Color(1f, 1f, 1f, fadeValue);
                }
                else if (world.Minute >= 720 && world.Minute < 1200)
                {
                    var fadeValue = 1.0f - ((float)(world.Minute - 720) / (float)(1200 - 720));
                    ShadowRenderer.color = new Color(1f, 1f, 1f, fadeValue);
                }
                else
                    ShadowRenderer.color = new Color(1f, 1f, 1f, 1f);

                ShadowRenderer.enabled = true;
                ShadowRenderer.transform.eulerAngles = new Vector3(0, 0, shadowValue);
            }
        }
    }
}