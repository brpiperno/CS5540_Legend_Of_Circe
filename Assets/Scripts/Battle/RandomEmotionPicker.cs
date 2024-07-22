using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime;

//A script that creates BattleMoves using a random choice of emotions available
public class RandomEmotionPicker : IMovePicker
{
    private float minEmotionValue = 10.0f; //the minimum amount of emotion needed to use a basic move
    private ISet<EmotionType> movesAvailable;
    private IEmotion[] target;
    private IEmotion user;

    public RandomEmotionPicker(IEmotion userEmotionValue, IEmotion[] targets)
    {
        minEmotionValue = 10; //the minimum amount of emotion needed to use a basic move
        movesAvailable = new HashSet<EmotionType>();
        target = targets;

        movesAvailable.Add(EmotionType.Love);
        movesAvailable.Add(EmotionType.Wrath);
        movesAvailable.Add(EmotionType.Mirth);
        movesAvailable.Add(EmotionType.Grief);
    }

    public IBattleMove GetBattleMove()
    {
        List<EmotionType> availableOptions = movesAvailable.Where(v => user.GetEmotion(v) >= minEmotionValue).ToList();
        Random rnd = new Random();
        int emotionChoice  = rnd.Next(0, availableOptions.Count - 1);
        int targetChoice = rnd.Next(0, target.Length - 1);
        return new EmotionMove(availableOptions[emotionChoice], 20);
    }
}
