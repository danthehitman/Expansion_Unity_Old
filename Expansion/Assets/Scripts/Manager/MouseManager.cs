using UnityEngine;

public class MouseManager : MonoBehaviour
{ 
    private Vector3 MouseLastPosition;
    private int LastWorldX;
    private int LastWorldY;
    private float LastLeftClickTime = 0;

    // Use this for initialization
    void Start () {
    }
    
    // Update is called once per frame
    void Update () {
        var mouseCurrentPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        int currentXFloor = Mathf.FloorToInt(mouseCurrentPosition.x);
        int currentYFloor = Mathf.FloorToInt(mouseCurrentPosition.y);

        //Handle screen drag.
        if (Input.GetMouseButton(2))
        {
            Vector3 diff = MouseLastPosition - mouseCurrentPosition;
            Camera.main.transform.Translate(diff);
        }
        //Handle left click.
        if (Input.GetMouseButtonUp(0))
        {
            //Check for double click.
            if (Time.time - LastLeftClickTime < 0.25f)
                WorldController.Instance.OnWorldCoordinateDoubleClick(currentXFloor, currentYFloor);
            else
                WorldController.Instance.OnWorldCoordinateActivated(currentXFloor, currentYFloor);

            LastLeftClickTime = Time.time;
        }
        
        CheckForOverWorldTileChangeAndNotify(currentXFloor, currentYFloor);
        SetLastMousePosition();
    }

    //Check to see if we are over a different tile than we were in the last update.
    private void CheckForOverWorldTileChangeAndNotify(int currentXFloor, int currentYFloor)
    {
        if (LastWorldX != currentXFloor || LastWorldY != currentYFloor)
            WorldController.Instance.OnMouseOverWorldCoordinateChanged(currentXFloor, currentYFloor);
    }

    private void SetLastMousePosition()
    {
        MouseLastPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        LastWorldX = Mathf.FloorToInt(MouseLastPosition.x);
        LastWorldY = Mathf.FloorToInt(MouseLastPosition.y);
    }
}
