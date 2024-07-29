using UnityEngine;
using EmotionTypeExtension;

[RequireComponent(typeof(IEmotion))]
[RequireComponent(typeof(IVisualController))]
public class PlayerMovePicker : AbstractMovePicker
{
    public GameObject spacePrompt;
    public EmotionType emotionChosen = EmotionType.Grief;
    protected bool isEmotionChosen = false;

    // Update is called once per frame
    void Update()
    {
        
        if (!isAskingForPlayInput) { return; } //do nothing when out of turn
        Debug.Log("Player's turn and player input is requested");
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
        visualController.setEmotionWheelVisibility(true);
        base.MoveRequested();
    }
}
