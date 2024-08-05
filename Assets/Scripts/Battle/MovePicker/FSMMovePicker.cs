using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EmotionTypeExtension;
using System;

/// <summary>
/// Simple Move Picker that chooses whatever move is super effective against 
/// </summary>
public class FSMMovePicker : AbstractMovePicker
{
    private readonly EmotionType[] emotionTypeArrayIndices = new EmotionType[4]
    {
        EmotionType.Love, EmotionType.Wrath, EmotionType.Grief, EmotionType.Mirth
    };
    public int currentMoodIndex = 0; //the default starting emotion of the NPC

    [Header("Transition Probability. Size must be 4. Order: Love, Wrath, Grief, Mirth")]
    public List<float> loveTransitions = new List<float>() { 0.25f, 0.25f, 0.25f, 0.25f };
    public List<float> griefTransitions = new List<float>() { 0.25f, 0.25f, 0.25f, 0.25f };
    public List<float> wrathTransitions = new List<float>() { 0.25f, 0.25f, 0.25f, 0.25f };
    public List<float> mirthTransitions = new List<float>() { 0.25f, 0.25f, 0.25f, 0.25f };
    public List<List<float>> transitionOdds = new List<List<float>>();


    // Start is called before the first frame update
    protected new void Start()
    {
        base.Start();
        transitionOdds.Add(loveTransitions);
        transitionOdds.Add(wrathTransitions);
        transitionOdds.Add(griefTransitions);
        transitionOdds.Add(mirthTransitions);
        foreach (List<float> transition in transitionOdds) {
            if (transition == null || transition.Count != 4)
            {
                transition.Clear();
                transition.AddRange( new float[] { 0.25f, 0.25f, 0.25f, 0.25f });
            }
        }
        for (int i = 0; i < emotionTypeArrayIndices.Length; i++)
        {
            if (emotionTypeArrayIndices[i] == userEmotionSystem.currentEmotion)
            {
                currentMoodIndex = i;
            }
        }
        Debug.Log("FSMMovePicker Start() in GameObject " + gameObject.name);
        
    }


    public new void UpdateLastMoveRecieved(IBattleMove received) 
    {
        base.UpdateLastMoveRecieved(received);
        //Update the row for the NPCs current mood
        for (int i = 0; i < emotionTypeArrayIndices.Length; i++)
        {
            //Double odds in the current mood for anything effective against the last move recieved
            if (emotionTypeArrayIndices[i].GetEffectivenessAgainst(received.GetEmotionType()) > 1)
            {
                transitionOdds[currentMoodIndex][i] = Mathf.Clamp(transitionOdds[currentMoodIndex][i] + 0.125f, 0, 1);
            }
            //Halve odds in the current mood for anything ineffective against the last move recieved.
            else if (emotionTypeArrayIndices[i].GetEffectivenessAgainst(received.GetEmotionType()) < 1)
            {
                transitionOdds[currentMoodIndex][i] = Mathf.Clamp(transitionOdds[currentMoodIndex][i] - 0.125f, 0, 1);
            }

            //TODO: readjust logic
        }
        Debug.Log("FSM transition odds updated:\n" + PrintArr(transitionOdds[currentMoodIndex].ToArray()));
    }

    public new void MoveRequested()
    {
        base.MoveRequested();
        //Debug.Log("FSMMovePicker: moveRequested reached");
        //Sum up the odds for the current mod FSM transitions, and pick a number in that range
        //transisition sum should always be 1 but just in case it ever isn't due to type effectiveness,
        //calculate it iteratively
        float transitionSum = 0;
        float[] transitionThresholds = new float[emotionTypeArrayIndices.Length];
        for (int i = 0; i < emotionTypeArrayIndices.Length; ++i)
        {
            transitionSum += transitionOdds[currentMoodIndex][i];
            transitionThresholds[i] = transitionSum;
        }
        Debug.Log("Calculating transitionThresholds: " + PrintArr(transitionThresholds) + ", transitionsum: " + transitionSum);
        float rng = UnityEngine.Random.Range(0, transitionSum);
        for (int i = 0; i < transitionThresholds.Length; ++i)
        {   
            if (transitionThresholds[i] >= rng )
            {
                userEmotionSystem.LoadNextMove(emotionTypeArrayIndices[i], MoveType.Damage);
                return;
            }
        }
        throw new Exception("Did not correctly calculate move with rng: " + rng + "using range: [0, " + transitionSum + "].");
    }

    private string PrintArr(float[] arr)
    {
        string output = " { ";
        
        foreach (var item in arr)
        {
            output += item + ", ";
        }
        output += "}\n";
        return output;
    }

}
