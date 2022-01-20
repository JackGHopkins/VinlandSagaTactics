using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapElement : MonoBehaviour
{
    public Tilemap tilemap;
    public Vector3Int cellPosition;
    public MainGame game;

    public void SetWorldPosition(Vector3Int pos, MainGame game)
    {
        pos = SwitchXandY(pos);
        Vector2 halfCellSize = game.GetCellSize() / 2;
        Vector3 worldPos = new Vector3((pos.x * halfCellSize.x) + (pos.y * -halfCellSize.x), (pos.x * halfCellSize.y) + (pos.y * halfCellSize.y), 0);
        worldPos.y = -worldPos.y + 0.16f;
        this.transform.position = worldPos;
    }
    public void SetMousePosition(Vector3 Pos, MainGame Game)
    {
        Vector2 halfCellSize = Game.GetCellSize() / 2;
        Vector3 worldPos = new Vector3((Pos.x * halfCellSize.x) + (Pos.y * -halfCellSize.x), (Pos.x * halfCellSize.y) + (Pos.y * halfCellSize.y), 0);
        worldPos.y += 0.16f;
        this.transform.position = worldPos;
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

