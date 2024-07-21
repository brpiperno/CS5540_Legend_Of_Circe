using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//An Interface for a strategy object that chooses IBattleMoves based on the state of its EmotionValue
public interface IMovePicker
{

    /// <summary>
    /// Method <c>GetBattleMove</c> Get the battlemove that this chooses.
    /// </summary>
    /// <returns></returns>
    public IBattleMove GetBattleMove();
}
