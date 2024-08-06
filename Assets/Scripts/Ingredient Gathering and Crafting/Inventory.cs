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
    private static Queue<EmotionType> emotionIngredientsCollected = new Queue<EmotionType>();
    private static Queue<MoveType> moveIngredientsCollected = new Queue<MoveType>();
    public static int maxMoveIngredients = 1;
    public static int maxEmotionIngredients = 1;
    public static float dropDistance = 5.0f;
    private static IBattleMove craftedSpell;
    public InventoryUIManager gatheringUI;

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



    // Start is called before the first frame update
    void Start()
    {
        initializeVariables();
        craftedSpell = new BasicMove(0, EmotionType.Null, MoveType.Null);
        //DontDestroyOnLoad(this.gameObject.transform.parent.gameObject);
        safeUpdateUI(); //don't do so if in a battle scene
    }

    private void safeUpdateUI()
    {
        if (gatheringUI != null)
        {
            gatheringUI.updateEmotionIngredientUI();
            gatheringUI.updateMoveIngredientUI();
        }
    }

    public void addMoveIngredient(MoveType moveType)
    {
        MoveType first;
        bool hasIngredient = moveIngredientsCollected.TryPeek(out first);
        if (hasIngredient && first == MoveType.Null) //if null for whatever reason, just drop it
        {
            moveIngredientsCollected.Dequeue();
            //Debug.Log("Removed Empty MoveIngredient");
        }
        //if the max size has been met, drop the first in the queue into the overworld a few feet in front of the player
        else if (hasIngredient && moveIngredientsCollected.Count >= maxMoveIngredients) 
        {
            dropInFrontOfPlayer(moveIngredientDropOptions[moveIngredientsCollected.Dequeue()]);
            //Debug.Log("Dropped movetype prefab in front of player");
        }
        moveIngredientsCollected.Enqueue(moveType);
        //Debug.Log("Collected moveType Ingredient: " + moveType.ToString());
        safeUpdateUI();
    }

    public void addEmotionIngredient(EmotionType emotionType)
    {
        EmotionType first;
        bool hasIngredient = emotionIngredientsCollected.TryPeek(out first);
        if (hasIngredient && first == EmotionType.Null) //if null for whatever reawson, just drop it
        {
            emotionIngredientsCollected.Dequeue();
            //Debug.Log("Removed Empty EmotionIngredient");
        }
        //if the max size has been met, drop the first in the queue into the overworld a few feet in front of the player
        else if (hasIngredient && emotionIngredientsCollected.Count >= maxEmotionIngredients)
        {
            //Debug.Log("emotionIngredientCount: " + emotionIngredientsCollected.Count);
            dropInFrontOfPlayer(emotionIngredientDropOptions[emotionIngredientsCollected.Dequeue()]);
            //Debug.Log("Dropped emotion prefab in front of player");
        }
        emotionIngredientsCollected.Enqueue(emotionType);
        //Debug.Log("Collected emotion ingredient: " + emotionType.ToString());
        safeUpdateUI();
    }

    private void dropInFrontOfPlayer(GameObject drop)
    {
        Vector3 dropLocation = transform.position + transform.forward * dropDistance + new Vector3(0, -.5f, 0);
        Instantiate(drop, dropLocation, transform.rotation);
    }

    public static EmotionType getEmotionType()
    {
        EmotionType emotion;
        bool hasIngredient = emotionIngredientsCollected.TryPeek(out emotion);
        return (hasIngredient) ? emotion : EmotionType.Null;
    }

    public static MoveType getMoveType()
    {
        return (moveIngredientsCollected.TryPeek(out MoveType moveType)) ? 
            moveType : MoveType.Null;
    }

    public static bool canCraft()
    {
        /*
        Debug.Log("Inventory: moveIngredient count: " + moveIngredientsCollected.Count);
        Debug.Log("Inventory: emotionIngredient count: " + emotionIngredientsCollected.Count);
        if (moveIngredientsCollected.Count > 0)
        {
            Debug.Log("Inventory: firstMoveIngredient = " + moveIngredientsCollected.Peek().ToString());
        }
        if (emotionIngredientsCollected.Count > 0)
        {
            Debug.Log("Inventory: firstEmotionIngredient = " + emotionIngredientsCollected.Peek().ToString());
        }
        */
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
    }
}
