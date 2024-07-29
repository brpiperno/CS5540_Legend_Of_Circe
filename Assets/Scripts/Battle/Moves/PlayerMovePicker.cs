using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EmotionTypeExtension;

[RequireComponent(typeof(IEmotion))]
[RequireComponent(typeof(IVisualController))]
public class PlayerMovePicker : MonoBehaviour, IMovePicker
{
    public IVisualController visualController;
    public IEmotion userEmotionSystem;
    public GameObject spacePrompt;
    private bool isAskingForPlayInput = false;
    private bool isEmotionChosen = false;


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
        spacePrompt = GameObject.FindGameObjectWithTag("Space");
    }

    // Update is called once per frame
    void Update()
    {
        if (!isAskingForPlayInput) { return; } //do nothing when out of turn

        EmotionType emotionChosen = EmotionType.Grief;
        if (Input.GetKeyDown("up"))
        {
            spacePrompt.SetActive(true);
            emotionChosen = EmotionType.Grief;
            isEmotionChosen = true;
            visualController.updateEmotionWheelSelection(emotionChosen);
        }
        else if (Input.GetKeyDown("left"))
        {
            spacePrompt.SetActive(true);
            emotionChosen = EmotionType.Love;
            isEmotionChosen = true;
            visualController.updateEmotionWheelSelection(emotionChosen);
        }
        else if (Input.GetKeyDown("right"))
        {   
            spacePrompt.SetActive(true);
            emotionChosen = EmotionType.Wrath;
            isEmotionChosen = true;
            visualController.updateEmotionWheelSelection(emotionChosen);
        }
        else if (Input.GetKeyDown("down"))
        {
            spacePrompt.SetActive(true);
            emotionChosen = EmotionType.Mirth;
            isEmotionChosen = true;
            visualController.updateEmotionWheelSelection(emotionChosen);
        }
        if (Input.GetKeyDown("space") && isEmotionChosen)
        {
            spacePrompt.SetActive(false);
            userEmotionSystem.LoadNextMove(emotionChosen, MoveType.Damage);
            isAskingForPlayInput = false;
            isEmotionChosen = false;
            visualController.setEmotionWheelVisibility(false);
            visualController.RemoveHighlight();
        }
    }

    public void MoveRequested()
    {
        isAskingForPlayInput = true;
        visualController.setEmotionWheelVisibility(true);
    }
}
