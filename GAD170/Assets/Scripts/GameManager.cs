using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public List<GameObject> EnemySpawnList;
    public List<GameObject> EnemiesToFight;

    //0 = health, 1 = curExp, 2 = level
  

    public enum Worlds
    {
        Overworld,
        BatleScene
    }

    private static GameManager gameManRef; 

    //void awake is called before void start or any object
    void Awake()
    {
        //this will make it so we can travel between scenes (good for keeping track of game play)
        if (gameManRef == null)
        {
            gameManRef = this;
            //This will make it so we can travel between scenes (good for keeping track of gameplay!)
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            DestroyImmediate(gameObject);
        }
    }

    void Start()
    {
        LoadPlayerStuff(true);
    }

    public void TravelToWorld(Worlds destination)
    {
        switch(destination)
        {
            case Worlds.Overworld:
                //load overworld scene;
                SavePlayerStuff(false);
                SceneManager.LoadScene("Overworld");
                LoadPlayerStuff(true);
                break;
            case Worlds.BatleScene:
                //load battlescene
                SavePlayerStuff(true);
                SceneManager.LoadScene("BattleScene");
                LoadPlayerStuff(false);
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
            //save both location and rotation as seperate floats, using similar naming conventions
            //storing position values
            PlayerPrefs.SetFloat("PlayerPosx", transform.position.x);
            PlayerPrefs.SetFloat("PlayerPosy", transform.position.y);
            PlayerPrefs.SetFloat("PlayerPosz", transform.position.z);
            //storing rotation values
            PlayerPrefs.SetFloat("PlayerRotx", transform.rotation.x);
            PlayerPrefs.SetFloat("PlayerRoty", transform.rotation.y);
            PlayerPrefs.SetFloat("PlayerRotz", transform.rotation.z);
        }

        //Save stats that we need to track!
        Stats playerStats = GameObject.FindGameObjectWithTag("Player").GetComponent<Stats>();
        //storedPlayerStats[0] = (int)playerStats.health;
        PlayerPrefs.SetFloat("playerHealth", playerStats.health);
        //storedPlayerStats[1] = playerStats.curExp;
        PlayerPrefs.SetInt("playerCurrentExp", playerStats.curExp);
        //storedPlayerStats[2] = playerStats.level;
        PlayerPrefs.SetInt("playerLevel", playerStats.level);
    }

    void LoadPlayerStuff(bool goingToOverworld)
    {
        //load the existing stats and apply them to the player!
        Stats playerStats = GameObject.FindGameObjectWithTag("Player").GetComponent<Stats>();
        playerStats.health = PlayerPrefs.GetFloat("playerHealth", playerStats.maxHealth);
        playerStats.curExp = PlayerPrefs.GetInt("playerCurrentExp", 0);
        playerStats.level = PlayerPrefs.GetInt("playerLevel", 1);
        //load position only in overworld
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (goingToOverworld)
        {
            playerObj.transform.position = new Vector3(PlayerPrefs.GetFloat("playerPosx",0f),PlayerPrefs.GetFloat("playerPosy", 2f),
                                                   PlayerPrefs.GetFloat("playerPosz", 0f));
            playerObj.transform.rotation = Quaternion.Euler(PlayerPrefs.GetFloat("playerRotx", 0f), PlayerPrefs.GetFloat("playerRoty", 2f),
                                                   PlayerPrefs.GetFloat("playerRotz", 0f));
        }

    }
    
    public void DeleteSavedStuff()
    {
        //hard reset
        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene("Overwolrd");
    }
  

    // Update is called once per frame
    void Update()
    {
        
    }
}
