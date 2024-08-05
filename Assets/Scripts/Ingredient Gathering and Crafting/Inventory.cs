using System.Collections.Generic;
using UnityEngine;
using EmotionTypeExtension;
using System;
using UnityEngine.UI;

/// <summary>
/// This behavior script is to be attached to the player game object and allows them to collect and manage spell crafting ingredients. This also controls any UI updates
/// </summary>
public class Inventory : MonoBehaviour
{
    [Header("Static Inventory Management")]
    public static int spellStrength = 20;
    private static Queue<EmotionType> emotionIngredientsCollected;
    private static Queue<MoveType> moveIngredientsCollected;
    public static int maxMoveIngredients = 1;
    public static int maxEmotionIngredients = 1;
    public static float dropDistance = 5.0f;
    private static IBattleMove craftedSpell;

    //When the inventory is maxed out, drop the prefab at the front of the queue of the right type
    [Header("EmotionIngrdient Drop Prefabs")]
    public GameObject mirthPrefabDrop;
    public GameObject lovePrefabDrop;
    public GameObject wrathPrefabDrop;
    public GameObject griefPrefabDrop;
    private Dictionary<EmotionType, GameObject> emotionIngredientDropOptions;

    [Header("MoveTypeIngrdient Drop Prefabs")]
    public GameObject EnhancementPrefabDrop;
    public GameObject ShieldPrefabDrop;
    public GameObject ParalysisPrefabDrop;
    public GameObject PharmakaPrefabDrop;
    public GameObject TransformationPrefabDrop;
    private Dictionary<MoveType, GameObject> moveIngredientDropOptions;

    [Header("Crafting 2D UI controls")]
    //UI variables
    public Text craftSpellText;
    public Image emotionIngredientPanel;
    public Image moveTypeIngredientPanel;
    public Image emotionIngredientIcon;
    public Image moveTypeIngredientIcon;
    public Image potionIcon;
    public Image potionPanel;
    public Text emotionIngredientText;
    public Text moveIngredientText;

    [Header("Emotion Ingredient Sprites")]
    //Sprites for collected emotion ingredients
    public Sprite mirthIcon;
    public Sprite loveIcon;
    public Sprite wrathIcon;
    public Sprite griefIcon;
    private Dictionary<EmotionType, Sprite> emotionSprites;

    [Header("MoveType Ingreidnet Sprites")]
    //Sprites for collected movetype ingredients
    public Sprite enhancementIcon;
    public Sprite shieldIcon;
    public Sprite paralysisIcon;
    public Sprite transformationIcon;
    public Sprite pharmakaIcon;
    private Dictionary<MoveType, Sprite> moveSprites;

    [Header("Completed Potion Sprites")]
    //Sprites for finalized potions
    public static Sprite pharmakaPotionSprite;
    public static Sprite paralysisPotionSprite;
    public static Sprite loveEnhancementPotionSprite;
    public static Sprite wrathEnhancementPotionSprite;
    public static Sprite griefEnhancementPotionSprite;
    public static Sprite mirthEnhancementPotionSprite;
    public static Sprite griefShieldPotionSprite;
    public static Sprite loveShieldPotionSprite;
    public static Sprite wrathShieldPotionSprite;
    public static Sprite mirthShieldPotionSprite;
    public static Sprite griefTransformationPotionSprite;
    public static Sprite loveTransformationPotionSprite;
    public static Sprite wrathTransformationPotionSprite;
    public static Sprite mirthTransformationPotionSprite;
    private Dictionary<MoveType, Dictionary<EmotionType, Sprite>> potionSprites;


    // Start is called before the first frame update
    void Start()
    {
        emotionIngredientsCollected = new Queue<EmotionType>();
        moveIngredientsCollected = new Queue<MoveType>();
        initializeVariables();
        craftedSpell = new BasicMove(0, EmotionType.Null, MoveType.Null);
        updateUI();
        DontDestroyOnLoad(this.gameObject);
    }


    public void addMoveIngredient(MoveType moveType)
    {
        MoveType first;
        bool hasIngredient = moveIngredientsCollected.TryPeek(out first);
        if (hasIngredient && first == MoveType.Null) //if null for whatever reason, just drop it
        {
            moveIngredientsCollected.Dequeue();
            Debug.Log("Removed Empty MoveIngredient");
        }
        //if the max size has been met, drop the first in the queue into the overworld a few feet in front of the player
        else if (hasIngredient && moveIngredientsCollected.Count >= maxMoveIngredients) 
        {
            dropInFrontOfPlayer(moveIngredientDropOptions[moveIngredientsCollected.Dequeue()]);
            Debug.Log("Dropped movetype prefab in front of player");
        }
        moveIngredientsCollected.Enqueue(moveType);
        Debug.Log("Collected moveType Ingredient: " + moveType.ToString());
        updateUI();
    }

