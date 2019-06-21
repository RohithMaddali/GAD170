using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sample_player : MonoBehaviour
{
    public Stats myStats;
    // Start is called before the first frame update
    void Start()
    {
        myStats = GetComponent<Stats>();
    }

    public void Attacked(int DMG, Stats.StatusEffect incEffect)
    {
        myStats.health -= DMG - myStats.defense;
        myStats.myStatus = incEffect;
        if (myStats.health <= 0)
            myStats.isDefeated = true;
    }
    public void AttackTarget(GameObject target)
    {
        target.GetComponent<Sample_player>().Attacked(myStats.attack, Stats.StatusEffect.none);
    }
}
