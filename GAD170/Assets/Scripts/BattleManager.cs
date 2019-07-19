using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//adding this line because whne we lose we want to restart the scene. 
using UnityEngine.SceneManagement;

public class BattleManager : MonoBehaviour
{
    public GameObject Enemy1, Enemy2, Enemy3, Enemy4;
    // Start is called before the first frame update
    public List<GameObject> enemyList;
    public List<GameObject> enemySpawnList;
    private bool doBattle = true;

    private GameObject gameManager;
    

    
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
        gameManager = GameObject.FindGameObjectWithTag("GameManager");

    }
    void Update()
    {
        if(doBattle)
        {
            StartCoroutine(Battlego());
            doBattle = false;
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
                BattleRound(playerobj, enemyobj);
                //Check if Enemy is defeated
                if (enemyobj.GetComponent<Stats>().isDefeated)
                    SpawnEnemy();
                //Next Case. Most likely EnemyTurn
                combatState = CombatState.EnemyTurn;
                break;

            //Enemy Turn
            case CombatState.EnemyTurn:
                //Desiciom - Attack
                //Attack the Player
                BattleRound(enemyobj, playerobj);
                //Check if player is defeated
                if(playerobj.GetComponent<Stats>().isDefeated)
                {
                    //set state to lose cause we died
                    combatState = CombatState.Loss;
                    Debug.Log("Lose");
                    gameManager.GetComponent<GameManager>().TravelToWorld(GameManager.Worlds.Overworld);
                    break;
                }
                //next case will be players turn.
                combatState = CombatState.PlayerTurn;
                break;

            //Victory
            case CombatState.Victory:
                Debug.Log("You are Win!!");
                gameManager.GetComponent<GameManager>().TravelToWorld(GameManager.Worlds.Overworld);
                break;
            //Tell the player they won
            //End game
            case CombatState.Loss:

                //Loss
                //Tell the player they lost
                //Restart the game
                SceneManager.LoadScene("SampleScene");
                break;



        }
    }
    public void BattleRound(GameObject attacker, GameObject defender)
    {
        //Will take an attacker and defender and make them do combat
        defender.GetComponent<Stats>().Attacked(attacker.GetComponent<Stats>().attack, Stats.StatusEffect.none);
        Debug.Log("Attacker: " + attacker.name + " | Defender: " + defender.name);
        Debug.Log(attacker.name +
             " attacks " +
             defender.name +
             " for a total of " +
             (attacker.GetComponent<Stats>().attack - defender.GetComponent<Stats>().defense) +
             " damage ");
    }
    IEnumerator Battlego()
    {
        CheckCombatState();
        yield return new WaitForSeconds(1f);
        doBattle = true;
    }
    
}

