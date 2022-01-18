using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MapElement
{
    private int HP, AP, Attack, Defense, RangedAttack, RangedDefense;
    private string Name;
    private StatusEffect StatusEffect = StatusEffect.Null;

    [SerializeField] UnitType UnitType = UnitType.Null;
    [SerializeField] UnitTeam UnitTeam = UnitTeam.Null;

    //static Unit() => new Unit();

    private Unit()
    {
        InitUnitType();
    }

    public void InitUnitType() {
        switch(UnitType) {
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
        Name = "Warrior";
        HP = 10;
        AP = 4;
        Attack = 5;
        Defense = 5;
        RangedAttack = 0;
        RangedDefense = 5;
    }

    void InitHunter() { 
        Name = "Hunter";
        HP = 5;
        AP = 4;
        Attack = 1;
        Defense = 3;
        RangedAttack = 8;
        RangedDefense = 5;
    }
}
