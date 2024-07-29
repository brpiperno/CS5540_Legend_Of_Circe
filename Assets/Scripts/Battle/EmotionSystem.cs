using System.Collections.Generic;
using UnityEngine;
using System;
using EmotionTypeExtension;

//Assign this script to Circe and all NPCs
[RequireComponent(typeof(VisualController))]
[RequireComponent(typeof(AbstractMovePicker))]
public class EmotionSystem : MonoBehaviour, IEmotion
{
    public GameObject playerAttackAnimationObject;
    public GameObject enemyAttackAnimationObject;
    public VisualController visualController;
    private Dictionary<EmotionType, float> emotionValues = new Dictionary<EmotionType, float> {
        {EmotionType.Wrath, 100},
        {EmotionType.Love, 100},
        {EmotionType.Grief, 100},
        {EmotionType.Mirth, 100}
    };
    private Dictionary<EmotionType, int> defenseModifiers = new Dictionary<EmotionType, int> {
        {EmotionType.Wrath, 1},
        {EmotionType.Love, 1},
        {EmotionType.Grief, 1},
        {EmotionType.Mirth, 1}
    };
    
    public EmotionType currentEmotion; //set some starting default emotion;
    public IBattleMove lastMoveUsed;
    public IBattleMove nextMove;
    public BattleManager battleManager; //The battle manager that it sends moves to;
    public AbstractMovePicker movePicker;
    public int baseStrength; //effectiveness of IBattleMoves instantiated, where applicable.

    void Start()
    {
        visualController = GetComponent<VisualController>();
        movePicker = GetComponent<AbstractMovePicker>();
    }

    public float GetEmotionValue(EmotionType type) {
        return emotionValues[type];
    }

    public void AcceptMove(IBattleMove move)
    {
        //take effect based on the moves spell and emotion type
        Debug.Log("Emotion System of " + name + ": Move: " + move.toString());
        switch (move.GetMoveType())
        {
            case MoveType.Enhancement:
                emotionValues[move.GetEmotionType()] += Mathf.Abs(move.getEffectStrength());
                visualController.updateEmotionBarUI();
                break;
            case MoveType.Shield:
                defenseModifiers[move.GetEmotionType()] *= 2;
                break;
            case MoveType.Transformation:
                LoadNextMove(move.GetEmotionType(), MoveType.Damage);
                break;
            case MoveType.Paralysis:
                LoadNextMove(currentEmotion, MoveType.Null);
                break;
            case MoveType.Damage:
                emotionValues[move.GetEmotionType()] -= 
                    Mathf.Abs(move.getEffectStrength())
                    * defenseModifiers[move.GetEmotionType()]
                    * move.GetEmotionType().GetEffectivenessAgainst(currentEmotion);
                visualController.updateEmotionBarUI();
                break;
            case MoveType.Pharmaka:
                battleManager.EndBattle(gameObject);
                break;
            case MoveType.Null:
                break; //do nothing. User of this move was stunned.
            default: throw new NotImplementedException();
        }
        CheckGameOver();
    }

    private void CheckGameOver()
    {
        foreach (EmotionType emotion in emotionValues.Keys)
        {
            if (emotionValues[emotion] <= 0)
            {
                battleManager.EndBattle(gameObject);
            }
        }
    }

    public void RequestNextMove()
    {
        Debug.Log("EmotionSystem: RequestNext move called on " + this.name);
        //if a next move was already loaded ( such as if stunned or transformed, use it)
        if (nextMove != null)
        {
            battleManager.SubmitMove(nextMove, this, battleManager.GetEnemy(this));
            return;
        }
        movePicker.MoveRequested();
    }

    public void LoadNextMove(EmotionType emotion, MoveType move)
    {
        nextMove = new BasicMove(this.baseStrength, emotion, move);
        //for shield and enhancement spells, the target is the user
        IEmotion target = (move == MoveType.Shield || move == MoveType.Enhancement) ?
            this : battleManager.GetEnemy(this); 
        battleManager.SubmitMove(nextMove, this, target);
    }

    // Method does not use the move variable so far
    public void PlayMove() {
        lastMoveUsed = nextMove;
        nextMove = null;

        visualController.setAnimationTrigger(lastMoveUsed.GetEmotionType(), lastMoveUsed.GetMoveType());
        battleManager.CompleteMove(this); //tell the battle manager that this user's turn is complete
    }
}