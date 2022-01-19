using UnityEngine;

public class Map
{
    private int Width;
    private int Height;
    private Vector2 CellSize;
    public MapTile[,] GridArray;

    public Map(int x, int y, Vector2 CellSize, Vector3 MapOrigin)
    {
        Width = x;
        Height = y;
        this.CellSize = CellSize;

        GridArray = new MapTile[Width, Height];

        for (int i = 0; i < GridArray.GetLength(0); i++)
        {
            for (int j = 0; j < GridArray.GetLength(1); j++)
            {
                GridArray[i, j] = new MapTile(new Vector2(i, j), 1);
            }
        }
    }

    void Start()
    {

    }
}

