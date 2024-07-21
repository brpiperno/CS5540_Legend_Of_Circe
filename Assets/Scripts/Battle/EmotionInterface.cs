using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface EmotionInterface
{

    int GetEmotion(EmotionType type);

    void ShiftEmotions(EmotionType emotion, int value);

    void PlayMove(IBattleMove move);

    void AcceptMove(IBattleMove move);

}