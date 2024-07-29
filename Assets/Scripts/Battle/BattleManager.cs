using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;
using EmotionTypeExtension;
using System.Linq;

//The BattleManager is a script that maintains the state of a battle and tells IEmotion instances when to attack.
public class BattleManager : MonoBehaviour
{
    //Level Management variables
    public string previousScene;
    public Text GameOverText;

    //Reworked Variables
    public EmotionSystem[] playersTeam;
    public EmotionSystem[] opponentTeam;
    private  List<EmotionSystem> turnOrder;
    public int turnIndex;
    public AudioClip gameOverSFX;
    public AudioClip gameWonSFX;

    // Start is called before the first frame update
    void Start()
    {

        if (playersTeam == null)
        {
            playersTeam = new EmotionSystem[1] { GameObject.FindGameObjectWithTag("Player").GetComponent<EmotionSystem>() };
        }
        if (opponentTeam == null)
        {
            throw new Exception("Opponent Team not set in inspector");
        }

        turnOrder = new List<EmotionSystem>();
        turnOrder.AddRange(playersTeam); //player's team goes first, then enemies
        turnOrder.AddRange(opponentTeam);
        turnIndex = 0;
        //TODO: Turn off other player controls or NPC behavior
        //TODO: Enable Battle Specific UI
        Debug.Log("BattleManager: created turn list of size " + turnOrder.Count);
        turnOrder[turnIndex].RequestNextMove();
    }

    /// <summary>
    /// Method to be used by IEmotion instances to submit a move chosen to the BattleManager to be enacted.
    /// </summary>
    /// <param name="move"> The move to be used</param>
    /// <param name="user"> The originator of the move</param>
    /// <param name="target">The target of the move</param>
    public void SubmitMove(IBattleMove move, IEmotion user, IEmotion target)
    {
        if (getPlayerIndex(user) != turnIndex)
        {
            return; //ignore out of turn moves
        } else
        {
            user.PlayMove();
            target.AcceptMove(move);
        }
    }

    /// <summary>
    /// Method used by IEmotion instances to notify the BattleManager that they have finished playing a move
    /// Tell the BattleManager to move onto the next turn
    /// </summary>
    /// <param name="user">The user that performed the move</param>
    public void CompleteMove(IEmotion user)
    {
        if (getPlayerIndex(user) != turnIndex)
        {
            return; //ignore out of turn completion calls
        }
        //increment the index, unles it is at its max,
        //in which set it back to zero and increment the round count
        turnIndex = (turnIndex == turnOrder.Count - 1) ? 0 : turnIndex + 1;
        turnOrder[turnIndex].RequestNextMove();//Ask the next person in line
    }

    /// <summary>
    /// End the battle, noting who lost. The BattleManager will use that info to determine any rewards or other behavior
    /// </summary>
    /// <param name="loser"></param>
    public void EndBattle(GameObject loser)
    {
        //end the battle
        //if the battleManager has an item held, give it to the player
        //load the previous scene if needed
        Debug.Log("Battle ended");
        if (loser.tag == "Player") {
            AudioSource.PlayClipAtPoint(gameOverSFX, Camera.main.transform.position);
        } else if (loser.tag == "Enemy") {
            AudioSource.PlayClipAtPoint(gameWonSFX, Camera.main.transform.position);
        }
    }

    private int getPlayerIndex(IEmotion player)
    {
        for (int i = 0; i < turnOrder.Count; i++)
        {
            if (turnOrder[i] == player)
            {
                return i;
            }
        }
        throw new ArgumentException("Player not managed by BattleManager called method");
    }

    public IEmotion GetEnemy(IEmotion player)
    {
        return (playersTeam.Contains(player)) ? opponentTeam[0] : playersTeam[0];
    }
}