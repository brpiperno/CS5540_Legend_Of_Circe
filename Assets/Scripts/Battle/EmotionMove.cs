using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class EmotionMove : IBattleMove
{
    EmotionType emotionType;
    int EmotionCost;
    int effectStrength;

    public EmotionMove(EmotionType e, int effectStrength = 10)
    {
        emotionType = e;
        this.effectStrength = effectStrength;
    }
    public EmotionType GetEmotionType()
    {
        return emotionType;
    }
    public string toString()
    {
        return "This is a " + emotionType + " emotion move.";
    }
    public int getEffectStrength()
    {
        return effectStrength;
    }
    public string getAnimationTrigger()
    {
        return null;
    }
}
