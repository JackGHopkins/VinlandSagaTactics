using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cursor : MapElement
{
    [SerializeField] private Camera Camera;

    public bool followMouse;
    public bool followKeyboard;
    public bool selected;

    private float timeSpan = 0.08f;
    private float time;


    // Start is called before the first frame update
    void Start()
    {
        cellPosition = new Vector3Int(0, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {

        KeyBoardControls();

        if (followMouse)
            FollowMouse();

        // Tool Bar
        if (Input.GetKeyDown(KeyCode.F2))
        {
            followMouse = !followMouse;
        }
        // Tool Bar
        if (Input.GetKeyDown(KeyCode.F3))
        {
            followKeyboard = !followKeyboard;
        }
    }

    private void FollowMouse()
    {
        Vector3 MousePos = Camera.ScreenToWorldPoint(Input.mousePosition);
        MousePos.y -= game.GetCellSize().y;
        MousePos.z = 0f;
        Vector3Int MapPosition = tilemap.WorldToCell(MousePos);
        if (tilemap.HasTile(MapPosition))
        {
            SetMousePosition(MapPosition, game);
            cellPosition = -tilemap.WorldToCell(MousePos);
        }
    }

    private void KeyBoardControls()
    {
        if (followKeyboard)
        {
            if (Input.GetKey(KeyCode.W) && cellPosition.y > 0 && MovementInhibiter())
            {
                cellPosition.y--;
                SetWorldPosition(cellPosition, game);
            }
            if (Input.GetKey(KeyCode.S) && cellPosition.y < game.GetMapHeight() - 1 && MovementInhibiter())
            {
                cellPosition.y++;
                SetWorldPosition(cellPosition, game);
            }
            if (Input.GetKey(KeyCode.D) && cellPosition.x > 0 && MovementInhibiter())
            {
                cellPosition.x--;
                SetWorldPosition(cellPosition, game);
            }
            if (Input.GetKey(KeyCode.A) && cellPosition.x < game.GetMapWidth() - 1 && MovementInhibiter())
            {
                cellPosition.x++;
                SetWorldPosition(cellPosition, game);
            }
        }

        if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.D))
            time = 0f;
    }

    private bool MovementInhibiter()
    {
        if (time == 0f)
        {
            time += Time.deltaTime;
            return true;
        }

        time += Time.deltaTime;

        if(time > timeSpan)
            time = 0f;
        return false;
    }
}
