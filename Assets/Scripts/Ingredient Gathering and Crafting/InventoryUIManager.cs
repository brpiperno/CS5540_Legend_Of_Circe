using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using EmotionTypeExtension;

public class InventoryUIManager : MonoBehaviour
{
    private MoveType moveType = MoveType.Null;
    private EmotionType emotionType = EmotionType.Null;

    [Header("Gathering 2D UI controls")]
    public Image emotionIngredientPanel;
    public Image emotionIngredientIcon;
    public Image moveTypeIngredientIcon;
    public Text emotionIngredientText;
    public Text moveIngredientText;

    [Header("Emotion Ingredient Sprites")]
    public Sprite mirthIcon;
    public Sprite loveIcon;
    public Sprite wrathIcon;
    public Sprite griefIcon;
    private Dictionary<EmotionType, Sprite> emotionSprites;

    [Header("MoveType Ingreidnet Sprites")]
    public Sprite enhancementIcon;
    public Sprite shieldIcon;
    public Sprite paralysisIcon;
    public Sprite transformationIcon;
    public Sprite pharmakaIcon;
    private Dictionary<MoveType, Sprite> moveSprites;


    // Start is called before the first frame update
    void Start()
    {
        initializeVariables();
        updateEmotionIngredientUI();
        updateMoveIngredientUI();
    }
    public void updateMoveIngredientUI()
    {       
        moveType = Inventory.getMoveType();
        if (moveType != MoveType.Null)
        {
            moveTypeIngredientIcon.enabled = true;
            moveTypeIngredientIcon.sprite = moveSprites[moveType];
            moveIngredientText.text = "Extract of " + moveType.ToString();
        }
        else
        {
            moveTypeIngredientIcon.enabled = false;
            moveIngredientText.text = "Alchemical Base: None";
        }
    }

    public void updateEmotionIngredientUI()
    {
        emotionType = Inventory.getEmotionType();
        if (Inventory.getEmotionType() != EmotionType.Null)
        {
            emotionIngredientPanel.color = emotionType.GetColor();
            emotionIngredientIcon.sprite = emotionSprites[emotionType];
            emotionIngredientIcon.enabled = true;
            emotionIngredientText.text = "Extract of " + emotionType.ToString();
        }
        else
        {
            emotionIngredientIcon.enabled = false;
            emotionIngredientText.text = "Emotional Extract: None";
        }
    }

    private void initializeVariables()
    {
        //Sprite for collected emotion ingredient
        emotionSprites = new Dictionary<EmotionType, Sprite>() {
            { EmotionType.Grief, griefIcon },
            { EmotionType.Mirth, mirthIcon },
            { EmotionType.Wrath, wrathIcon },
            { EmotionType.Love, loveIcon }
        };

        //Sprite for collected movetype ingredient
        moveSprites = new Dictionary<MoveType, Sprite>() {
            { MoveType.Enhancement, enhancementIcon},
            { MoveType.Shield, shieldIcon },
            { MoveType.Paralysis, paralysisIcon },
            { MoveType.Transformation, transformationIcon },
            { MoveType.Pharmaka, pharmakaIcon }
        };
    }
}