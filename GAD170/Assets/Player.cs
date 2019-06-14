﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player  : MonoBehaviour
{
    // Start is called before the first frame update
    public Playerstats Pstats;
    public Enemystats Estats;

    void Start()
    {
        
        
        
            
    }

    // Update is called once per frame
    void Update()
    {
        Pstats = GetComponent<Playerstats>();
        Estats = GetComponent<Enemystats>();
        if (Input.GetKeyDown(KeyCode.Space))
        {
            int EH = Random.Range(Estats.health, Estats.health + 101);
            int Hdiff = EH - Pstats.Health;
            if (Hdiff < Pstats.Health)
            {
                if (Random.Range(1, 11) > 2)
                {
                    Attack(EH, Pstats.Dmg);
                }
                else
                {
                    Debug.Log("Attack missed");
                }
            }
            if (Hdiff > Pstats.Health)
            {
                if (Random.Range(1, 11) > 6)
                {
                    Attack(EH, Pstats.Dmg);
                }
                else
                {
                    Debug.Log("Attack missed");
                }
            }
            if (Hdiff == Pstats.Health)
            {
                if (Random.Range(1, 11) > 4)
                {
                    Attack(EH, Pstats.Dmg);
                }
                else
                {
                    Debug.Log("Attack missed");
                }
            }

        }

    }
    public void Attack (int health, int pdmg)
    {
        int count = 0;
        float expreq = 0;
        int dmg = Random.Range(pdmg - 5, pdmg + 6);
        while (health > 0)
        {
            health -= pdmg;
            count++;
        }
        Debug.Log("Player defeats the enemy in " + count + " hits.");
        switch(count)
        {
            case 3:
                Pstats.exp += 30;
                break;
            case 4:
                Pstats.exp += 25;
                break;
            case 5:
                Pstats.exp += 20;
                break;
            case 6:
                Pstats.exp += 15;
                break;
            case 7:
                Pstats.exp += 10;
                break;
            case 8:
                Pstats.exp += 5;
                break;
            case 9:
                Pstats.exp += 2;
                break;
        }
        expreq = Mathf.Pow(Pstats.level + 3, 3) + 100;
        if(Pstats.exp > expreq)
        {
            Pstats.level += 1;
            Debug.Log("player leveled up to " + Pstats.level);
        }
        
        
        
        
        
    }
    public void Levelup()
    {

    }

}