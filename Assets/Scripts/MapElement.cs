using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapElement : MonoBehaviour
{
    public Vector3 Position;
    public MainGame Game;

    public void SetWorldPosition(Vector3 Pos, Vector2 CellSize)
    {
        Vector2 HalfCellSize = CellSize / 2;
        Vector3 WorldPos = new Vector3((Pos.x * HalfCellSize.x) + (Pos.y * -HalfCellSize.x), (Pos.x * HalfCellSize.y) + (Pos.y * HalfCellSize.y), 0);
        WorldPos.y = -WorldPos.y + 0.16f;
        this.transform.position = WorldPos;
    }
}

