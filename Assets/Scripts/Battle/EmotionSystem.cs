using System.Collections.Generic;
using UnityEngine;
using System;
using EmotionTypeExtension;
using UnityEngine.UI;
using TMPro;

//Assign this script to Circe and all NPCs
//[RequireComponent(typeof(VisualController))]
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
    public EmotionType currentEmotion = EmotionType.Null; //set some starting default emotion this is updated with each move
    public IBattleMove lastMoveUsed = new BasicMove(-1, EmotionType.Null, MoveType.Null);
    public IBattleMove nextMove = new BasicMove(-1, EmotionType.Null, MoveType.Null);
    public BattleManager battleManager; //The battle manager that it sends moves to;
    public IMovePicker movePicker;
    public int baseStrength; //effectiveness of IBattleMoves instantiated, where applicable.
    public float enemySpellAnimationDelay = 1.7f;
    private bool isStunned = false;
    private bool isTransformed = false;

    private BattleTextQueue BattleText;

    //dialogue options when using a damage move of the appropriate emotion type.
    public string[] WrathDialogue;
    public string[] MirthDialogue;
    public string[] GriefDialogue;
    public string[] LoveDialogue;
    private Dictionary<EmotionType, string[]> dialogueOptions;

    void Awake()
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
        BattleText = GameObject.FindGameObjectWithTag("BattleText").GetComponent<BattleTextQueue>();
        dialogueOptions = new Dictionary<EmotionType, string[]>() {
            {EmotionType.Grief, GriefDialogue },
            {EmotionType.Mirth, MirthDialogue },
            {EmotionType.Wrath, WrathDialogue },
            {EmotionType.Love, LoveDialogue }
        };
        if (battleManager == null)
        {
            battleManager = GameObject.FindGameObjectWithTag("BattleManager").GetComponent<BattleManager>();
        }
    }

    public float GetEmotionValue(EmotionType type)
    {
        return emotionValues[type];
    }

    public void AcceptMove(IBattleMove move)
    {
        //take effect based on the moves spell and emotion type
        Debug.Log("Emotion System of " + name + " has accepted move: Move: " + move.toString());
        switch (move.GetMoveType())
        {
            case MoveType.Enhancement:
                emotionValues[move.GetEmotionType()] += Mathf.Abs(move.getEffectStrength());
                visualController.updateEmotionBarUI();
                break;
            case MoveType.Shield:
                defenseModifiers[move.GetEmotionType()] *= 2;
                visualController.UpdateDefense(move.GetEmotionType(), defenseModifiers[move.GetEmotionType()]);
                break;
            case MoveType.Transformation:
                LoadNextMove(move.GetEmotionType(), MoveType.Damage);
                Debug.Log("Transformed users next move to be damage, type :" + move.GetMoveType().ToString());
                isTransformed = true;
                break;
            case MoveType.Paralysis:
                isStunned = true;
                break;
            case MoveType.Damage:
                int baseDamage = move.getEffectStrength();
                float defense = defenseModifiers[move.GetEmotionType()];
                float effectiveness = move.GetEmotionType().GetEffectivenessAgainst(currentEmotion);
                float damageDealt = (baseDamage * effectiveness) / defense;
                float currentHealth = emotionValues[move.GetEmotionType()];
                emotionValues[move.GetEmotionType()] = Mathf.Clamp(currentHealth - damageDealt, 0, 100);
                visualController.updateEmotionBarUI();
                //Debug.Log(String.Format("Took %d damage. Defense: %.2f. Effectiveness: %.2f. Base Strength: %d", ));
                break;
            case MoveType.Pharmaka:
                battleManager.EndBattle(this);
                break;
            case MoveType.Null:
                break; //do nothing. User of this move was stunned.
            default: throw new NotImplementedException();
        }
        BattleText.Enqueue(GetAcceptedMoveText(name, move, currentEmotion), false);
        CheckGameOver();
        //movePicker = GetComponent<IMovePicker>();
        if (!gameObject.CompareTag("Player"))
        {
            GetComponent<FSMMovePicker>().UpdateLastMoveRecieved(move);
            ////(movePicker as FSMMovePicker).UpdateLastMoveRecieved(move);
        }
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
        //Debug.Log("EmotionSystem: RequestNext move called on " + this.name);
        //if a next move was already loaded ( such as if stunned or transformed, use it)
        if (isStunned)
        {
            battleManager.SubmitMove(new BasicMove(0, EmotionType.Null, MoveType.Null), this, battleManager.GetEnemy(this));
            isStunned = false;
            return;
        } else if (isTransformed)
        {
            battleManager.SubmitMove(nextMove, this, battleManager.GetEnemy(this));
            isTransformed = false;
            return;
        }
        //Debug.Log("Line 107: " + gameObject.name + " Is movePicker null? " + (movePicker == null).ToString());
        movePicker = GetComponent<IMovePicker>();
        
        if (gameObject.tag == "Player") {
            BattleText.Enqueue("Pick a move.", false);
            visualController = gameObject.GetComponent<VisualController>();
            Debug.Log("EmotionSystem: " + name);
            EmotionSystem enemy = battleManager.GetEnemy(this);
            IBattleMove enemiesMove = enemy.lastMoveUsed;
            Debug.Log("EmotionSystem: visualController is null: " + (visualController == null));
            visualController.UpdateSuperEffectiveHits(enemiesMove.GetEmotionType());
            //visualController.UpdateSuperEffectiveHits(battleManager.GetEnemy(this).lastMoveUsed.GetEmotionType());
            movePicker.MoveRequested();
        } else
        {
            //TODO: debug why this isn't working with casting
            (movePicker as FSMMovePicker).MoveRequested();
        }
        //Debug.Log("called moveRequested on movePicker in " + this.name);
    }

    public void LoadNextMove(EmotionType emotion, MoveType move)
    {
        nextMove = new BasicMove(baseStrength, emotion, move);
        //Debug.Log("LoadNextMove has created " + nextMove.toString());
        //for shield and enhancement spells, the target is the user
        EmotionSystem target = (move == MoveType.Shield || move == MoveType.Enhancement) ?
            this : battleManager.GetEnemy(this); 
        battleManager.SubmitMove(nextMove, this, target);
    }

    // Method does not use the move variable so far
    public void PlayMove() {
        lastMoveUsed = nextMove;
        nextMove = new BasicMove(0, EmotionType.Null, MoveType.Null);
        currentEmotion = lastMoveUsed.GetEmotionType();
        //visualController.setAnimationTrigger(lastMoveUsed.GetEmotionType(), lastMoveUsed.GetMoveType());
        //battleManager.CompleteMove(this); //tell the battle manager that this user's turn is 
        // Starts the opponent spell cast animation during the player's turn, because the animation takes a bit of time to start
        if (gameObject.tag == "Player" && lastMoveUsed.GetMoveType() == MoveType.Damage) {
            visualController.PlayCirceSpellCastAnimation();
            Invoke("PlayEnemySpellCastAnimation", enemySpellAnimationDelay);
            visualController.PlayEnemyBlockAnimation();
        }
        if (!lastMoveUsed.GetMoveType().Equals(MoveType.Null)) //don't play a move if stunned
        {
            StartCoroutine(visualController.setAnimationTrigger(lastMoveUsed.GetEmotionType(), lastMoveUsed.GetMoveType()));
        }
        string msg = GetPlayMoveText(name, lastMoveUsed);
        if (lastMoveUsed.GetEmotionType() != EmotionType.Null)
        {
            System.Random rnd = new System.Random();
            int maxIndex = dialogueOptions[lastMoveUsed.GetEmotionType()].Length - 1;
            msg += "\n";
            msg += name + ": " + dialogueOptions[lastMoveUsed.GetEmotionType()][rnd.Next(0, maxIndex)];
        }
        BattleText.Enqueue(msg, true);
        Invoke("FinishTurn", 5);
    }

    private void FinishTurn()
    {
        battleManager.CompleteMove(this);
    }

    private void PlayEnemySpellCastAnimation() {
        visualController.PlayEnemySpellCastAnimation();
        visualController.PlayCirceBlockAnimation();
    }

    private string GetPlayMoveText(string name, IBattleMove movePlayed)
    {
        switch (movePlayed.GetMoveType())
        {
            case MoveType.Null:
                return string.Format("{0} was stunned! They were unable to speak!", name);
            case MoveType.Damage:
                return string.Format("{0} spoke with {1}!", name, movePlayed.GetEmotionType().ToString());
            default:
                return string.Format("{0} used a {1}.", name, PotionCraftingUIManager.getPotionName(movePlayed.GetEmotionType(), movePlayed.GetMoveType()));
        }
    }

    private string GetAcceptedMoveText(string name, IBattleMove moveAccepted, EmotionType defendingEmotion)
    {
        switch (moveAccepted.GetMoveType())
        {
            case MoveType.Null:
                return "";
            case MoveType.Damage:
                float effectiveness = moveAccepted.GetEmotionType().GetEffectivenessAgainst(defendingEmotion);
                if (effectiveness > 1)
                {
                    return string.Format("{0}'s {1} was vulnerable to {2}.", name, defendingEmotion.ToString(), moveAccepted.GetEmotionType().ToString());
                }
                else if (effectiveness < 1)
                {
                    return string.Format("{0}'s {1} made them resist words of {2}.", name, defendingEmotion.ToString(), moveAccepted.GetEmotionType().ToString());
                }
                else
                {
                    return "";
                }
            case MoveType.Paralysis:
                return string.Format("{0} was stunned", name);
            case MoveType.Enhancement:
                return string.Format("{0}'s %s was restored!", name, moveAccepted.GetEmotionType().ToString());
            case MoveType.Pharmaka:
                return string.Format("With the power of Circe's Pharmaka, {0} has transcended into divinity!", name);
            case MoveType.Shield:
                return string.Format("{0} raised their resistanced to words of {1}", name, moveAccepted.GetEmotionType().ToString());
            case MoveType.Transformation:
                return string.Format("{0} was enchanted to think thoughts of {1}. Their next move will be type: {1}", name, moveAccepted.GetEmotionType().ToString());
            default:
                throw new Exception("Unaccounted movetype" +  moveAccepted.GetMoveType().ToString());
                
        }
    }
}
