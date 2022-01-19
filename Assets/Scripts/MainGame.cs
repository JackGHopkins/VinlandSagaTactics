using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MainGame : MonoBehaviour
{
    private bool playerTurn = true;
    private Unit curUnit;

    [SerializeField] int width = 5;
    [SerializeField] int height = 5;
    [SerializeField] Vector2 cellSize;
    [SerializeField] Vector3 mapOrigin;
    [SerializeField] Cursor cursor;
    [SerializeField] Unit[] teamPlayer;
    [SerializeField] Unit[] teamEnemy;

    public Map map;
    public Tilemap tilemap;
    // Start is called before the first frame update
    void Awake()
    {
        map = new Map(width, height, cellSize, mapOrigin);
        playerTurn = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (cursor.selected)
            {
                cursor.selected = false;
                curUnit.SetPosition(cursor.cellPosition);
                Debug.Log("Moved: " + curUnit.name + " to: " + curUnit.cellPosition);
                curUnit = null;
            }
            else
            {
                if (map.GridArray[cursor.cellPosition.x, cursor.cellPosition.y].occupied)
                {
                    cursor.selected = true;
                    curUnit = map.GridArray[cursor.cellPosition.x, cursor.cellPosition.y].currentUnit;
                    Debug.Log("Selected: " + curUnit.name + " at: " + curUnit.cellPosition);
                }
            }
        }
    }

    public int GetMapWidth() { return width; }
    public int GetMapHeight() { return height; }
    public Vector2 GetCellSize() { return cellSize; }

    private void ChangeTurns()
    {
        playerTurn = !playerTurn;
    }
}
