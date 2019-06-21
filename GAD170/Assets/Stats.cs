using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats : MonoBehaviour
{
    public int health;
    public int attack;
    public int speed;
    public int defense;
    public int luck;

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
}
