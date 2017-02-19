using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ContextMenuView {

    private List<ContextAction> actions { get; set; }
    public GameObject GameObject = null;

    // Use this for initialization
    public ContextMenuView(GameObject parent)
    {
        GameObject = UIHelper.GetRectImageGameObject(100, 100, new Color(.2f, .2f, .2f, .95f),
            "ContextMenu", parent.transform);
        var vertGroup = GameObject.AddComponent<VerticalLayoutGroup>();
        vertGroup.childForceExpandHeight = true;
        vertGroup.childForceExpandWidth = true;
        vertGroup.childAlignment = TextAnchor.UpperLeft;
        GameObject.SetActive(false);
    }

    public void ShowMenu(List<ContextAction> actionsArg, Vector3 position)
    {
        actions = actionsArg;
        ClearInterface();
        BuildInterface(position);
    }

    public void HideMenu()
    {
        ClearInterface();
        actions = null;
        GameObject.SetActive(false);
    }

    private void ClearInterface()
    {
        foreach(Transform child in GameObject.transform)
        {
            GameObject.Destroy(child.gameObject);
        }
    }

    private void BuildInterface(Vector3 position)
    {        
        //If we dont have any actions to perform dont show the interface.
        if (actions == null || actions.Count == 0)
            return;

        GameObject.SetActive(true);

        position.x += 10f;
        position.y += 10f;

        GameObject.transform.position = position;
        int longestMenuItem = 0;
        foreach (ContextAction action in actions)
        {
            if (action.DisplayText.Length > longestMenuItem)
                longestMenuItem = action.DisplayText.Length;
            var button = UIHelper.CreateButton("ContextItem", GameObject.transform, action.DisplayText,
                () => { action.TheAction(); HideMenu(); }, 100, 18);
            button.transform.SetParent(GameObject.transform);
        }
        GameObject.GetComponent<RectTransform>().sizeDelta = new Vector2((longestMenuItem + 2) * 7, 20 * actions.Count);
    }
}
