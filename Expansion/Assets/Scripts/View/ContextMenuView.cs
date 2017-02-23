using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ContextMenuView {

    private List<ContextAction> actions { get; set; }
    private GameObject Parent;
    public GameObject GameObject = null;

    // Use this for initialization
    public ContextMenuView(GameObject parent)
    {
        Parent = parent;
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
    }

    private void ClearInterface()
    {
        if (GameObject != null)
        {
            foreach (Transform child in GameObject.transform)
            {
                GameObject.Destroy(child.gameObject);
            }
            GameObject.Destroy(GameObject);
        }
    }

    private void BuildInterface(Vector3 position)
    {

        GameObject = UIHelper.GetRectImageGameObject(1, 1, new Color(.2f, .2f, .2f, .95f),
            "ContextMenu", Parent.transform);
        var vertGroup = GameObject.AddComponent<VerticalLayoutGroup>();
        vertGroup.childForceExpandHeight = true;
        vertGroup.childForceExpandWidth = true;
        vertGroup.childAlignment = TextAnchor.UpperLeft;
        GameObject.SetActive(false);

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
