using System.Collections.Generic;
using UnityEngine;
using System;
using EmotionTypeExtension;

//Assign this script to Circe and all NPCs
[RequireComponent(typeof(VisualController))]
public class EmotionSystem : MonoBehaviour, IEmotion
{
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
    
    public EmotionType currentEmotion = EmotionType.Love; //set some starting default emotion this is updated with each move
    public IBattleMove lastMoveUsed;
    public IBattleMove nextMove;
    public BattleManager battleManager; //The battle manager that it sends moves to;
    public IMovePicker movePicker;
    public int baseStrength = 10; //effectiveness of IBattleMoves instantiated, where applicable.
    public float enemySpellAnimationDelay = 1.7f;
    public bool hasFSM = false;

    void Start()
    {
        if (visualController == null)
        {
            visualController = GetComponent<VisualController>();
        }
        //if (movePicker == null)
        //{
        //    movePicker = GetComponent<IMovePicker>();
        //}
        //Debug.Log("Is movePicker null?");
        //Debug.Log(gameObject.name + " Is movePicker null? " + (movePicker == null).ToString());
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
                battleManager.EndBattle(this);
                break;
            case MoveType.Null:
                break; //do nothing. User of this move was stunned.
            default: throw new NotImplementedException();
        }
        CheckGameOver();
        movePicker = GetComponent<IMovePicker>();
        movePicker.UpdateLastMoveRecieved(move);
    }

    private void CheckGameOver()
    {
        foreach (EmotionType emotion in emotionValues.Keys)
        {
            if (emotionValues[emotion] <= 0)
            {
                battleManager.EndBattle(this);
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
            Debug.Log("Skipping turn, next move already loaded:" + nextMove.toString());
            return;
        }
        movePicker = GetComponent<IMovePicker>();
        /*
        if (gameObject.CompareTag("Player")) {
            movePicker.MoveRequested();
        } else
        {
            //TODO: debug why this isn't working with casting
            (movePicker as FSMMovePicker).MoveRequested();
        }
        */
        if (hasFSM) {
            (movePicker as FSMMovePicker).MoveRequested();
        } else {
            movePicker.MoveRequested();
        }
        Debug.Log("called moveRequested on movePicker in ");
    }

    public void LoadNextMove(EmotionType emotion, MoveType move)
    {
        nextMove = new BasicMove(this.baseStrength, emotion, move);
        Debug.Log("LoadNextMove has created " + nextMove.toString());
        //for shield and enhancement spells, the target is the user
        EmotionSystem target = (move == MoveType.Shield || move == MoveType.Enhancement) ?
            this : battleManager.GetEnemy(this); 
        battleManager.SubmitMove(nextMove, this, target);
    }

    // Method does not use the move variable so far
    public void PlayMove() {
        lastMoveUsed = nextMove;
        nextMove = null;
        currentEmotion = lastMoveUsed.GetEmotionType();
        visualController.setAnimationTrigger(lastMoveUsed.GetEmotionType(), lastMoveUsed.GetMoveType());
        battleManager.CompleteMove(this); //tell the battle manager that this user's turn is 
        // Starts the opponent spell cast animation during the player's turn, because the animation takes a bit of time to start
        if (gameObject.tag == "Player") {
            Debug.Log("Reached line 121");
            Invoke("PlayEnemySpellCastAnimation", enemySpellAnimationDelay);
            visualController.PlayEnemyBlockAnimation();
        }
        StartCoroutine(visualController.setAnimationTrigger(lastMoveUsed.GetEmotionType(), lastMoveUsed.GetMoveType()));
        Invoke("FinishTurn", 2);
    }

    private void FinishTurn()
    {
        battleManager.CompleteMove(this);
    }

    private void PlayEnemySpellCastAnimation() {
        visualController.PlayEnemySpellCastAnimation();
    }
}