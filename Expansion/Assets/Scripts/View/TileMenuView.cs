using System;
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
        gameObject.SetActive(true);
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
        var position = Camera.main.WorldToScreenPoint(new Vector3(Tile.X, Tile.Y, 0));
        position.x += 10f;
        position.y += 10f;

        gameObject.transform.position = position;

        foreach (ContextAction action in Tile.GetActions())
        {
            var button = CreateButton(action.DisplayText, action.TheAction);
            button.transform.SetParent(gameObject.transform);
        }
    }

    private GameObject CreateButton(string text, Action click)
    {
        GameObject button = new GameObject();
        button.transform.parent = gameObject.transform;
        button.AddComponent<RectTransform>();
        button.AddComponent<Button>();
        button.transform.position = gameObject.transform.position;
        button.GetComponent<Button>().onClick.AddListener(()=>{ click(); });
        button.AddComponent<Text>();
        var rect = button.GetComponent<RectTransform>().sizeDelta = new Vector2(100, 100);
        var display = button.GetComponent<Text>();
        display.text = text;
        Font ArialFont = (Font)Resources.GetBuiltinResource(typeof(Font), "Arial.ttf");
        display.font = ArialFont;

        return button;
    }
}
