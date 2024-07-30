using UnityEngine;
using EmotionTypeExtension;

[RequireComponent(typeof(EmotionSystem))]
[RequireComponent(typeof(VisualController))]
public class PlayerMovePicker : AbstractMovePicker
{
    public EmotionType emotionChosen = EmotionType.Grief;
    protected bool isEmotionChosen = false;
    private GameObject spacePrompt;

    void Start()
    {
        if (userEmotionSystem == null)
        {
            userEmotionSystem = GetComponent<EmotionSystem>();
        }
        if (visualController == null)
        {
            visualController = GetComponent<VisualController>();
        }
        spacePrompt = GameObject.FindGameObjectWithTag("Space");
        spacePrompt.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
        if (!isAskingForPlayInput) { return; } //do nothing when out of turn
        Debug.Log("Player's turn and player input is requested");
        if (Input.GetKeyDown("up"))
        {
            emotionChosen = EmotionType.Grief;
            isEmotionChosen = true;
            visualController.updateEmotionWheelSelection(emotionChosen);
            spacePrompt.SetActive(true);
        }
        else if (Input.GetKeyDown("left"))
        {
            emotionChosen = EmotionType.Love;
            isEmotionChosen = true;
            visualController.updateEmotionWheelSelection(emotionChosen);
            spacePrompt.SetActive(true);
        }
        else if (Input.GetKeyDown("right"))
        {   
            emotionChosen = EmotionType.Wrath;
            isEmotionChosen = true;
            visualController.updateEmotionWheelSelection(emotionChosen);
            spacePrompt.SetActive(true);
        }
        else if (Input.GetKeyDown("down"))
        {
            emotionChosen = EmotionType.Mirth;
            isEmotionChosen = true;
            visualController.updateEmotionWheelSelection(emotionChosen);
            spacePrompt.SetActive(true);
        }
        if (Input.GetKeyDown("space") && isEmotionChosen)
        {
            userEmotionSystem.LoadNextMove(emotionChosen, MoveType.Damage);
            isAskingForPlayInput = false;
            isEmotionChosen = false;
            visualController.setEmotionWheelVisibility(false);
            spacePrompt.SetActive(false);
        }
    }

    public void MoveRequested()
    {
        visualController.setEmotionWheelVisibility(true);
        base.MoveRequested();
    }
}
