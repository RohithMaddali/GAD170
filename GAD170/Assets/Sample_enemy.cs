using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sample_enemy : MonoBehaviour
{

     public Stats myStats;
    public int enemyID = 1;
    private GameObject GameManager;
    public enum EnemyTypes
    {
        small,
        medium,
        large
    }

    public EnemyTypes myType;
    void Start()
    {
        //Find our game manager
        GameManager = GameObject.FindGameObjectWithTag("GameManager");
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
        if (myStats.health <= 0)
            myStats.isDefeated = true;
    }
    public void AttackTarget(GameObject target)
    {
        target.GetComponent<Sample_player>().Attacked(myStats.attack,Stats.StatusEffect.none);
    }
    IEnumerator randomDelay()
    {
        yield return new WaitForSeconds(Random.Range(1, 4));
    }
    public void Defeated()
    {
        GameManager.GetComponent<GameManager>().RemoveEnemy(gameObject);
    }
}

