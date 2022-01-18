using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cursor : MapElement
{
    // Start is called before the first frame update
    void Start()
    {
        Position = new Vector3(0, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W) && Position.y > 0 ) { 
            Position.y--;
            SetWorldPosition(Position, Game.GetCellSize());
        }
        if (Input.GetKeyDown(KeyCode.S) && Position.y < Game.GetMapHeight() - 1) { 
            Position.y++;
            SetWorldPosition(Position, Game.GetCellSize());
        }
        if (Input.GetKeyDown(KeyCode.A) && Position.x > 0) { 
            Position.x--;
            SetWorldPosition(Position, Game.GetCellSize());
        }
        if (Input.GetKeyDown(KeyCode.D) && Position.x < Game.GetMapWidth() - 1) {
            Position.x++;
            SetWorldPosition(Position, Game.GetCellSize()); 
        }
    }
}
