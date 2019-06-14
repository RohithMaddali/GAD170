using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    Stats myStats;
    public int enemyID = 1;
    public enum EnemyTypes
    {
        small,
        medium,
        large
    }

    public EnemyTypes myType;
    void Start()
    {
        myStats = GetComponent<Stats>();
        switch(myType)
        {
            case EnemyTypes.small:
                //do setup
                break;
            case EnemyTypes.medium:
                //do thing
                break;
            case EnemyTypes.large:
                //do thing
                break;
        }
    }
    public void Attacked(int DMG, Stats.StatusEffect incEffect)
    {
        myStats.health -= DMG - myStats.defense;
        myStats.myStatus = incEffect;
    }
    public void AttackTarget()
    {
        Attacked(myStats.attack,Stats.StatusEffect.none);
    }
}

