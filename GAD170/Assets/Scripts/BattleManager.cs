using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//adding this line because whne we lose we want to restart the scene. 
using UnityEngine.SceneManagement;
using UnityEngine.Events;


public class BattleManager : MonoBehaviour
{
    public GameObject Enemy1, Enemy2, Enemy3, Enemy4;
    // Start is called before the first frame update
    public List<GameObject> enemyList;
    public List<GameObject> enemySpawnList;
    private bool doBattle = true;

    private GameObject gameManager;

    //events only need types not names.
    public event System.Action<bool, float> UpdateHealth;
    

    
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
    private GameObject battleUIManager;

    void Awake()
    {
        //sub to battleuimanager
        battleUIManager = GameObject.FindGameObjectWithTag("BattleUIManager");
        battleUIManager.GetComponent<BattleUIManager>().CallAttack += CheckCombatState;
        battleUIManager.GetComponent<BattleUIManager>().CallDefend += CheckCombatState;
        battleUIManager.GetComponent<BattleUIManager>().CallHeal += CheckCombatState;
        //you would need to probably have an enum called playerdescision which would keep track of whatever button was pressed
        //Automatically run enemy's turn but turn it back to manual during the player's turn.
        //can use bools or coroutines to handle this.
    }

    void Start()
    {

        gameManager = GameObject.FindGameObjectWithTag("GameManager");
        //copy list of enemies to spawn from
        foreach (GameObject tempEnemy in gameManager.GetComponent<GameManager>().EnemiesToFight)
        {
            enemySpawnList.Add(tempEnemy);
        }
        //clear the list so the gameManager doesn't need to worry about it and we're ready to go for next time!
        gameManager.GetComponent<GameManager>().EnemiesToFight.Clear();
        //Spawn our first enemy
        SpawnEnemy();

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
        if (enemySpawnList.Count > 0)
        {
            //get the spawn location for the enemies using a tag like "EnemySpawnLoc"
            Transform EnemySpawnLoc = GameObject.FindGameObjectWithTag("EnemySpawnLoc").transform;
            //assign the enemy obj
            enemyobj = Instantiate(enemySpawnList[0], EnemySpawnLoc);
        }
        else
        {
            combatState = CombatState.Victory;
        }
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
        float percentage = defender.GetComponent<Stats>().health / defender.GetComponent<Stats>().maxHealth;
        UpdateHealth(combatState == CombatState.PlayerTurn, percentage);
        Debug.Log(percentage);
    }
    IEnumerator Battlego()
    {
        CheckCombatState();
        yield return new WaitForSeconds(1f);
        doBattle = true;
    }
    
}

