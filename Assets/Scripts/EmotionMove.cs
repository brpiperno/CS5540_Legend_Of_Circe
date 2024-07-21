using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmotionMove : IBattleMove
{
    private EmotionType emotionType;
    private EmotionValue user;
    private EmotionValue target;
    private int effectStrength;

    public EmotionMove(EmotionType e, EmotionValue user, EmotionValue target, int effectStrength) {
        emotionType = e;
        this.user = user;
        this.target = target;
        this.effectStrength = effectStrength;
    }

    public EmotionValue getUser()
    {
        return user;
    }
    public EmotionValue getTarget()
    {
        return target;
    }
    public void ApplyMove()
    {
        target.ShiftEmotion(emotionType, effectStrength);
    }
    public string toString() {
        return "This is an emotion move done by " + getUser() + " against " + getTarget() + ". The emotion is " + emotionType;
    }
}
