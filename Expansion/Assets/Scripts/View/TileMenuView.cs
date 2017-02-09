using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class TileMenuView : MonoBehaviour {

    public BaseTile Tile { get; set; }

    // Use this for initialization
    void Start () {
        gameObject.SetActive(false);
    }
    
    // Update is called once per frame
    void Update () {
        
    }

    public void ShowMenu(BaseTile baseTile)
    {
        Tile = baseTile;
        ClearInterface();
        BuildInterface();
    }

    public void HideMenu()
    {
        ClearInterface();
        Tile = null;
        gameObject.SetActive(false);
    }

    private void ClearInterface()
    {
        foreach(Transform child in transform)
        {
            GameObject.Destroy(child.gameObject);
        }
    }

    private void BuildInterface()
    {
        var actions = Tile.GetActions(WorldController.Instance.EntityController.ActiveEntity).ToList();
        //If we dont have any actions to perform dont show the interface.
        if (actions == null || actions.Count == 0)
            return;

        gameObject.SetActive(true);
        var position = Camera.main.WorldToScreenPoint(new Vector3(Tile.X, Tile.Y, 0));
        position.x += 10f;
        position.y += 10f;

        gameObject.transform.position = position;
        int longestMenuItem = 0;
        foreach (ContextAction action in actions)
        {
            if (action.DisplayText.Length > longestMenuItem)
                longestMenuItem = action.DisplayText.Length;
            var button = CreateButton(action.DisplayText, action.TheAction);
            button.transform.SetParent(gameObject.transform);
        }
        gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2((longestMenuItem + 2) * 7, 20 * actions.Count);
    }

    private GameObject CreateButton(string text, Action click)
    {
        GameObject button = new GameObject();
        button.transform.parent = gameObject.transform;
        button.AddComponent<RectTransform>();
        button.AddComponent<Button>();
        button.transform.position = gameObject.transform.position;
        button.GetComponent<Button>().onClick.AddListener(()=>{ click(); HideMenu(); });
        var display = button.AddComponent<Text>();
        var rect = button.GetComponent<RectTransform>();
        rect.sizeDelta = new Vector2(100, 18);
        display.horizontalOverflow = HorizontalWrapMode.Overflow;
        display.alignment = TextAnchor.MiddleLeft;
        display.text = "  " + text;
        Font ArialFont = (Font)Resources.GetBuiltinResource(typeof(Font), "Arial.ttf");
        display.font = ArialFont;
        display.color = Color.black;

        return button;
    }
}
