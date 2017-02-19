using System.ComponentModel;
using UnityEngine;

public class PlayerView
{
    private GameObject PlayerObject;
    private GameObject ShadowObject;
    private SpriteRenderer ShadowRenderer;
    private HumanEntity Player;

    // Use this for initialization
    public PlayerView(HumanEntity player)
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
        var player = sender as HumanEntity;
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
            if (world.GetMinuteInDay() <= 300 || world.GetMinuteInDay() >= 1200)
            {
                ShadowRenderer.enabled = false;
            }
            else
            {
                float hoursPercent = (float)(world.GetMinuteInDay() - 300) / (float)(1200 - 300) * 100.0f;
                float shadowValue = (float)(0 - 220) * hoursPercent / 100.0f + 0.0f;
                float fadeValue = 0;
                if (world.GetMinuteInDay() > 300 && world.GetMinuteInDay() < 720)
                {
                    fadeValue = (float)(world.GetMinuteInDay() - 300) / (float)(720 - 300);
                    ShadowRenderer.color = new Color(1f, 1f, 1f, Mathf.Clamp(fadeValue, 0.2f, 1f));
                }
                else if (world.GetMinuteInDay() >= 720 && world.GetMinuteInDay() < 1200)
                {
                    fadeValue = 1.0f - ((float)(world.GetMinuteInDay() - 720) / (float)(1200 - 720));
                    ShadowRenderer.color = new Color(1f, 1f, 1f, Mathf.Clamp(fadeValue, 0.2f, 1f));
                }
                else
                    ShadowRenderer.color = new Color(1f, 1f, 1f, 1f);

                ShadowRenderer.enabled = true;
                ShadowRenderer.transform.eulerAngles = new Vector3(0, 0, -shadowValue);
                ShadowRenderer.transform.localScale = new Vector3(1- fadeValue, 1 - fadeValue, 1f);
            }
        }
    }
}