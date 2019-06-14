using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Testscript_01 : MonoBehaviour
{
    public int fruit;
    public int apples;
    public int var;
    int playerlives = 50;
    public int classSize = 25;
    public bool classCounted;


    // Start is called before the first frame update
    void Start()
    {
        /* HelloWorld();
         modifylives(-2);
         displayname("Rohith", "Maddali");*/
        
    }

    // Update is called once per frame
    void Update()
    {
       if(Input.GetKeyDown(KeyCode.Space))
        {
            //simple 1d6 dice
            // if(Random.Range(successCalc,100) > 75)
            if (Random.Range(1,7) > 4)
            {
                HelloWorld();
                lives(2);
                gameover(playerlives);
                Debug.Log("you won");
            }
            else
            {
                Debug.Log("you lost");
            }
            
        }
        if (!classCounted)
        {
            for (int i = 1; i < classSize; i++)
            {
                Debug.Log(i);
            }
            classCounted = true;
        }


    }

    void HelloWorld()
    {
        Debug.Log("Hello world");
    }

    void modifylives(int inclives)
    {
        playerlives += inclives;
        Debug.Log(inclives);
    }
    void displayname(string firstname, string lastname)
    {
        Debug.Log(firstname + " " + lastname);
    }
    void lives(int n)
    {
        
            playerlives -= n;
            Debug.Log(playerlives);
    }
    void gameover(int n)
    {
        if(n<=0)
        {
            Debug.Log("Game Over");
        }
        else
        {
            Debug.Log("You have " + n + " lives left");
        }
        
    }
    void addexp()
    {

    }
    void levelup()
    {

    }
    void attributes()
    {

    }
     





}
