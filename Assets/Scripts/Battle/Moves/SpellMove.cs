using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellMove : IBattleMove
{
    MoveType spellType;
    int strength;

    public SpellMove(MoveType spellType, int strength) {
        this.spellType = spellType;
        this.strength =  strength;
    }
    public string toString()
    {
        return "This is a spell of type " + spellType;
    }
    public string getAnimationTrigger() {
        return null;
    }
    public int getEffectStrength() {
        return strength;
    }
    public MoveType GetMoveType() {
        return spellType;
    }
}
