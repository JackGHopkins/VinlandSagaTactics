using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MapElement
{
    private int HP, AP, Attack, Defense, RangedAttack, RangedDefense;
    private StatusEffect statusEffect = StatusEffect.Null;

    [SerializeField] string name;
    [SerializeField] UnitClass unitType = UnitClass.Null;
    [SerializeField] UnitTeam unitTeam = UnitTeam.Null;
    [SerializeField] UnitSelection unitSelection = UnitSelection.Move;
    [SerializeField] GameObject tileMovementPrefab;
    [SerializeField] GameObject tileAttackPrefab;

    public bool turnComplete;
    public bool selected = false;
    public bool tileDrawn = false;
    public GameObject[] tilesUI;

    //static Unit() => new Unit();

    private Unit() {}

    void Start()
    {
        SetPosition(cellPosition);
        InitUnitType();

        tilesUI = new GameObject[100];
    }

    private void Update()
    {
        if (selected)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                DestroyTiles();
                unitSelection = UnitSelection.Move;
            }
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                DestroyTiles();
                unitSelection = UnitSelection.Attack;
            }
            DrawTiles();
        } else
        {
            if (tilesUI[0])
                DestroyTiles();
        }
    }

    public void InitUnitType() {
        switch(unitType) {
            case UnitClass.Null:
                break;
            case UnitClass.Dragger:
                InitWarrior();
                break;
            case UnitClass.Sword:
                InitHunter();
                break;            
            case UnitClass.Spear:
                InitHunter();
                break;            
            case UnitClass.Crossbow:
                InitHunter();
                break;           
        }
    }

    void InitWarrior() { 
        name = "Warrior";
        HP = 10;
        AP = 2;
        Attack = 5;
        Defense = 5;
        RangedAttack = 0;
        RangedDefense = 5;
    }

    void InitHunter() { 
        name = "Hunter";
        HP = 5;
        AP = 4;
        Attack = 1;
        Defense = 3;
        RangedAttack = 8;
        RangedDefense = 5;
    }

    private bool CheckPosition(Vector3Int position)
    {
        if (!game.map.GridArray[position.x, position.y].occupied)
            return true;
        Debug.Log("Grid Element Overlap: There is already a Unit in this Grid Position");
        return false;
    }

    private void DrawTiles()
    {
        if (!tileDrawn)
        {
            if (unitSelection == UnitSelection.Move)
                DrawMovement();

            if (unitSelection == UnitSelection.Attack)
                DrawAttack();

            tileDrawn = true;
        }
    }

    private void DrawMovement()
    {
        int tileArrEle = 0;
        int orignal = 2;
        int h = orignal;
        int w = 0;
        for (int i = -h; i <= h; i++)
        {
            w = orignal - Math.Abs(i);
            for (int j = -w; j <= w; j++)
            {
                tilesUI[tileArrEle] = Instantiate(tileMovementPrefab, ReturnCellPosition(new Vector3Int(cellPosition.x + i, cellPosition.y + j, 0)), transform.rotation);
                tileArrEle++;
            }
        }
    }

    private void DrawAttack()
    {
        tilesUI[0] = Instantiate(tileAttackPrefab, transform.position, transform.rotation);
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
        game.map.GridArray[cellPosition.x, cellPosition.y].occupied = false;
        game.map.GridArray[position.x, position.y].occupied = true;
        game.map.GridArray[position.x, position.y].currentUnit = this;
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

    public Vector3 ReturnCellPosition(Vector3Int pos)
    {
        pos = SwitchXandY(pos);
        Vector2 halfCellSize = game.GetCellSize() / 2;
        Vector3 worldPos = new Vector3((pos.x * halfCellSize.x) + (pos.y * -halfCellSize.x), (pos.x * halfCellSize.y) + (pos.y * halfCellSize.y), 0);
        worldPos.y = -worldPos.y + 0.16f;
        return worldPos;
    }

}
