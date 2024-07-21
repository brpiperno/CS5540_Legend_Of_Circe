using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBattleMove
{

    //Get the user of this IBattleMove
    public EmotionValue getUser();
    
    //Get the target of this IBattleMove
    public EmotionValue getTarget();
    
    //Apply the effects of the IBattleMove
    public void ApplyMove();
    
    //Represent the IBattleMove as a string
    public string toString();
    public string getAnimationTrigger();
}
