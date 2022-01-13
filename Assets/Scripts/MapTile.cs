using UnityEngine;

public class MapTile
{
    public Vector2 GlobalPos;
    public int Traversability;
    public bool Occupied;

    public MapTile(Vector2 Pos, int Traversability)
    {
        GlobalPos = Pos;
        this.Traversability = Traversability;
        Occupied = false;
    }
}