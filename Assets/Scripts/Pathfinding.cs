using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinding
{
    private const int MOVE_STRAIGHT_COST = 10;
    private List<PathNode> openList;
    private List<PathNode> closedList;

    [SerializeField] MainGame game;

    public Map<PathNode> grid;

    public Pathfinding(int width, int height, MainGame game)
    {
        this.game = game;
        grid = new Map<PathNode>(width, height, game.GetCellSize(), game.GetMapOrigin(), (Map<PathNode> g, int x, int y) => new PathNode(g, x, y));
    }

    public List<PathNode> FindPath(int startX, int startY, int endX, int endY)
    {
        PathNode startNode = grid.grid[startX, startY];
        PathNode endNode = grid.grid[endX, endY];
        openList = new List<PathNode> { startNode };
        closedList = new List<PathNode>();

        for (int x = 0; x < grid.grid.GetLength(0); x++)
        {
            for (int y = 0; y < grid.grid.GetLength(1); y++)
            {
                PathNode pathNode = grid.grid[x, y];
                pathNode.gCost = int.MaxValue;
                pathNode.CalculateFCost();
                pathNode.previousNode = null;
            }
        }
        startNode.gCost = 0;
        startNode.hCost = CalculateDstanceCost(startNode, endNode);
        startNode.CalculateFCost();

        while (openList.Count > 0)
        {
            PathNode curNode = GetLowestFCostNode(openList);
            if (curNode == endNode)
            {
                // Reached Final Goal
                return CalculatedPath(endNode);
            }

            openList.Remove(curNode);
            closedList.Add(curNode);

            foreach ( PathNode nNode in GetNeighbourList(curNode))
            {
                if (closedList.Contains(nNode))
                    continue;

                if (!nNode.isWalkable)
                {
                    closedList.Add(nNode);
                    continue;
                }

                int tempGCost = curNode.gCost + CalculateDstanceCost(curNode, nNode);
                if(tempGCost < nNode.gCost)
                {
                    nNode.previousNode = curNode;
                    nNode.gCost = tempGCost;
                    nNode.hCost = CalculateDstanceCost(nNode, endNode);
                    nNode.CalculateFCost();

                    if (!openList.Contains(nNode))
                        openList.Add(nNode);
                }
            }
        }

        // Out of Nodes on OpenList
        return null;
    }

    private int CalculateDstanceCost(PathNode a, PathNode b)
    {
        int xDistance = Mathf.Abs(a.x - b.x);
        int yDistance = Mathf.Abs(a.y - b.y);
        int remaining = Mathf.Abs(xDistance = yDistance);
        return MOVE_STRAIGHT_COST * remaining;
    }

    private PathNode GetLowestFCostNode(List<PathNode> pathNodeList)
    {
        PathNode lowestFCostNode = pathNodeList[0];
        for(int i = 1; i < pathNodeList.Count; i++)
        {
            if (pathNodeList[i].fCost < lowestFCostNode.fCost)
                lowestFCostNode = pathNodeList[i];
        }
        return lowestFCostNode;
    }

    private List<PathNode> CalculatedPath(PathNode endNode)
    {
        List<PathNode> path = new List<PathNode>();
        path.Add(endNode);
        PathNode curNode = endNode;
        
        while(curNode.previousNode != null)
        {
            path.Add(curNode.previousNode);
            curNode = curNode.previousNode;
        }
        path.Reverse();
        return path;
    }

    private List<PathNode> GetNeighbourList(PathNode curNode)
    {
        List<PathNode> nList = new List<PathNode>();
        // Left
        if(curNode.x - 1 >= 0)
            nList.Add(grid.grid[curNode.x - 1, curNode.y]);
        // Right
        if (curNode.x + 1 < grid.grid.GetLength(0))
            nList.Add(grid.grid[curNode.x + 1, curNode.y]);
        // Up
        if (curNode.y - 1 >= 0)
            nList.Add(grid.grid[curNode.x, curNode.y - 1]);
        // Down
        if (curNode.y + 1 < grid.grid.GetLength(1))
            nList.Add(grid.grid[curNode.x, curNode.y + 1]);

        return nList;
    }
}
