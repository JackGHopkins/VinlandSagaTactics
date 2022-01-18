using UnityEngine;

public class Map
{
    private int Width;
    private int Height;
    private Vector2 CellSize;
    private MapTile[,] GridArray;

    public Map(int x, int y, Vector2 CellSize, Vector3 MapOrigin)
    {
        Width = x;
        Height = y;
        this.CellSize = CellSize;

        MapTile[,] GridArray = new MapTile[x, y];
    }
}

