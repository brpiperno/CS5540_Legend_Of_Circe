using EmotionTypeExtension;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBattleMove
{
    //public EmotionType GetEmotionType();
    
    //Represent the IBattleMove as a string
    public string toString();

    //Get the animation trigger to be used by the visual controller to set appropriate UI changes or animations.
    //Most Triggers are derived from a combination of the movetype and emotion type where applicable.
    public string getAnimationTrigger();

    //Get the move effectiveness strength, where applicable
    public int getEffectStrength();

    /// <summary>
    /// Get the type of move being used.
    /// </summary>
    /// <returns></returns>
    public MoveType GetMoveType();

    /// <summary>
    /// Get the emotion type of the move being used. For some move types, the emotion type has no impact.
    /// </summary>
    /// <returns>The moves emotion type</returns>
    public EmotionType GetEmotionType();
}
