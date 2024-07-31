using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EmotionTypeExtension;
using System;

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

    Dictionary<EmotionType, GameObject> emotionIngredientDropOptions;

    public GameObject EnhancementPrefabDrop;
    public GameObject ShieldPrefabDrop;
    public GameObject ParalysisPrefabDrop;
    public GameObject PharmakaPrefabDrop;
    public GameObject TransformationPrefabDrop;

    Dictionary<MoveType, GameObject> moveIngredientDropOptions;

    // Start is called before the first frame update
    void Start()
    {
        emotionIngredientsCollected = new Queue<EmotionType>();
        moveIngredientsCollected = new Queue<MoveType>();

        emotionIngredientDropOptions = new Dictionary<EmotionType, GameObject>();
        emotionIngredientDropOptions.Add(EmotionType.Mirth, mirthPrefabDrop);
        emotionIngredientDropOptions.Add(EmotionType.Love, lovePrefabDrop);
        emotionIngredientDropOptions.Add(EmotionType.Wrath, wrathPrefabDrop);
        emotionIngredientDropOptions.Add(EmotionType.Grief, griefPrefabDrop);

        moveIngredientDropOptions = new Dictionary<MoveType, GameObject>();
        moveIngredientDropOptions.Add(MoveType.Enhancement, EnhancementPrefabDrop);
        moveIngredientDropOptions.Add(MoveType.Shield, ShieldPrefabDrop);
        moveIngredientDropOptions.Add(MoveType.Paralysis, ParalysisPrefabDrop);
        moveIngredientDropOptions.Add(MoveType.Pharmaka, PharmakaPrefabDrop);
        moveIngredientDropOptions.Add(MoveType.Transformation, TransformationPrefabDrop);

        craftedSpell = new BasicMove(0, EmotionType.Null, MoveType.Null);
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

    public bool hasSpell()
    {
        return craftedSpell.GetMoveType() == MoveType.Null || craftedSpell.GetEmotionType() == EmotionType.Null;
}
}
