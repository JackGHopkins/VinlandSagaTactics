using UnityEngine;

public class MapTile
{
    public Vector3Int cellPosition;
    public Map<MapTile> map;
    public int traversability;
    public bool occupied;
    public Unit currentUnit;

    public MapTile()
    {
        this.traversability = 1;
        occupied = false;
    }

    public MapTile(Map<MapTile> map, int x, int y)
    {
        this.map = map;
        cellPosition = new Vector3Int(x, y, 0);
        this.traversability = 1;
        occupied = false;
    }
}