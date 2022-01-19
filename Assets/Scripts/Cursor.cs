using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cursor : MapElement
{
    [SerializeField] private Camera Camera;

    public bool followMouse;
    public bool followKeyboard;
    public bool selected;

    // Start is called before the first frame update
    void Start()
    {
        cellPosition = new Vector3Int(0, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {

        if (followKeyboard)
        {
            if (Input.GetKeyDown(KeyCode.W) && cellPosition.y > 0)
            {
                cellPosition.y--;
                SetWorldPosition(cellPosition, game);
            }
            if (Input.GetKeyDown(KeyCode.S) && cellPosition.y < game.GetMapHeight() - 1)
            {
                cellPosition.y++;
                SetWorldPosition(cellPosition, game);
            }
            if (Input.GetKeyDown(KeyCode.A) && cellPosition.x > 0)
            {
                cellPosition.x--;
                SetWorldPosition(cellPosition, game);
            }
            if (Input.GetKeyDown(KeyCode.D) && cellPosition.x < game.GetMapWidth() - 1)
            {
                cellPosition.x++;
                SetWorldPosition(cellPosition, game);
            }
        }

        if (followMouse)
            FollowMouse();
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
}
