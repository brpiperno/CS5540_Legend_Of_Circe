using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface EmotionInterface
{
    int GetWrath();
    int GetLove();
    int GetGrief();
    int GetMirth();

    void ShiftEmotion(EmotionType emotion, int value);



}