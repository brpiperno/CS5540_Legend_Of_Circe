using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellMove : IBattleMove
{
    EmotionType emotionType;
    SpellType spellType;

    public GameObject getUser()
    {
        return null;
    }
    public GameObject getTarget()
    {
        return null;
    }
    public void ApplyMove()
    {
        return;
    }
    public string toString()
    {
        return null;
    }
}
