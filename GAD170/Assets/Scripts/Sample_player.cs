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

   
   /*
    *public void AttackTarget(GameObject target)
    {
        target.GetComponent<Sample_player>().Attacked(myStats.attack, Stats.StatusEffect.none);
    }
    */
}
