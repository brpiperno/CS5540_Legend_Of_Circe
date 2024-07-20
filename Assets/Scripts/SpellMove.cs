using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellMove : IBattleMove
{
    EmotionType emotionType;
    SpellType spellType;

    public GameObject getUser();
    public GameObject getTarget();
    public void ApplyMove();
    public string toString();
}
