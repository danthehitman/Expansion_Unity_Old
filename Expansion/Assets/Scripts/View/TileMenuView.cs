using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class TileMenuView : MonoBehaviour {

    // Use this for initialization
    void Start () {
        
    }
    
    // Update is called once per frame
    void Update () {
        
    }

    public void ShowMenu(BaseTile baseTile)
    {
        gameObject.SetActive(true);
        BuildInterface(baseTile.GetActions().ToList());
    }

    private void BuildInterface(List<ContextAction> contextualActions)
    {
        gameObject.transform.position = Input.mousePosition + new Vector3(10, -10, 0);        

        foreach (ContextAction action in contextualActions)
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
        button.GetComponent<Button>().onClick.AddListener(()=>{ click.Invoke(); });
        button.AddComponent<Text>();
        var rect = button.GetComponent<RectTransform>().sizeDelta = new Vector2(100, 100);
        var display = button.GetComponent<Text>();
        display.text = text;
        Font ArialFont = (Font)Resources.GetBuiltinResource(typeof(Font), "Arial.ttf");
        display.font = ArialFont;

        return button;
    }
}
