using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;
using UnityEngine.SceneManagement;

public class BattleManager : MonoBehaviour
{
    bool isPlayerTurn = true;

    public GameObject circe;
    public GameObject opponent;

    // Start is called before the first frame update
    void Start()
    {
        if (circe == null) {
            circe = GameObject.FindGameObjectWithTag("Player");
        }
    }

    // Update is called once per frame
    void Update()
    {
        //int wrath = GetComponent<EmotionInterface>().GetWrath(); example on getting wrath values

        if (isPlayerTurn) {
            if (Input.GetKeyDown("up")) {
                submitMove(new EmotionMove(EmotionType.Grief));
            } else if (Input.GetKeyDown("left")) {
                submitMove(new EmotionMove(EmotionType.Love));
            } else if (Input.GetKeyDown("right")) {
                submitMove(new EmotionMove(EmotionType.Wrath));
            } else if (Input.GetKeyDown("down")) {
                submitMove(new EmotionMove(EmotionType.Mirth));
            } else if (Input.GetKeyDown("space")) {
                openSpellMenu();
            }
        }
    }

    /** Battle starts when we open the battle scene?
    public void StartBattle(GameObject caller, GameObject target)
    {
        //get the player on one side
        //get the NPC being interacted with on the other

        //bring up any game UIs (emotion values, heart indicators, spell indicators)
        SceneManager.LoadScene("BattleScreen");

        //set all values for the caller and target emotions

        //enable the caller to set an input. Once that input is set, then use that input and the input from the target to determine the effects of the turn
        playersTurn();
    }
    **/

    public void submitMove(IBattleMove battleMove)
    {
        //this is called by the player when they are ready to determine the effects of the move chosen


        //get the next move from the NPC
        IBattleMove opponentMove = determineOpponentMove();

        // base number of points for each attack, specified as a variable able to be edited in the inspector?
        // Call getTypeChartMultiplier() and multiply this by the base number of points
        // Multiply this by circe.GetComponent<EmotionValue>.getDefense(whichever_emotionType)
          // or opponent.GetComponent<EmotionValue>.getDefense(whichever_emotionType)
        // update player's bars, aura
        // update opponent's bars, aura

        // If animations are different depending on the type chart multiplier, do them after calling getTypeChartMultiplier
        //Enact the caller's move, displaying appropriate animations and playing sounds
        animateMoveForCirce(battleMove);
        //Enact the target's move, displaying appropriate animations and playing sounds
        animateMoveForOpponent(opponentMove);

        //if the move was supereffective against the target, update the heart UI

        //check if battle over
            // heart UI is filled on enemy
            // all emotion values for a caller or target are 0
            // specific emotion goals for target are met
        //enable the caller to set an input for the next turn
        isPlayerTurn = true;
    }


    // Battle move is one of:
    // - a dialogue move
    // - a spell
    // - a special move from an NPC

    /*
     * A Battle move has the following:
     * - A Target
     * - An Origin
     * - an effect (higher order function that changes a target's emotion)
     * 
     * A BattleMove has the following method:
     * - BattleMove.Apply() {
     *      //get the target's current emotion
     *      // determine if the move is normally effective, super effective, or not really effective (depends on the target, whether you are affecting yourself or an enemy)
     *      //call on the target's emotion component to change the emotion by a given value
     *      Target.GetEmotion().changeEmotion(EmotionType.Wrath, -20); example of what it looks like
     *      //check if the target is an NPC and the origin is the player, and the move was super effective, update heart UI
     *      //else if the target is an NPC and the origin is the player, and the move was not super effecive, reset the heart UI
     * }
     */


    //Emotion has the following
    /*
     * A mirth level
     * A wrath level
     * A love level
     * A grief level
     * For each type of emotion, it also has a defense modifier that determines how effective BattleMoves are at affecting it
     */

     public void openSpellMenu() {

     }

     public IBattleMove determineOpponentMove() {
        return null;
     }

<<<<<<< HEAD
     public void animateMoveForCirce(IBattleMove move) {
=======
     /*public void animateMoveForCirce() {
>>>>>>> aec8856efcb76c092a12ce1de37bb0270e32206a

     }
    public void animateMoveForOpponent(IBattleMove move) {

     }*/
}
