using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class EmotionValue : MonoBehaviour, EmotionInterface
{
    //add each emotion type and set the initial values to 100;
    private Dictionary<EmotionType, int> emotionValues =
        new Dictionary<EmotionType, int> {
            {EmotionType.Wrath, 100},
            {EmotionType.Love, 100},
            {EmotionType.Grief, 100},
            {EmotionType.Mirth, 100}
        };
    
    //add each emotion type and set the initial values to 1;
    private Dictionary<EmotionType, int> defenseModifiers =
        new Dictionary<EmotionType, int> {
            {EmotionType.Wrath, 1},
            {EmotionType.Love, 1},
            {EmotionType.Grief, 1},
            {EmotionType.Mirth, 1}
        };
    

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

    public int GetEmotionValue(EmotionType type)
    {
        return emotionValues[type];
    }

    public void ShiftEmotion(EmotionType emotion, int value)
    {
        //get the current value of the chose Emotion type, and update it accordingly,
        //based on the min,max, current value, and defense modifier

        //TODO: update UI for that bar
    }

}
