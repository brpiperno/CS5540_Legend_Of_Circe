using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EmotionTypeExtension;

//Implementation of the IBattleMove interface
//that provides a basic but general purpose struct for all types of spell moves.
public class BasicMove : IBattleMove
{
    private int strength;
    private EmotionType emotion;
    private MoveType moveType;
    
    public BasicMove(int strength, EmotionType emotion,  MoveType moveType)
    {
        this.strength = strength;
        this.emotion = emotion;
        this.moveType = moveType;
    }

    public string toString()
    {
        return "Basic Move: strength:" + strength +
            " emotion: " + emotion.ToString() +
            "move: " + moveType.ToString();
    }

    public int getEffectStrength() {
        return strength;
    }

    public EmotionType GetEmotionType() {
        return emotion;
    }

    public MoveType GetMoveType() {
        return moveType;
    }

    public string getAnimationTrigger()
    {
        switch (this.moveType)
        {
            case MoveType.Null:
            case MoveType.Paralysis:
            case MoveType.Pharmaka:
                return moveType.ToString();
            default:
                return moveType.ToString() + "-" + emotion.ToString();
        }
    }
}
