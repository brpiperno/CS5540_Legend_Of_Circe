using EmotionTypeExtension;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Disply sprites for potions the player can create;
/// </summary>
public class PotionCraftingUIManager : MonoBehaviour
{
    [Header("Completed Potion Sprites")]
    public Sprite pharmakaPotionSprite;
    public Sprite paralysisPotionSprite;
    public Sprite loveEnhancementPotionSprite;
    public Sprite wrathEnhancementPotionSprite;
    public Sprite griefEnhancementPotionSprite;
    public Sprite mirthEnhancementPotionSprite;
    public Sprite griefShieldPotionSprite;
    public Sprite loveShieldPotionSprite;
    public Sprite wrathShieldPotionSprite;
    public Sprite mirthShieldPotionSprite;
    public Sprite griefTransformationPotionSprite;
    public Sprite loveTransformationPotionSprite;
    public Sprite wrathTransformationPotionSprite;
    public Sprite mirthTransformationPotionSprite;
    private Dictionary<MoveType, Dictionary<EmotionType, Sprite>> potionSprites;

    [Header("Crafting 2D UI elements")]
    public Image potionIcon;
    //public Image potionPanel;
    public Text craftingInstructionText;
    public Text potionNameText;

    public EmotionType potionEmotion = EmotionType.Null;
    public MoveType potionMoveType = MoveType.Null;

    // Start is called before the first frame update
    void Start()
    {
        potionSprites = InitializeDictionary();
    }

    public void UpdateUI(EmotionType emotion, MoveType move)
    {
        potionSprites = InitializeDictionary();
        Debug.Log("Updating UI with :" + emotion.ToString() + " / " + move.ToString());
        if (emotion != EmotionType.Null && move != MoveType.Null)
        {
            //potionPanel.gameObject.SetActive(true);
            //potionPanel.color = potionEmotion.GetColor();
            potionIcon.sprite = potionSprites[move][emotion];
            potionIcon.enabled = true;
            craftingInstructionText.text = "Press 'X' to craft!";
            potionNameText.text = getPotionName(emotion, move);
        } else
        {
            potionIcon.enabled = false;
            potionNameText.text = "";
            craftingInstructionText.text = "No potions available!";
        }
    }

    private Dictionary<MoveType, Dictionary<EmotionType, Sprite>> InitializeDictionary()
    {
        return new Dictionary<MoveType, Dictionary<EmotionType, Sprite>> {
            { MoveType.Pharmaka, new Dictionary<EmotionType, Sprite>{
                { EmotionType.Love, pharmakaPotionSprite},
                { EmotionType.Mirth, pharmakaPotionSprite},
                { EmotionType.Wrath, pharmakaPotionSprite},
                { EmotionType.Grief, pharmakaPotionSprite}
            } },
            { MoveType.Paralysis, new Dictionary<EmotionType, Sprite>{
                { EmotionType.Love, paralysisPotionSprite},
                { EmotionType.Mirth, paralysisPotionSprite},
                { EmotionType.Wrath, paralysisPotionSprite},
                { EmotionType.Grief, paralysisPotionSprite}
            } },
            { MoveType.Transformation, new Dictionary<EmotionType, Sprite>{
                { EmotionType.Love, loveTransformationPotionSprite },
                { EmotionType.Mirth, mirthTransformationPotionSprite },
                { EmotionType.Wrath, wrathTransformationPotionSprite},
                { EmotionType.Grief, griefTransformationPotionSprite }
            } },
            { MoveType.Enhancement, new Dictionary<EmotionType, Sprite> {
                { EmotionType.Love, loveEnhancementPotionSprite },
                { EmotionType.Mirth, mirthEnhancementPotionSprite },
                { EmotionType.Wrath, wrathEnhancementPotionSprite},
                { EmotionType.Grief, griefEnhancementPotionSprite }
            } },
            { MoveType.Shield, new Dictionary<EmotionType, Sprite>() {
                { EmotionType.Love, loveShieldPotionSprite },
                { EmotionType.Mirth, mirthShieldPotionSprite },
                { EmotionType.Wrath, wrathShieldPotionSprite},
                { EmotionType.Grief, griefShieldPotionSprite }
            } }
        };
    }

    public static string getPotionName(EmotionType emotion, MoveType moveType)
    {
        switch (moveType)
        {
            case MoveType.Paralysis:
                return "Potion of Paralysis";
            case MoveType.Pharmaka:
                return "Pharmaka";
            case MoveType.Shield:
                return "Potion of Defense (" + emotion.ToString() + ")";
            case MoveType.Enhancement:
                return "Potion of Healing (" + emotion.ToString() + ")";
            case MoveType.Transformation:
                return "Potion of Transformation (" + emotion.ToString() + ")";
            case MoveType.Null:
                return "";
            default:
                throw new System.Exception("Unaccounted potion type: " + moveType.ToString() + " - " + emotion.ToString());
        }
    }

    public static string getPotionDescription(EmotionType emotion, MoveType moveType)
    {
        switch (moveType)
        {
            case MoveType.Paralysis:
                return "Stun your opponent's next turn!";
            case MoveType.Pharmaka:
                return "Transcend the laws of divinity!";
            case MoveType.Shield:
                return "Increase your defense against words of " + emotion.ToString() + ".";
            case MoveType.Enhancement:
                return "Restore your " + emotion.ToString() + " level.";
            case MoveType.Transformation:
                return "Set your opponent's next turn to type: " + emotion.ToString() + ".";
            case MoveType.Null:
                return "";
            default:
                throw new System.Exception("Unaccounted potion type: " + moveType.ToString() + " - " + emotion.ToString());
        }
    }

}
