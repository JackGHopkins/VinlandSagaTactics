using UnityEngine;

public class MapTile
{
    public Vector2 globalPos;
    public int traversability;
    public bool occupied;
    public Unit currentUnit;

    public MapTile()
    {
        this.traversability = 1;
        occupied = false;
    }

    public MapTile(Vector2 Pos, int Traversability)
    {
        globalPos = Pos;
        this.traversability = 1;
        occupied = false;
    }
}