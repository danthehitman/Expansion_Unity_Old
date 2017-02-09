using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    private Vector3 MouseLastPosition;
    private int LastWorldX;
    private int LastWorldY;
    private float LastLeftClickTime = 0;
    private float LastRightClickDown = 0;

    // Ìs panning the camera
    private bool isPanning = false;
    private float panningThreshold = .015f;
    private Vector3 panningMouseStart = Vector3.zero;

    // Use this for initialization
    void Start () {
    }
    
    // Update is called once per frame
    void Update () {
        HandleMouseUpdates();
        HandleKeyboardUpdates();
    }

    private void HandleKeyboardUpdates()
    {
        List<TileDirectionEnum> directions = new List<TileDirectionEnum>();

        if (Input.GetKey(KeyCode.W))
        {
            directions.Add(TileDirectionEnum.Up);
        }
        if (Input.GetKey(KeyCode.A))
        {
            directions.Add(TileDirectionEnum.Left);
        }
        if (Input.GetKey(KeyCode.S))
        {
            directions.Add(TileDirectionEnum.Down);
        }
        if (Input.GetKey(KeyCode.D))
        {
            directions.Add(TileDirectionEnum.Right);
        }
        WorldController.Instance.OnMovementKeyPressed(directions);
    }

    private void HandleMouseUpdates()
    {
        if (RectTransformUtility.RectangleContainsScreenPoint(
            WorldController.Instance.ContextPanel as RectTransform, Input.mousePosition))
        {
            return;
        }

        var mouseCurrentPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        int currentXFloor = Mathf.FloorToInt(mouseCurrentPosition.x + 0.5f);
        int currentYFloor = Mathf.FloorToInt(mouseCurrentPosition.y + 0.5f);
        
        if (Vector3.Distance(panningMouseStart, mouseCurrentPosition) > panningThreshold * Camera.main.orthographicSize)
        {
            isPanning = true;
        }

        //Handle screen drag.
        if (Input.GetMouseButton(0) && isPanning)
        {
            Vector3 diff = MouseLastPosition - mouseCurrentPosition;
            Camera.main.transform.Translate(diff);
        }

        if (!Input.GetMouseButton(0))
        {
            isPanning = false;
        }

        if (Input.GetMouseButtonUp(1))
        {
            WorldController.Instance.OnWorldCoordinateMenuClick(currentXFloor, currentYFloor);
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

        Camera.main.orthographicSize -= Camera.main.orthographicSize * Input.GetAxis("Mouse ScrollWheel") * 2;
        Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize, 3f, 200f);

        if (isPanning)
            WorldController.Instance.OnMapPanning();
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
        LastWorldX = Mathf.FloorToInt(MouseLastPosition.x + 0.5f);
        LastWorldY = Mathf.FloorToInt(MouseLastPosition.y + 0.5f);
    }
}
