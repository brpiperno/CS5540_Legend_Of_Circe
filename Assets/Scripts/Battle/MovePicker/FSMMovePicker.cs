using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EmotionTypeExtension;

/// <summary>
/// Simple Move Picker that chooses whatever move is super effective against 
/// </summary>
public class FSMMovePicker : AbstractMovePicker
{
    private EmotionType[] emotionTypeArrayIndices;
    private float[,] transitions;
    public int currentMoodIndex = 0; //the default starting emotion of the NPC

    


    // Start is called before the first frame update
    protected new void Start()
    {
        Debug.Log("FSMMovePicker Start() in GameObject " + gameObject.name);
        emotionTypeArrayIndices = new EmotionType[4] {
            EmotionType.Love, EmotionType.Wrath, EmotionType.Grief, EmotionType.Mirth
        };
        Debug.Log("Length of emotionTypeArrayIndices is " + emotionTypeArrayIndices.Length);

        float[,] fsmtransitionOdds = { 
            { 0.25f, 0.25f, 0.25f, 0.25f}, //Likelihood of transitions out of love
            { 0.25f, 0.25f, 0.25f, 0.25f}, //Likelihood of transitions out of wrath
            { 0.25f, 0.25f, 0.25f, 0.25f}, //Likelihood of transitions out of grief
            { 0.25f, 0.25f, 0.25f, 0.25f}, //Likelihood of transitions out of mirth    
        };

        transitions = fsmtransitionOdds;

        base.Start();
    }


    public new void UpdateLastMoveRecieved(IBattleMove received) 
    {
        //Update the row for the NPCs current mood
        for (int i = 0; i < emotionTypeArrayIndices.Length; i++)
        {
            //Double odds in the current mood for anything effective against the last move recieved
            if (emotionTypeArrayIndices[i].GetEffectivenessAgainst(received.GetEmotionType()) > 1)
            {
                transitions[currentMoodIndex, i] = Mathf.Clamp(transitions[currentMoodIndex, i] + 0.125f, 0, 1);
            }
            //Halve odds in the current mood for anything ineffective against the last move recieved.
            else if (emotionTypeArrayIndices[i].GetEffectivenessAgainst(received.GetEmotionType()) < 1)
            {
                transitions[currentMoodIndex, i] = Mathf.Clamp(transitions[currentMoodIndex, i] - 0.125f, 0, 1);
            }
        }
        Debug.Log("FSM transition odds updated:\n" + transitions);
    }

    public new void MoveRequested()
    {
        Debug.Log("FSMMovePicker: moveRequested reached");
        //Sum up the odds for the current mod FSM transitions, and pick a number in that range
        //transisition sum should always be 1 but just in case it ever isn't due to type effectiveness,
        //calculate it iteratively
        float transitionSum = 0;
        base.MoveRequested();
        float[] transitionThresholds = new float[emotionTypeArrayIndices.Length];
        for (int i = 0; i < emotionTypeArrayIndices.Length; ++i)
        {
            transitionSum += transitions[currentMoodIndex, i];
            transitionThresholds[i] = transitionSum;
        }
        float rng = Random.Range(0, transitionSum);
        for (int i = 0; i < transitionThresholds.Length; ++i)
        {   
            if (transitionThresholds[i] < rng )
            {
                userEmotionSystem.LoadNextMove(emotionTypeArrayIndices[i], MoveType.Damage);
                return;
            }
        }
        throw new System.Exception("Did not correctly calculate move with rng: " + rng + "using range: [0, " + transitionSum + "].");

    }

}
