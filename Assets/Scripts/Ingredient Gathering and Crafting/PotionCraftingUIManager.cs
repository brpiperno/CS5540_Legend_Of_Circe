using EmotionTypeExtension;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Disply sprites for potions the player can create;
/// </summary>
public class PotionCraftingUIManager : MonoBehaviour
{
    [Header("Completed Potion Sprites")]
    //Sprites for finalized potions
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
    public Image potionPanel;
    public Text potionText;

    public EmotionType potionEmotion = EmotionType.Null;
    public MoveType potionMoveType = MoveType.Null;

    // Start is called before the first frame update
    void Start()
    {
        potionSprites = InitializeDictionary();
        foreach (MoveType m in potionSprites.Keys)
        {
            /*
             * Debug.Log("PotionSprites: MoveType: " + m.ToString());
            foreach (EmotionType e in potionSprites[m].Keys)
            {
                Debug.Log("PotionSprites[" + m.ToString() + "][" + e.ToString() + "] = " + potionSprites[m][e].ToString());
            }
            */
        }
    }

    public void UpdateUI(EmotionType emotion, MoveType move)
    {
        potionSprites = InitializeDictionary();
        Debug.Log("Updating UI with :" + emotion.ToString() + " / " + move.ToString());
        if (emotion != EmotionType.Null && move != MoveType.Null)
        {
            potionPanel.gameObject.SetActive(true);
            potionPanel.color = potionEmotion.GetColor();
            potionIcon.sprite = potionSprites[move][emotion];
            potionText.text = "Press 'X' to craft!";
        } else
        {
            potionPanel.gameObject.SetActive(false);
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
}
