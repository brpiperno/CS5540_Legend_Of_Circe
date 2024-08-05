using UnityEngine;
using EmotionTypeExtension;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(VisualController))]
public class PlayerMovePicker : AbstractMovePicker
{
    public EmotionType emotionChosen = EmotionType.Grief;
    protected bool isEmotionChosen = false;
    BattleManager battleManager;
    VisualController visualController;


    protected new void Start()
    {
        base.Start();
        if (visualController == null)
        {
            visualController = GetComponent<VisualController>();
        }
        battleManager = GameObject.FindGameObjectWithTag("BattleManager").GetComponent<BattleManager>();
        battleManager.spacePrompt.SetActive(false);
        //Because inventory items are static, we dont need a specific instance
    }

    // Update is called once per frame
    void Update()
    {
        
        if (!isAskingForPlayInput) { return; } //do nothing when out of turn
        //Debug.Log("Player's turn and player input is requested");
        if (Input.GetKeyDown("up"))
        {
            emotionChosen = EmotionType.Grief;
            isEmotionChosen = true;
            visualController.updateEmotionWheelSelection(emotionChosen);
            battleManager.spacePrompt.SetActive(true);
        }
        else if (Input.GetKeyDown("left"))
        {
            emotionChosen = EmotionType.Love;
            isEmotionChosen = true;
            visualController.updateEmotionWheelSelection(emotionChosen);
            battleManager.spacePrompt.SetActive(true);
        }
        else if (Input.GetKeyDown("right"))
        {   
            emotionChosen = EmotionType.Wrath;
            isEmotionChosen = true;
            visualController.updateEmotionWheelSelection(emotionChosen);
            battleManager.spacePrompt.SetActive(true);
        }
        else if (Input.GetKeyDown("down"))
        {
            emotionChosen = EmotionType.Mirth;
            isEmotionChosen = true;
            visualController.updateEmotionWheelSelection(emotionChosen);
            battleManager.spacePrompt.SetActive(true);
        }
        if (Input.GetKeyDown("space") && isEmotionChosen)
        {
            userEmotionSystem.LoadNextMove(emotionChosen, MoveType.Damage);
            isAskingForPlayInput = false;
            isEmotionChosen = false;
            visualController.setEmotionWheelVisibility(false);
            battleManager.spacePrompt.SetActive(false);
        }
    }

    public new void MoveRequested()
    {
        Debug.Log("PlayerMovePicker MoveRequested reached");
        visualController.setEmotionWheelVisibility(true);
        base.MoveRequested();
    }
}
