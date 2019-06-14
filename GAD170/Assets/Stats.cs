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

    public enum StatusEffect
    {
        none,
        dizzy,
        poisoned,
        stunned,
        disabled
    }
    public StatusEffect myStatus;
    public StatusEffect attackEffect;
}