    public void addEmotionIngredient(EmotionType emotionType)
    {
        EmotionType first;
        bool hasIngredient = emotionIngredientsCollected.TryPeek(out first);
        if (hasIngredient && first == EmotionType.Null) //if null for whatever reawson, just drop it
        {
            emotionIngredientsCollected.Dequeue();
            Debug.Log("Removed Empty EmotionIngredient");
        }
        //if the max size has been met, drop the first in the queue into the overworld a few feet in front of the player
        else if (hasIngredient && emotionIngredientsCollected.Count >= maxEmotionIngredients)
        {
            Debug.Log("emotionIngredientCount: " + emotionIngredientsCollected.Count);
            dropInFrontOfPlayer(emotionIngredientDropOptions[emotionIngredientsCollected.Dequeue()]);
            Debug.Log("Dropped emotion prefab in front of player");
        }
        emotionIngredientsCollected.Enqueue(emotionType);
        Debug.Log("Collected emotion ingredient: " + emotionType.ToString());
        updateUI();
    }

    void dropInFrontOfPlayer(GameObject drop)
    {
        Vector3 dropLocation = transform.position + transform.forward * dropDistance + new Vector3(0, -.5f, 0);
        Instantiate(drop, dropLocation, transform.rotation);
    }

    public static bool canCraft()
    {
        return emotionIngredientsCollected.TryPeek(out EmotionType emotionType) 
            && emotionType != EmotionType.Null
            && moveIngredientsCollected.TryPeek(out MoveType moveType)
            && moveType != MoveType.Null;
    }

    public static void CraftSpell()
    {
        if (!canCraft())
        {
            throw new Exception("Tried Crafting a Spell when unable to do so!");
        }
        craftedSpell = new BasicMove(spellStrength, emotionIngredientsCollected.Dequeue(), moveIngredientsCollected.Dequeue());
    }

    public static IBattleMove GetSpell()
    {
        if (craftedSpell.GetMoveType() == MoveType.Null || craftedSpell.GetEmotionType() == EmotionType.Null)
        {
            throw new InvalidOperationException("Tried to retrieve Inventory's battle move when MoveType or EmotionType was null");
        }
        return craftedSpell;
    }

    public static void UseSpell()
    {
        craftedSpell = new BasicMove(0, EmotionType.Null, MoveType.Null);
    }

    public static bool hasSpell()
    {
        return craftedSpell.GetMoveType() == MoveType.Null || craftedSpell.GetEmotionType() == EmotionType.Null;
    }

    private void updateUI()
    {
        updateEmotionIngredientUI();
        updateMoveIngredientUI();

        /* Skip the code below for now
        craftSpellText.enabled = canCraft();
        if (craftedSpell != null || craftedSpell.GetEmotionType() == EmotionType.Null || craftedSpell.GetMoveType() == MoveType.Null)
        {
            potionIcon.enabled = false;
            potionPanel.enabled = false;
        }
        else
        {
            potionPanel.enabled = true;
            potionIcon.enabled = true;
            potionIcon.color = craftedSpell.GetEmotionType().GetColor();
            potionIcon.sprite = potionSprites[craftedSpell.GetMoveType()][craftedSpell.GetEmotionType()];
        }
        */
    }


    private void updateMoveIngredientUI()
    {
        if (moveIngredientsCollected.TryPeek(out MoveType moveType) && moveType != MoveType.Null)
        {
            moveTypeIngredientIcon.enabled = true;
            moveTypeIngredientIcon.sprite = moveSprites[moveType];
            moveIngredientText.text = "Extract of\n" + moveType.ToString();
        }
        else
        {
            moveTypeIngredientIcon.enabled = false;
            moveIngredientText.text = "Alchemical Base:\nNone";
        }
    }

    private void updateEmotionIngredientUI()
    {
        if (emotionIngredientsCollected.TryPeek(out EmotionType emotion) && emotion != EmotionType.Null)
        {
            emotionIngredientPanel.color = emotion.GetColor();
            emotionIngredientIcon.sprite = emotionSprites[emotion];
            emotionIngredientIcon.enabled = true;
            emotionIngredientText.text = "Extract of\n" + emotion.ToString();
        }
        else
        {
            emotionIngredientIcon.enabled = false;
            emotionIngredientText.text = "Emotional Extract:\nNone";
        }
    }

    private void initializeVariables()
    {
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
}
