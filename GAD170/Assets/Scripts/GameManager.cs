using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public enum Worlds
    {
        Overworld,
        BatleScene
    }

    //void awake is called before void start or any object
    void Awake()
    {
        //this will make it so we can travel between scenes (good for keeping track of game play)
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
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
