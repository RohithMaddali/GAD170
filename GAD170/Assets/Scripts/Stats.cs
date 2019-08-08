using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats : MonoBehaviour
{
    public float health;
    public int attack;
    public int speed;
    public int defense;
    public int luck;
    public float maxHealth;
    public int curExp;
    public int neededExp;
    public int level;

    public bool isDefeated;

    public enum StatusEffect
    {
        none,
        dizzy,
        poisoned,
        stunned,
        disabled
    }
    public enum Type
    {
        small,medium,large
    }
    public StatusEffect myStatus;
    public StatusEffect attackEffect;
    public void Attacked(int DMG, StatusEffect incEffect)
    {
        health -= DMG - defense;
        myStatus = incEffect;
        if (health <= 0)
            isDefeated = true;
    }
}
