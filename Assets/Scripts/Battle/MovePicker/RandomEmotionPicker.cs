using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using EmotionTypeExtension;

//A script that creates BattleMoves using a random choice of emotions available
public class RandomEmotionPicker : AbstractMovePicker
{
    private ISet<EmotionType> movesAvailable = new HashSet<EmotionType>();

    protected new void Start()
    {
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
        if (userEmotionSystem == null) {
            Debug.Log("User Emotion System is null");
        }
        if (emtn == null) {
            Debug.Log("emtn is null");
        }
        userEmotionSystem = GetComponent<EmotionSystem>();
        userEmotionSystem.LoadNextMove(emtn, MoveType.Damage);
    }
}
