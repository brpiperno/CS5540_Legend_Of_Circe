using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using System;

//Assign this script to Circe and all NPCs

public class EmotionSystem : MonoBehaviour, IEmotion
{
    public GameObject playerAttackAnimationObject;
    public GameObject enemyAttackAnimationObject;
    public IVisualController visualController;
    private Dictionary<EmotionType, float> emotionValues;  
    
    private Dictionary<EmotionType, int> defenseModifiers;
    
    void Start() {
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

    public float GetEmotion(EmotionType type) {
        return emotionValues[type];
    }

    // Method does not use the move variable so far
    public void PlayMove(IBattleMove move) {
        GameObject newAttack;
        // Circe's move animation
        if (gameObject.tag == "Player") {
            //newAttack = Instantiate(playerAttackAnimationObject, new Vector3(1.26f, 1.42f, -3.07f), Quaternion.Euler(new Vector3(90, 33, 0)));
            newAttack = Instantiate(playerAttackAnimationObject, new Vector3(0.5f, 1.5f, -6f), Quaternion.Euler(new Vector3(-90, 0, 0)));
        } else if (gameObject.tag == "Enemy") { // Opponent's move animation
            //newAttack = Instantiate(enemyAttackAnimationObject, new Vector3(0.35f, 1.63f, -2.04f), Quaternion.Euler(new Vector3(90, 33, 0)));
            newAttack = Instantiate(enemyAttackAnimationObject, new Vector3(2.7f, 1.5f, 0f), Quaternion.Euler(new Vector3(0, 0, 0)));
            Invoke("SetAskInput", 2);
        } else {
            throw new ArgumentException("Tag of GameObject containing this EmotionSystem script is neither Player nor Enemy.");
        }
        if (move.GetMoveType() == MoveType.Damage) {
            ParticleSystem.MainModule ps = newAttack.GetComponent<ParticleSystem>().main;
            switch((move as EmotionMove).GetEmotionType()) {
                case EmotionType.Grief:
                    //newAttack.GetComponent<Renderer>().material.color = new Color(0.2941177f, 0, 1f);
                    ps.startColor = new Color(0.2941177f, 0, 1f);
                    break;
                case EmotionType.Love:
                    //newAttack.GetComponent<Renderer>().material.color = new Color(0.9411765f, 0.3333333f, 0.8207547f);
                    ps.startColor = new Color(0.9411765f, 0.3333333f, 0.8207547f);
                    break;
                case EmotionType.Wrath:
                    //newAttack.GetComponent<Renderer>().material.color = new Color(1f, 0.2352941f, 0f);
                    ps.startColor = new Color(1f, 0.2352941f, 0f);
                    break;
                case EmotionType.Mirth:
                    //newAttack.GetComponent<Renderer>().material.color = new Color(0.3921569f, 0.8823529f, 0.2941177f);
                    ps.startColor = new Color(0.3921569f, 0.8823529f, 0.2941177f);
                    break;
                default:
                    break;
            }
        }
    }

    /*
        Performs the move "attacker" against the current object. Needs to take in the move chosen by this
        object as well to calculate effectiveness.
    */
    public void AcceptEmotionMove(EmotionMove attacker, EmotionMove receiver) {
        //float damage = BattleManager.basePowerForMoves * 
        //    BattleManager.getTypeChartMultiplier(attacker.GetEmotionType(), 
        //    receiver.GetEmotionType()) * defenseModifiers[attacker.GetEmotionType()];

        float damage = (attacker as EmotionMove).getEffectStrength() * 
            attacker.GetEmotionType().getEffectivenessAgainst(receiver.GetEmotionType()) *
            defenseModifiers[attacker.GetEmotionType()];

        float newValue = ShiftEmotions(attacker.GetEmotionType(), damage);

        if (gameObject.tag == "Player") {
            Debug.Log("Circe's action has a " + attacker.GetEmotionType().getEffectivenessAgainst(receiver.GetEmotionType()) +
                "x multiplier");
            //visualController.updateEmotionBarUI(true, attacker.GetEmotionType(), newValue);
            Debug.Log("Opponent spoke with " + attacker.GetEmotionType() + ", causing " + damage + " damage to Circe!");
        } else if (gameObject.tag == "Enemy") {
            Debug.Log("The opponent's action has a " + attacker.GetEmotionType().getEffectivenessAgainst(receiver.GetEmotionType()) +
                "x multiplier");
            //visualController.updateEmotionBarUI(false, attacker.GetEmotionType(), newValue);
            Debug.Log("Circe spoke with " + attacker.GetEmotionType() + ", causing " + damage + " damage to the opponent!");
        }
    }

    // Returns the new value of the emotion bar that was modified
    public float ShiftEmotions(EmotionType emotion, float value)
    {
        //get the current value of the chose Emotion type, and update it accordingly,
        //based on the min,max, current value, and defense modifier
        float newValue = GetEmotion(emotion) - value;
        if (newValue < 0) {
            newValue = 0;
        }
        emotionValues[emotion] = newValue;
        Debug.Log("new value for " + emotion + " is : " + newValue + "for " + name);
        return newValue;
    }

    // Sets it to true only (Invoke only works on functions with no parameters)
    public void SetAskInput() {
        BattleManager.isAskingForPlayerInput = true;
    }
}
