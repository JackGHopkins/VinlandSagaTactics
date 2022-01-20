using System;
using UnityEngine;

public class Map<T>
{
    private int Width;
    private int Height;
    private Vector2 CellSize;
    public T[,] grid;

    public Map(int x, int y, Vector2 CellSize, Vector3 MapOrigin, Func<Map<T>, int, int, T> createGridObject)
    {
        Width = x;
        Height = y;
        this.CellSize = CellSize;

        grid = new T[Width, Height];

        for (int i = 0; i < grid.GetLength(0); i++)
        {
            for (int j = 0; j < grid.GetLength(1); j++)
            {
                grid[i, j] = createGridObject(this, i, j);
            }
        }
    }
}

