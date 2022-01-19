using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MapElement
{
    private int HP, AP, Attack, Defense, RangedAttack, RangedDefense;
    private StatusEffect statusEffect = StatusEffect.Null;

    [SerializeField] string name;
    [SerializeField] UnitType unitType = UnitType.Null;
    [SerializeField] UnitTeam unitTeam = UnitTeam.Null;

    public bool turnComplete;
    public bool selected = false;

    //static Unit() => new Unit();

    private Unit() {}

    void Start()
    {
        SetPosition(cellPosition);
        InitUnitType();
    }

    public void InitUnitType() {
        switch(unitType) {
            case UnitType.Null:
                break;
            case UnitType.Warrior:
                InitWarrior();
                break;
            case UnitType.Hunter:
                InitHunter();
                break;
        }
    }

    void InitWarrior() { 
        name = "Warrior";
        HP = 10;
        AP = 4;
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
        {
            game.map.GridArray[position.x, position.y].occupied = true;
            game.map.GridArray[position.x, position.y].currentUnit = this;
            game.map.GridArray[cellPosition.x, cellPosition.y].occupied = false;
            return true;
        }
        Debug.Log("Grid Element Overlap: There is already a Unit in this Grid Position");
        return false;
    }

    public void SetPosition(Vector3Int position)
    {
        if (CheckPosition(position))
        {
            SetWorldPosition(cellPosition, game);
            cellPosition = position;
        }
    }
}
