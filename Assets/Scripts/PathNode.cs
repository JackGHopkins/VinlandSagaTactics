using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathNode
{
    private Map<PathNode> map;

    public int x;
    public int y;
    public int gCost;
    public int hCost;
    public int fCost;
    public Vector2 cellSize;
    public Vector3 worldPosition;

    public bool isWalkable;
    public PathNode previousNode;
    public PathNode(Map<PathNode> map, int x, int y)
    {
        this.cellSize = new Vector2(0.64f, 0.32f);
        this.map = map;
        this.x = x;
        this.y = y;
        this.isWalkable = true;

        this.worldPosition = ReturnCellPosition(new Vector3Int(x, y, 0));
    }

    public void CalculateFCost()
    {
        fCost = gCost + hCost;
    }

    // Returns Position of Cell in the world
    public Vector3 ReturnCellPosition(Vector3Int pos)
    {
        pos = SwitchXandY(pos);
        Vector2 halfCellSize = cellSize / 2;
        Vector3 worldPos = new Vector3((pos.x * halfCellSize.x) + (pos.y * -halfCellSize.x), (pos.x * halfCellSize.y) + (pos.y * halfCellSize.y), 0);
        worldPos.y = -worldPos.y + 0.48f;
        return worldPos;
    }

    public Vector3Int SwitchXandY(Vector3Int position)
    {
        int temp;
        temp = position.x;
        position.x = position.y;
        position.y = temp;

        return position;
    }
}
