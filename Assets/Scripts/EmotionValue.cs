using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class EmotionValue : MonoBehaviour, EmotionInterface
{
    private Dictionary<EmotionType, int> emotionValues;
    //add each emotion type and set the initial values to 100;
    private Dictionary<EmotionType, int> defenseModifiers;
    //add each emotion type and set the initial values to 1;

    public int GetWrath()
    {
        return emotionValues[EmotionType.Wrath];
    }

    public int GetLove()
    {
        return emotionValues[EmotionType.Love];
    }

    public int GetGrief()
    {
        return emotionValues[EmotionType.Grief];
    }

    public int GetMirth()
    {
        return emotionValues[EmotionType.Mirth];
    }


    public void ShiftEmotion(EmotionType emotion, int value)
    {
        //get the current value of the chose Emotion type, and update it accordingly,
        //based on the min,max, current value, and defense modifier
    }

}
