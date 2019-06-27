using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager_sample : MonoBehaviour
{
    public GameObject Enemy1, Enemy2, Enemy3, Enemy4;
    // Start is called before the first frame update
    public List<GameObject> enemyList;
    public List<GameObject> enemySpawnList;
    

    
    public enum GameState
    {
        notIncombat,
        Incombat
    }
    public GameState gameState;

    public enum CombatState
    {
        PlayerTurn,
        EnemyTurn,
        Victory,
        Loss
    }
    
    public CombatState combatState;
    //Objects for combat
    public GameObject enemyobj;
    public GameObject playerobj;

    void Start()
    {
        
        foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            enemyList.Add(enemy);

        }

    }

    // Update is called once per frame
    public void healenemies()
    {
        foreach(GameObject enemy in enemyList)
        {
            enemy.GetComponent<Stats>().health += 10;
        }
    }
    public void RemoveEnemy(GameObject enemyToRemove)
    {
        enemyList.Remove(enemyToRemove);
    }
    public void SpawnEnemy()
    {
        //spawn enemy from list
        //using the size of the list as the random range maximum
        Instantiate(enemySpawnList[Random.Range(0, enemySpawnList.Count)], transform);
    }
    public void CheckCombatState()
    {
        switch(combatState)
        {
            //Player Turn
            case CombatState.PlayerTurn:
            //Desicion - Attack
            //Attack the enemy
            playerobj.GetComponent<Sample_player>().AttackTarget(enemyobj);
                //Check if Enemy is defeated
                if (enemyobj.GetComponent<Sample_enemy>().myStats.isDefeated)
                    SpawnEnemy();
                //Next Case. Most likely EnemyTurn
                break;

            //Enemy Turn
            case CombatState.EnemyTurn:
                //Desiciom - Attack
                //Attack the Player
                enemyobj.GetComponent<Sample_enemy>().AttackTarget(playerobj);
                //Check if player is defeated
                if(playerobj.GetComponent<Sample_player>().myStats.isDefeated)
                {

                }
                break;

            //Victory
            //Tell the player they won
            //End game

            //Loss
            //Tell the player they lost
            //Restart the game

        }
    }
    
}

