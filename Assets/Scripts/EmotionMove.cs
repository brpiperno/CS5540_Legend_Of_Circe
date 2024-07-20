using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmotionMove : IBattleMove
{
    EmotionType emotionType;

    public EmotionMove(EmotionType e) {
        emotionType = e;
    }

    public GameObject getUser();
    public GameObject getTarget();
    public void ApplyMove();
    public string toString() {
        return "This is an emotion move done by " + getUser() + " against " + getTarget() + ". The emotion is " + emotionType;
    }
}
