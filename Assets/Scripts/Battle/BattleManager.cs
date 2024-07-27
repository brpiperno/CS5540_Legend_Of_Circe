using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;
using UnityEditor.PackageManager.Requests;

//The BattleManager is a script that maintains the state of a battle and tells IEmotion instances when to attack.
public class BattleManager : MonoBehaviour
{

    public GameObject circe;
    public GameObject opponent;
    public IEmotion playerSystem;
    public IEmotion opponentSystem;
    public IMovePicker enemyMovePicker;
    //public static float basePowerForMoves = 10f;
    // Replaced with an effect strength for each move
    bool isAskingForPlayerInput = true;
    bool isRoundFinished = false;



    //Level Management variables
    public string previousScene;
    public Text GameOverText;
    private bool isBattleFinished = false;


    //Reworked Variables
    public IEmotion[] playersTeam;
    public IEmotion[] opponentTeam;
    public bool isPlayersTeamsTurn;
    public int activePlayerIndex;
    public int currentRound;
    public bool askedCurrentPlayerForInput = false;
    public bool isCurrentPlayersMoveComplete = false;




    // Start is called before the first frame update
    void Start()
    {
        if (playersTeam == null)
        {
            playersTeam = new IEmotion[1] { GameObject.FindGameObjectWithTag("Player").GetComponent<EmotionSystem>() };
        }
        if (opponentTeam == null)
        {
            throw new Exception("Opponent Team not set in inspector");
        }
        currentRound = 0;
        activePlayerIndex = 0;
        isPlayersTeamsTurn = true;

        foreach (var player in playersTeam)
        {
            player.ReadyBattle();
        }
        foreach (var player in opponentTeam)
        {
            player.ReadyBattle();
        }

        //Turn off other player controls or NPC behavior

        //Enable Battle Specific UI
    }


    private void Updatev2()
    {
        if (askedCurrentPlayerForInput)
        {
            return;
        } else if (isPlayersTeamsTurn)
        {
            playersTeam[activePlayerIndex].RequestNextMove()
        } else
        {
            opponentTeam[activePlayerIndex].RequestNextMove();
        }
        askedCurrentPlayerForInput = true;
    }

    /// <summary>
    /// Method to be used by IEmotion instances to submit a move chosen to the BattleManager to be enacted.
    /// </summary>
    /// <param name="move"> The move to be used</param>
    /// <param name="user"> The originator of the move</param>
    /// <param name="target">The target of the move</param>
    public void SubmitMove(IBattleMove move, IEmotion user, IEmotion target)
    {
        if (user != getCurrentPlayer())
        {
            return; //ignore out of turn moves
        } else
        {
            user.PlayMove(move);
            target.AcceptMove(move);
        }
    }

    /// <summary>
    /// Method used by IEmotion instances to notify the BattleManager that they have finished Playing a move
    /// </summary>
    /// <param name="user">The user that performed the move</param>
    public void CompleteMove(IEmotion user)
    {
        //reset the asking for input variable
        askedCurrentPlayerForInput = false;

        IEmotion[] team = isPlayersTeamsTurn ? playersTeam : opponentTeam;
        if (activePlayerIndex == team.Length)
        {
            isPlayersTeamsTurn = !isPlayersTeamsTurn;
            activePlayerIndex = 0;
        }
        else
        {
            activePlayerIndex++;
        }

        //increment the index, unles it is at its max, in which case switch teams
    }

    private IEmotion getCurrentPlayer()
    {
        if (isPlayersTeamsTurn)
        {
            return playersTeam[activePlayerIndex];
        } else
        {
            return opponentTeam[activePlayerIndex];
        }
    }

    // Update is called once per frame
    void Update()
    {
        //int wrath = GetComponent<IEmotion>().GetWrath(); example on getting wrath values

        if (isAskingForPlayerInput && !isBattleFinished) {
            IBattleMove playersMove = null;
            if (Input.GetKeyDown("up")) {
                isAskingForPlayerInput = false;
                playersMove = new EmotionMove(EmotionType.Grief, 25);
                playerSystem.PlayMove(playersMove);
            } else if (Input.GetKeyDown("left")) {
                isAskingForPlayerInput = false;
                playersMove = new EmotionMove(EmotionType.Love, 25);
                playerSystem.PlayMove(playersMove);
            } else if (Input.GetKeyDown("right")) {
                isAskingForPlayerInput = false;
                playersMove = new EmotionMove(EmotionType.Wrath, 25);
                playerSystem.PlayMove(playersMove);
            } else if (Input.GetKeyDown("down")) {
                isAskingForPlayerInput = false;
                playersMove = new EmotionMove(EmotionType.Mirth, 25);
                playerSystem.PlayMove(playersMove);
            }
            else if (Input.GetKeyDown("space")) {
                useSpell();
            }
            if (playersMove != null) {
                IBattleMove opponentsMove = enemyMovePicker.GetBattleMove();
                if (playersMove.GetMoveType() == MoveType.Damage && opponentsMove.GetMoveType() == MoveType.Damage) {
                    opponentSystem.AcceptEmotionMove(playersMove as EmotionMove, opponentsMove as EmotionMove);
                    opponentSystem.PlayMove(opponentsMove);
                    playerSystem.AcceptEmotionMove(opponentsMove as EmotionMove, playersMove as EmotionMove);
                }
                isAskingForPlayerInput = true;
            }
        }

        //check for a battle draw condition:
        foreach (EmotionType e in Enum.GetValues(typeof(EmotionType))) {
            if (playerSystem.GetEmotion(e) <= 0)
            {
                //load previous level
                SceneManager.LoadScene(previousScene);
                isBattleFinished = true;
            }
            if (opponentSystem.GetEmotion(e) <= 0)
            {
                //show game won screen
                GameOverText.gameObject.SetActive(true);
                isBattleFinished = true;

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

    //public void submitMove(IBattleMove battleMove)
    //{
        //this is called by the player when they are ready to determine the effects of the move chosen


        //get the next move from the NPC
        //IBattleMove opponentMove = determineOpponentMove();

        // base number of points for each attack, specified as a variable able to be edited in the inspector?
        // Call getTypeChartMultiplier() and multiply this by the base number of points
        // Multiply this by circe.GetComponent<EmotionSystem>.getDefense(whichever_emotionType)
          // or opponent.GetComponent<EmotionSystem>.getDefense(whichever_emotionType)
        // update player's bars, aura
        // update opponent's bars, aura

        // If animations are different depending on the type chart multiplier, do them after calling getTypeChartMultiplier
        //Enact the caller's move, displaying appropriate animations and playing sounds
        //animateMoveForCirce(battleMove);
        //Enact the target's move, displaying appropriate animations and playing sounds
        //animateMoveForOpponent(opponentMove);

        //if the move was supereffective against the target, update the heart UI

        //check if battle over
            // heart UI is filled on enemy
            // all emotion values for a caller or target are 0
            // specific emotion goals for target are met
        //enable the caller to set an input for the next turn
        //isPlayerTurn = true;
    //}


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

     public void useSpell() {

     }
}
