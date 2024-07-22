using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

//Assign this script to Circe and all NPCs

public class EmotionSystem : MonoBehaviour, IEmotion
{
    public GameObject playerAttackAnimationObject;
    public GameObject enemyAttackAnimationObject;
    public IVisualController visualController;
    private Dictionary<EmotionType, float> emotionValues;

    private Dictionary<EmotionType, int> defenseModifiers;

    void Start()
    {
        //add each emotion type and set the initial values to 100;
        emotionValues = new Dictionary<EmotionType, float> {
            {EmotionType.Wrath, 100},
            {EmotionType.Love, 100},
            {EmotionType.Grief, 100},
            {EmotionType.Mirth, 100}
        };
        //add each emotion type and set the initial values to 1;
        defenseModifiers = new Dictionary<EmotionType, int> {
            {EmotionType.Wrath, 1},
            {EmotionType.Love, 1},
            {EmotionType.Grief, 1},
            {EmotionType.Mirth, 1}
        };
    }

    public float GetEmotion(EmotionType type)
    {
        return emotionValues[type];
    }

    // Method does not use the move variable so far
    public void PlayMove(IBattleMove move)
    {
        // Circe's move animation
        if (gameObject.tag == "Player")
        {
            GameObject newAttack = Instantiate(playerAttackAnimationObject, new Vector3(3, 3, -3), Quaternion.Euler(new Vector3(90, 45, 0)));
            Destroy(newAttack, 1);
        }
        else if (gameObject.tag == "Enemy")
        { // Opponent's move animation
            GameObject newAttack = Instantiate(enemyAttackAnimationObject, new Vector3(3, 3, -3), Quaternion.Euler(new Vector3(90, 45, 0)));
            Destroy(newAttack, 1);
        }
        else
        {
            throw new ArgumentException("Tag of GameObject containing this EmotionSystem script is neither Player nor Enemy.");
        }
    }

    /*
        Performs the move "attacker" against the current object. Needs to take in the move chosen by this
        object as well to calculate effectiveness.
    */
    public void AcceptMove(IBattleMove attacker, IBattleMove receiver)
    {
        //float damage = BattleManager.basePowerForMoves * 
        //    BattleManager.getTypeChartMultiplier(attacker.GetEmotionType(), 
        //    receiver.GetEmotionType()) * defenseModifiers[attacker.GetEmotionType()];

        float damage = (attacker as EmotionMove).getEffectStrength() *
            BattleManager.getTypeChartMultiplier(attacker.GetEmotionType(),
            receiver.GetEmotionType()) * defenseModifiers[attacker.GetEmotionType()];

        float newValue = ShiftEmotions(attacker.GetEmotionType(), damage);

        if (gameObject.tag == "Player")
        {
            Debug.Log("Circe's action has a " + BattleManager.getTypeChartMultiplier(attacker.GetEmotionType(),
                receiver.GetEmotionType()) + "x multiplier");
            visualController.updateEmotionBarUI(true, attacker.GetEmotionType(), newValue);
            Debug.Log("Opponent spoke with " + attacker.GetEmotionType() + ", causing " + damage + " damage to Circe!");
        }
        else if (gameObject.tag == "Enemy")
        {
            Debug.Log("The opponent's action has a " +
                BattleManager.getTypeChartMultiplier(attacker.GetEmotionType(),
                receiver.GetEmotionType()) + "x multiplier");
            visualController.updateEmotionBarUI(false, attacker.GetEmotionType(), newValue);
            Debug.Log("Circe spoke with " + attacker.GetEmotionType() + ", causing " + damage + " damage to the opponent!");
        }
    }

    // Returns the new value of the emotion bar that was modified
    public float ShiftEmotions(EmotionType emotion, float value)
    {
        //get the current value of the chose Emotion type, and update it accordingly,
        //based on the min,max, current value, and defense modifier
        float newValue = GetEmotion(emotion) - value;
        if (newValue < 0)
        {
            newValue = 0;
        }
        emotionValues[emotion] = newValue;
        return newValue;
    }

}
