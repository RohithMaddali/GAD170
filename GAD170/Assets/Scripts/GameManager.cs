using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public List<GameObject> EnemySpawnList;
    public List<GameObject> EnemiesToFight;

    //0 = health, 1 = curExp, 2 = level
    List<int> storedPlayerStats;
    Transform storedPlayerTransform;
    public string tracker;

    public enum Worlds
    {
        Overworld,
        BatleScene
    }

    //void awake is called before void start or any object
    void Awake()
    {
        //this will make it so we can travel between scenes (good for keeping track of game play)
        foreach (GameObject gameMan in GameObject.FindGameObjectsWithTag("GameManager"))
        {
            if (gameMan.GetComponent<GameManager>().tracker != "sup")
            {
                Destroy(gameMan);
            }
        }
        //This will make it so we can travel between scenes (good for keeping track of gameplay!)
        DontDestroyOnLoad(this.gameObject);
    }

    public void TravelToWorld(Worlds destination)
    {
        switch(destination)
        {
            case Worlds.Overworld:
                //load overworld scene;
                SceneManager.LoadScene("Overworld");
                break;
            case Worlds.BatleScene:
                //load battlescene
                SceneManager.LoadScene("BattleScene");
                break;
        }
    }
    void GenerateEnemies()
    {
        for (int i = 0; i < 3; i++)
        {
            //add random enemies to fight from our list, this will run each time we enter wild grass!
            EnemiesToFight.Add(EnemySpawnList[Random.Range(0, EnemySpawnList.Count)]);
        }
    }
    void SavePlayerStuff(bool isFromOverworld)
    {
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        //only save position in overworld
        if (isFromOverworld)
        {
            storedPlayerTransform.position = playerObj.transform.position;
            storedPlayerTransform.rotation = playerObj.transform.rotation;
        }

        //Save stats that we need to track!
        Stats playerStats = GameObject.FindGameObjectWithTag("Player").GetComponent<Stats>();
        storedPlayerStats[0] = (int)playerStats.health;
        storedPlayerStats[1] = playerStats.curExp;
        storedPlayerStats[2] = playerStats.level;
    }

    void LoadPlayerStuff(bool goingToOverworld)
    {
        //load the existing stats and apply them to the player!
        Stats playerStats = GameObject.FindGameObjectWithTag("Player").GetComponent<Stats>();
        playerStats.health = storedPlayerStats[0];
        playerStats.curExp = storedPlayerStats[1];
        playerStats.level = storedPlayerStats[2];
        //load position only in overworld
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (goingToOverworld)
        {
            playerObj.transform.position = storedPlayerTransform.position;
            playerObj.transform.rotation = storedPlayerTransform.rotation;
        }

    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
