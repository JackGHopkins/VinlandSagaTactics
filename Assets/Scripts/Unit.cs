using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MapElement
{
    private int HP, AP, CurAP, Attack, Defense, RangedAttack, RangedDefense, Range;
    private int MAX_AP = 6;
    private StatusEffect statusEffect = StatusEffect.Null;

    [SerializeField] string name;
    [SerializeField] UnitClass unitClass = UnitClass.Null;
    [SerializeField] UnitTeam unitTeam = UnitTeam.Null;
    [SerializeField] UnitSelection unitSelection = UnitSelection.Move;
    [SerializeField] GameObject tileMovementPrefab;
    [SerializeField] GameObject tileAttackPrefab;

    public bool turnComplete;
    public bool selected = false;
    public bool tileDrawn = false;
    public GameObject[] tilesUI;

    private Unit() {}

    void Start()
    {
        SetPosition(cellPosition);
        InitUnitType();

        tilesUI = new GameObject[100];
    }

    private void Update()
    {
        if (turnComplete)
            SetNextTurnAP();
    }

    public void InitUnitType() {
        switch(unitClass) {
            case UnitClass.Null:
                Debug.Log("UNIT CLASS NULL");
                break;
            case UnitClass.Dagger:
                InitDagger();
                break;
            case UnitClass.Sword:
                InitSword();
                break;            
            case UnitClass.Spear:
                InitSpear();
                break;            
            case UnitClass.Crossbow:
                InitCrossbow();
                break;           
        }
    }

    void InitDagger() { 
        name = "Dagger";
        HP = 10;
        AP = 6;
        Attack = 3;
        Defense = 3;
        RangedAttack = 0;
        RangedDefense = 5;
        Range = 2;
    }

    void InitSword()
    {
        name = "Sword";
        HP = 10;
        AP = 4;
        Attack = 5;
        Defense = 5;
        RangedAttack = 0;
        RangedDefense = 5;
        Range = 1;
    }
    void InitSpear()
    {
        name = "Spearman";
        HP = 10;
        AP = 4;
        Attack = 5;
        Defense = 5;
        RangedAttack = 0;
        RangedDefense = 5;
        Range = 3;
    }

    void InitCrossbow() { 
        name = "Crossbow";
        HP = 5;
        AP = 4;
        Attack = 1;
        Defense = 3;
        RangedAttack = 8;
        RangedDefense = 5;
        Range = 5;
    }

    private bool CheckPosition(Vector3Int position)
    {
        if (!game.map.grid[position.x, position.y].occupied)
            return true;
        Debug.Log("Grid Element Overlap: There is already a Unit in this Grid Position");
        return false;
    }

    public void DrawTiles(Pathfinding pathfinding)
    {
        if (selected)
        {
            if (Input.GetKey(KeyCode.Alpha1))
            {
                DestroyTiles();
                unitSelection = UnitSelection.Move;
            }
            if (Input.GetKey(KeyCode.Alpha2))
            {
                DestroyTiles();
                unitSelection = UnitSelection.Attack;
            }

            // Draw Tiles
            if (!tileDrawn)
            {
                if (unitSelection == UnitSelection.Move)
                    DrawMovement(pathfinding);

                if (unitSelection == UnitSelection.Attack)
                    DrawAttack(pathfinding);

                tileDrawn = true;
            }
        }
        else
        {
            if (tilesUI[0])
                DestroyTiles();
        }
    }

    private void DrawMovement(Pathfinding pathfinding)
    {
        int tile = 0;
        //int orignal = 2;
        //int h = orignal;
        //int w = 0;
        //for (int i = -h; i <= h; i++)
        //{
        //    w = orignal - Math.Abs(i);
        //    for (int j = -w; j <= w; j++)
        //    {
        //        tilesUI[tile] = Instantiate(tileMovementPrefab, ReturnCellPosition(new Vector3Int(cellPosition.x + i, cellPosition.y + j, 0)), transform.rotation);
        //        tile++;
        //    }
        //}
        for (int i = cellPosition.x - AP; i <= cellPosition.x + AP; i++)
        {
            if (i < 0 || i > game.map.grid.GetLength(0))
                continue;
            for (int j = cellPosition.y - AP; j <= cellPosition.y + AP; j++)
            {
                if (j < 0 || j > game.map.grid.GetLength(1) || (i == cellPosition.x && j == cellPosition.y))
                    continue;
                if (pathfinding.grid.grid[i, j].isWalkable)
                {
                    List<PathNode> path = pathfinding.FindPath(cellPosition.x, cellPosition.y, i, j);
                    if (path != null)
                    {
                        if (path.Count <= AP)
                        {
                            tilesUI[tile] = Instantiate(tileMovementPrefab, ReturnCellPosition(new Vector3Int(i, j, 0)), transform.rotation);
                            tile++;
                        }
                    }
                }
            }
        }
    }

    private void DrawAttack(Pathfinding pathfinding)
    {
        if (unitClass != UnitClass.Sword)
        {
            PathfindingTileDraw(pathfinding, Range);
            return;
        }
        int tile = 0;
        for (int i = cellPosition.x - Range; i <= cellPosition.x + Range; i++)
        {
            if (i < 0 || i > game.map.grid.GetLength(0))
                continue;
            for (int j = cellPosition.y - Range; j <= cellPosition.y + Range; j++)
            {
                if (j < 0 || j > game.map.grid.GetLength(1) || (i == cellPosition.x && j == cellPosition.y))
                    continue;
                tilesUI[tile] = Instantiate(tileAttackPrefab, ReturnCellPosition(new Vector3Int(i, j, 0)), transform.rotation);
                tile++;
            }
        }
    }

    private void PathfindingTileDraw(Pathfinding pathfinding, int Range)
    {
        int tile = 0;
        for (int i = cellPosition.x - Range; i <= cellPosition.x + Range; i++)
        {
            if (i < 0 || i > game.map.grid.GetLength(0))
                continue;
            for (int j = cellPosition.y - Range; j <= cellPosition.y + Range; j++)
            {
                if (j < 0 || j > game.map.grid.GetLength(1) || (i == cellPosition.x && j == cellPosition.y))
                    continue;
                if (pathfinding.grid.grid[i, j].isWalkable)
                {
                    List<PathNode> path = pathfinding.FindPath(cellPosition.x, cellPosition.y, i, j);
                    if (path != null)
                    {
                        if (path.Count <= Range)
                        {
                            tilesUI[tile] = Instantiate(tileAttackPrefab, ReturnCellPosition(new Vector3Int(i, j, 0)), transform.rotation);
                            tile++;
                        }
                    }
                }
            }
        }
    }

    private void DestroyTiles()
    {
        tileDrawn = false;
        foreach ( var i in tilesUI)
        {
            if (i)
                Destroy(i);
        }
    }

    private void OccupyTile(Vector3Int position)
    {
        game.map.grid[cellPosition.x, cellPosition.y].occupied = false;
        game.map.grid[position.x, position.y].occupied = true;
        game.map.grid[position.x, position.y].currentUnit = this;
    }

    public void SetPosition(Vector3Int position)
    {
        if (CheckPosition(position))
        {
            OccupyTile(position);
            SetWorldPosition(position, game);
            cellPosition = position;
        }
    }

    // Returns Position of Cell in the world
    public Vector3 ReturnCellPosition(Vector3Int pos)
    {
        pos = SwitchXandY(pos);
        Vector2 halfCellSize = game.GetCellSize() / 2;
        Vector3 worldPos = new Vector3((pos.x * halfCellSize.x) + (pos.y * -halfCellSize.x), (pos.x * halfCellSize.y) + (pos.y * halfCellSize.y), 0);
        worldPos.y = -worldPos.y + 0.16f;
        return worldPos;
    }

    private void SetNextTurnAP()
    {
        this.AP += 4;
        if (AP > 6)
            AP = 6;
        CurAP = AP;
    }
}
