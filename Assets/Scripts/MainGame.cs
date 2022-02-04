using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MainGame : MonoBehaviour
{
    private bool playerTurn = true;
    private Unit curUnit;
    private Pathfinding pathfinding;

    [SerializeField] int width = 5;
    [SerializeField] int height = 5;
    [SerializeField] Vector2 cellSize;
    [SerializeField] Vector3 mapOrigin;
    [SerializeField] Cursor cursor;
    [SerializeField] Unit[] teamPlayer;
    [SerializeField] Unit[] teamEnemy;

    public Map<MapTile> map;
    public Tilemap tilemap;
    // Start is called before the first frame update
    void Awake()
    {
        map = new Map<MapTile>(width, height, cellSize, mapOrigin, (Map<MapTile> map, int x, int y) => new MapTile(map, x, y));
        pathfinding = new Pathfinding(width, height, this);

        UpdateWalkablePathGrid();

        playerTurn = true;
    }

    // Update is called once per frame
    void Update()
    {
        CheckUnitSelected();
    }

    public int GetMapWidth() { return width; }
    public int GetMapHeight() { return height; }
    public Vector2 GetCellSize() { return cellSize; }
    public Vector2 GetMapOrigin() { return mapOrigin; }

    private void CheckUnitSelected()
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            if (cursor.selected)
            {
                PathDebug();

                pathfinding.grid.grid[curUnit.cellPosition.x, curUnit.cellPosition.y].isWalkable = true;
                pathfinding.grid.grid[cursor.cellPosition.x, cursor.cellPosition.y].isWalkable = false ;
                cursor.selected = false;
                curUnit.SetPosition(cursor.cellPosition);
                curUnit.selected = false;
                curUnit.tileDrawn = false;
                if (curUnit.AP == 1)
                    curUnit._State = UnitState.End;
                curUnit.DrawTiles(pathfinding);
                Debug.Log("Moved: " + curUnit.name + " to: " + curUnit.cellPosition);
                curUnit = null;
            }
            else
            {
                if (map.grid[cursor.cellPosition.x, cursor.cellPosition.y].occupied)
                {
                    cursor.selected = true;
                    curUnit = map.grid[cursor.cellPosition.x, cursor.cellPosition.y].currentUnit;
                    curUnit.selected = true;
                    Debug.Log("Selected: " + curUnit.name + " at: " + curUnit.cellPosition);
                    curUnit.DrawTiles(pathfinding);
                }
            }
        }
    }

    private void ChangeTurns()
    {
        playerTurn = !playerTurn;
    }
    
    private void UpdateWalkablePathGrid()
    {
        for (int x = 0; x < map.grid.GetLength(0); x++)
        {
            for (int y = 0; y < map.grid.GetLength(0); y++)
            {
                if (map.grid[x, y].occupied)
                    pathfinding.grid.grid[x, y].isWalkable = false;
            }
        }
    }

    private void PathDebug()
    {
        List<PathNode> path = pathfinding.FindPath(curUnit.cellPosition.x, curUnit.cellPosition.y, cursor.cellPosition.x, cursor.cellPosition.y);
        if (path != null)
        {
            for (int i = 0; i < path.Count - 1; i++)
            {
                Debug.DrawLine(new Vector3(path[i].worldPosition.x, path[i].worldPosition.y, 0), new Vector3(path[i + 1].worldPosition.x, path[i + 1].worldPosition.y, 0), Color.white, 100f);
            }
        }
    }
}
