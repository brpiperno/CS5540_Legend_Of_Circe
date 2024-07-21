using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellMove : IBattleMove
{
    EmotionType emotionType;
    SpellType spellType;

    public EmotionType GetEmotionType() {
        return emotionType;
    }
    public string toString()
    {
        return null;
    }
}
