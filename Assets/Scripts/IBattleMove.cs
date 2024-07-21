using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBattleMove
{
    public GameObject getUser();
    public GameObject getTarget();
    public void ApplyMove();
    public string toString();
    public string getAnimationTrigger();
}
