using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBattleMove
{
    //public EmotionType GetEmotionType();
    
    //Represent the IBattleMove as a string
    public string toString();
    public string getAnimationTrigger();
    public int getEffectStrength();
    public MoveType GetMoveType();
    public EmotionType GetEmotionType();
}
