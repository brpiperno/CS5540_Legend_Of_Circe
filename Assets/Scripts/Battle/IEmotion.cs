using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEmotion
{
    float GetEmotion(EmotionType type);

    float ShiftEmotions(EmotionType emotion, float value);

    void PlayMove(IBattleMove move);

    void AcceptEmotionMove(EmotionMove attacker, EmotionMove receiver);
}
