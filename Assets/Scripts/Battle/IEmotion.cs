using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEmotion
{
    float GetEmotion(EmotionType type);

    float ShiftEmotions(EmotionType emotion, float value);

    void PlayMove(IBattleMove move);

    void AcceptEmotionMove(EmotionMove attacker, EmotionMove receiver);

    // Sets it to true only (Invoke only works on functions with no parameters)
    void SetAskInput();
    // Highlights the arrow that was pressed by calling VisualController. For direction - 0 means up, 1 means left, 2 means right, 
    // and 3 means down
    public void HighlightArrow(int direction);
    // Returns all arrows to their original color.
    public void RemoveHighlight();
}
