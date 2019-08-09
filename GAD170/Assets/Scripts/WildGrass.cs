using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WildGrass : MonoBehaviour
{
    private GameObject gameManager;
    public bool isInGrass;
    public int chance;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager");
        RollDice();
    }

    void RollDice()
    {
        int diceRoll = Random.Range(1, chance);
        if (diceRoll == chance - 1 && isInGrass)
        {
            //encounter! Load battl scene
            Debug.Log(diceRoll);
            gameManager.GetComponent<GameManager>().TravelToWorld(GameManager.Worlds.BatleScene);
        }
        StartCoroutine(CheckTimer());
    }
    private void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.tag == "Player")
        {
            isInGrass = true;
        }
        
    }
    private void OnTriggerExit(Collider col)
    {
        if(col.gameObject.tag == "Player")
        {
            isInGrass = false;
        }
    }

    IEnumerator CheckTimer()
    {
        yield return new WaitForSeconds(2);
        RollDice();
    }
}
