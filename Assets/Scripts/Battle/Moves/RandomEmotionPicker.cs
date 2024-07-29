using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime;
using UnityEngine;
using EmotionTypeExtension;

[RequireComponent(typeof(IEmotion))]
[RequireComponent(typeof(IVisualController))]
//A script that creates BattleMoves using a random choice of emotions available
public class RandomEmotionPicker : MonoBehaviour, IMovePicker
{
    private int minEmotionValue = 10; //the minimum amount of emotion needed to use a basic move
    private ISet<EmotionType> movesAvailable = new HashSet<EmotionType>();
    private IEmotion[] targetEmotions; //TODO: confirm if this should be a type EmotionValue or GameObject
    private IEmotion userEmotions;
    private IVisualController visualController;



    void Start()
    {
        if (targetEmotions.Length == 0)
        {
            targetEmotions = new IEmotion[1] {
                GameObject.FindGameObjectWithTag("Player").GetComponent<IEmotion>()
            };
        }
        userEmotions = GetComponent<IEmotion>();
        visualController = GetComponent<VisualController>();

        movesAvailable.Add(EmotionType.Love);
        movesAvailable.Add(EmotionType.Wrath);
        movesAvailable.Add(EmotionType.Mirth);
        movesAvailable.Add(EmotionType.Grief);
    }

    public void MoveRequested()
    {
        System.Random rnd = new System.Random();
        int emotionChoice  = rnd.Next(0, movesAvailable.Count - 1);
        EmotionType emtn = movesAvailable.ElementAt(emotionChoice);
        userEmotions.LoadNextMove(emtn, MoveType.Damage);
    }
}
