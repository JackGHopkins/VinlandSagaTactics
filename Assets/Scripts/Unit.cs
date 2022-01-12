using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public int HP, AP, Attack, Defense, RangedAttack, RangedDefense;
    public string Name;
    public Vector2 Position;
    public StatusEffect StatusEffect;

    public Unit(UnitType UT){
        InitUnitType(UT);
    }

    static Unit() => new Unit(UnitType.Null);

    public void InitUnitType(UnitType UT) {
        switch(UT) {
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
