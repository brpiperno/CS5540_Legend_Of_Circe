using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EmotionTypeExtension;
using System;
using UnityEngine.UI;
using System.Runtime.CompilerServices;

/// <summary>
/// This behavior script is to be attached to the player game object and allows them to collect and manage spell crafting ingredients. This also controls any UI updates
/// </summary>
public class Inventory : MonoBehaviour
{
    public int spellStrength = 20;
    private Queue<EmotionType> emotionIngredientsCollected;
    private Queue<MoveType> moveIngredientsCollected;
    public int maxMoveIngredients = 1;
    public int maxEmotionIngredients = 1;
    public float dropDistance = 1.0f;
    private IBattleMove craftedSpell;

    //When the inventory is maxed out, drop the prefab at the front of the queue of the right type
    public GameObject mirthPrefabDrop;
    public GameObject lovePrefabDrop;
    public GameObject wrathPrefabDrop;
    public GameObject griefPrefabDrop;
    private Dictionary<EmotionType, GameObject> emotionIngredientDropOptions;

    public GameObject EnhancementPrefabDrop;
    public GameObject ShieldPrefabDrop;
    public GameObject ParalysisPrefabDrop;
    public GameObject PharmakaPrefabDrop;
    public GameObject TransformationPrefabDrop;
    private Dictionary<MoveType, GameObject> moveIngredientDropOptions;

    //UI variables
    public Text craftSpellText;
    public Image emotionIngredientPanel;
    public Image moveTypeIngredientPanel;
    public Image emotionIngredientIcon;
    public Image moveTypeIngredientIcon;
    public Image potionIcon;
    public Image potionPanel;

    //Sprites for collected emotion ingredients
    public Sprite mirthIcon;
    public Sprite loveIcon;
    public Sprite wrathIcon;
    public Sprite griefIcon;
    private Dictionary<EmotionType, Sprite> emotionSprites;

    //Sprites for collected movetype ingredients
    public Sprite enhancementIcon;
    public Sprite shieldIcon;
    public Sprite paralysisIcon;
    public Sprite transformationIcon;
    public Sprite pharmakaIcon;
    private Dictionary<MoveType, Sprite> moveSprites;


    //Sprites for finalized potions
    public Sprite craftedPotionIcon;
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


