using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//An Interface for a strategy object that chooses IBattleMoves based on the state of its EmotionValue
public interface IMovePicker
{
    /// <summary>
    /// Set the IMovePicker to determine another move 
    /// (which it provides using the LoadNextMove method in the IEmotion interface once determined)
    /// </summary>
    public void MoveRequested();

    /// <summary>
    /// Update the IMovePicker to decide based on the last move its EmotionSystem received.
    /// </summary>
    /// <param name="received">The battlemove received</param>
    void UpdateLastMoveRecieved(IBattleMove received);
}
