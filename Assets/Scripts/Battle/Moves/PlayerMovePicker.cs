using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EmotionTypeExtension;

[RequireComponent(typeof(IEmotion))]
[RequireComponent(typeof(IVisualController))]
public class PlayerMovePicker : MonoBehaviour, IMovePicker
{

    private bool isAskingForPlayInput = false;
    private bool isEmotionChosen = false;
    public IVisualController visualController;
    public IEmotion userEmotionSystem;


    // Start is called before the first frame update
    void Start()
    {
        if (userEmotionSystem == null)
        {
            userEmotionSystem = GetComponent<IEmotion>();
        }
        if (visualController == null)
        {
            visualController = GetComponent<VisualController>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        EmotionType emotionChosen = EmotionType.Grief;
        if (Input.GetKeyDown("up"))
        {
            emotionChosen = EmotionType.Grief;
            isEmotionChosen = true;
            visualController.updateEmotionWheelSelection(emotionChosen);
        }
        else if (Input.GetKeyDown("left"))
        {
            emotionChosen = EmotionType.Love;
            isEmotionChosen = true;
            visualController.updateEmotionWheelSelection(emotionChosen);
        }
        else if (Input.GetKeyDown("right"))
        {   
            emotionChosen = EmotionType.Wrath;
            isEmotionChosen = true;
            visualController.updateEmotionWheelSelection(emotionChosen);
        }
        else if (Input.GetKeyDown("down"))
        {
            emotionChosen = EmotionType.Mirth;
            isEmotionChosen = true;
            visualController.updateEmotionWheelSelection(emotionChosen);
        }
        if (Input.GetKeyDown("space") && isEmotionChosen)
        {
            userEmotionSystem.LoadNextMove(emotionChosen, MoveType.Damage);
            isAskingForPlayInput = false;
            isEmotionChosen = false;
            visualController.setEmotionWheelVisibility(false);
        }
    }

    public void MoveRequested()
    {
        isAskingForPlayInput = true;
        visualController.setEmotionWheelVisibility(true);
    }
}
