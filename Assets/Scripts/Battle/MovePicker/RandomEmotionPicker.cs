using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime;
using UnityEngine;
using EmotionTypeExtension;

[RequireComponent(typeof(IEmotion))]
//A script that creates BattleMoves using a random choice of emotions available
public class RandomEmotionPicker : AbstractMovePicker
{
    private ISet<EmotionType> movesAvailable = new HashSet<EmotionType>();
    private IEmotion userEmotions;

    void Start()
    {
        userEmotions = GetComponent<IEmotion>();
        movesAvailable.Add(EmotionType.Love);
        movesAvailable.Add(EmotionType.Wrath);
        movesAvailable.Add(EmotionType.Mirth);
        movesAvailable.Add(EmotionType.Grief);
    }

    private void Update()
    { 
        if (isAskingForPlayInput)
        {
            PickMove();
            isAskingForPlayInput = false;
        }
    }

    private void PickMove()
    {
        Debug.Log("RandomEmotionPicker: moveRequested was called");
        System.Random rnd = new System.Random();
        int emotionChoice = rnd.Next(0, movesAvailable.Count - 1);
        EmotionType emtn = movesAvailable.ElementAt(emotionChoice);
        userEmotions.LoadNextMove(emtn, MoveType.Damage);
    }
}