    // Start is called before the first frame update
    void Start()
    {
        emotionIngredientsCollected = new Queue<EmotionType>();
        moveIngredientsCollected = new Queue<MoveType>();

        emotionIngredientDropOptions = new Dictionary<EmotionType, GameObject>
        {
            { EmotionType.Mirth, mirthPrefabDrop },
            { EmotionType.Love, lovePrefabDrop },
            { EmotionType.Wrath, wrathPrefabDrop },
            { EmotionType.Grief, griefPrefabDrop }
        };

        moveIngredientDropOptions = new Dictionary<MoveType, GameObject>() {
            { MoveType.Enhancement, EnhancementPrefabDrop },
            { MoveType.Shield, ShieldPrefabDrop },
            { MoveType.Paralysis, ParalysisPrefabDrop },
            { MoveType.Pharmaka, PharmakaPrefabDrop },
            { MoveType.Transformation, TransformationPrefabDrop }
        };

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

        craftedSpell = new BasicMove(0, EmotionType.Null, MoveType.Null);
        //Assign all possible potion sprites according to MoveType and EmotionType
        potionSprites = new Dictionary<MoveType, Dictionary<EmotionType, Sprite>> {
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

    public void addMoveIngredient(MoveType moveType)
    {
        MoveType first;
        bool hasIngredient = moveIngredientsCollected.TryPeek(out first);
        if (hasIngredient && first == MoveType.Null) //if null for whatever reason, just drop it
        {
            moveIngredientsCollected.Dequeue();
        }
        else if (hasIngredient && moveIngredientsCollected.Count > maxMoveIngredients) //if the max size has been met, drop the first in the queue into the overworld a few feet in front of the player
        {
            dropInFrontOfPlayer(moveIngredientDropOptions[moveIngredientsCollected.Dequeue()]);
        }
        moveIngredientsCollected.Enqueue(moveType);
        updateMoveIngredientUI();
    }

    public void addEmotionIngredient(EmotionType emotionType)
    {
        EmotionType first;
        bool hasIngredient = emotionIngredientsCollected.TryPeek(out first);
        if (hasIngredient && first == EmotionType.Null) //if null for whatever reawson, just drop it
        {
            emotionIngredientsCollected.Dequeue();
        }
        else if (hasIngredient && emotionIngredientsCollected.Count > maxEmotionIngredients) //if the max size has been met, drop the first in the queue into the overworld a few feet in front of the player
        {
            dropInFrontOfPlayer(emotionIngredientDropOptions[emotionIngredientsCollected.Dequeue()]);
        }
        emotionIngredientsCollected.Enqueue(emotionType);
        updateEmotionIngredientUI();
    }

    void dropInFrontOfPlayer(GameObject drop)
    {
        Vector3 dropLocation = transform.position + transform.forward * dropDistance;
        Instantiate(drop, dropLocation, transform.rotation);
    }

    public bool canCraft()
    {
        EmotionType emotionType;
        MoveType moveType;
        return (emotionIngredientsCollected.TryPeek(out  emotionType) 
            && emotionType != EmotionType.Null
            && moveIngredientsCollected.TryPeek(out moveType)
            && moveType != MoveType.Null);
    }

    public void CraftSpell()
    {
        if (!canCraft())
        {
            throw new System.Exception("Tried Crafting a Spell when unable to do so!");
        }
        craftedSpell = new BasicMove(spellStrength, emotionIngredientsCollected.Dequeue(), moveIngredientsCollected.Dequeue());
    }

    public IBattleMove GetSpell()
    {
        if (craftedSpell.GetMoveType() == MoveType.Null || craftedSpell.GetEmotionType() == EmotionType.Null)
        {
            throw new InvalidOperationException("Tried to retrieve Inventory's battle move when MoveType or EmotionType was null");
        }
        return craftedSpell;
    }

    public void UseSpell()
    {
        craftedSpell = new BasicMove(0, EmotionType.Null, MoveType.Null);
    }

    public bool hasSpell()
    {
        return craftedSpell.GetMoveType() == MoveType.Null || craftedSpell.GetEmotionType() == EmotionType.Null;
    }

    private void updateUI()
    {
        updateEmotionIngredientUI();
        updateMoveIngredientUI();
        //Handle Crafting Operation Text
        craftSpellText.enabled = canCraft();
        //Handle Potion
        if (craftedSpell.GetEmotionType() == EmotionType.Null && craftedSpell.GetMoveType() == MoveType.Null)
        {
            potionIcon.enabled = false;
        }
        else
        {
            potionIcon.enabled = true;
            potionIcon.color = craftedSpell.GetEmotionType().GetColor();
            potionIcon.sprite = potionSprites[craftedSpell.GetMoveType()][craftedSpell.GetEmotionType()];
        }
    }


    private void updateMoveIngredientUI()
    {
        if (moveIngredientsCollected.TryPeek(out MoveType moveType))
        {
            moveTypeIngredientIcon.enabled = true;
            moveTypeIngredientIcon.sprite = moveSprites[moveType];
        }
        else
        {
            moveTypeIngredientIcon.enabled = false;
        }
    }

    private void updateEmotionIngredientUI()
    {
        if (emotionIngredientsCollected.TryPeek(out EmotionType emotion))
        {
            emotionIngredientPanel.color = emotion.GetColor();
            emotionIngredientIcon.sprite = emotionSprites[emotion];
            emotionIngredientIcon.enabled = true;
        }
        else
        {
            emotionIngredientIcon.enabled = false;
        }
    }
}
