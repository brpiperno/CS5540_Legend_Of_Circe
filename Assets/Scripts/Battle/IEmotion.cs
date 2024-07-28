using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EmotionTypeExtension;

/// <summary>
/// Interface that describes the emotions used by NPCs and the player in battle.
/// </summary>
public interface IEmotion
{
    /// <summary>
    /// Get the IEmotion's current value for a given emotiontype.
    /// </summary>
    /// <param name="type"> The emotion type to get data about</param>
    /// <returns></returns>
    float GetEmotionValue(EmotionType type);

    /// <summary>
    /// Allow the IEmotion to enact its move. This is usually called by the BattleManager and is used for the IEmotion to set triggers for any animations or special effects
    /// </summary>
    void PlayMove();

    /// <summary>
    /// Have the IEmotion accept the provided move.
    /// </summary>
    /// <param name="move"></param>
    void AcceptMove(IBattleMove move);

    /// <summary>
    /// Notify the IEmotion that it should submit a move to the BattleManager.
    /// </summary>
    void RequestNextMove();

    /// <summary>
    /// Notify the IEmotion of 
    /// </summary>
    /// <param name="emotion"></param>
    /// <param name="move"></param>
    void LoadNextMove(EmotionType emotion, MoveType move);
}
