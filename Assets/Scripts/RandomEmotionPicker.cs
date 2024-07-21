using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime;

//A script that creates BattleMoves using a random choice of emotions available
public class RandomEmotionPicker : IMovePicker
{
    private int minEmotionValue = 10; //the minimum amount of emotion needed to use a basic move
    private ISet<EmotionType> movesAvailable;
    private EmotionValue[] targetEmotionValues; //TODO: confirm if this should be a type EmotionValue or GameObject
    private EmotionValue userEmotionValue;

    public RandomEmotionPicker(EmotionValue userEmotionValue, EmotionValue[] targets)
    {
        minEmotionValue = 10; //the minimum amount of emotion needed to use a basic move
        movesAvailable = new HashSet<EmotionType>();
        targetEmotionValues = targets; //TODO: confirm if this should be a type EmotionValue or GameObject

        movesAvailable.Add(EmotionType.Love);
        movesAvailable.Add(EmotionType.Wrath);
        movesAvailable.Add(EmotionType.Mirth);
        movesAvailable.Add(EmotionType.Grief);
    }

    public IBattleMove GetBattleMove()
    {
        List<EmotionType> availableOptions = movesAvailable.Where(v => userEmotionValue.GetEmotionValue(v) >= minEmotionValue).ToList();
        Random rnd = new Random();
        int emotionChoice  = rnd.Next(0, availableOptions.Count - 1);
        int targetChoice = rnd.Next(0, targetEmotionValues.Length - 1);
        return new EmotionMove(availableOptions[emotionChoice], userEmotionValue, targetEmotionValues[targetChoice], 20);
    }
}
