using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

//Assign this script to Circe and all NPCs

public class EmotionSystem : MonoBehaviour, EmotionInterface
{
    public GameObject playerAttackAnimationObject;
    public GameObject enemyAttackAnimationObject;
    public IVisualController visualController;
    private Dictionary<EmotionType, int> emotionValues;  
    
    private Dictionary<EmotionType, int> defenseModifiers;
    
    void Start() {
        //add each emotion type and set the initial values to 100;
        emotionValues = new Dictionary<EmotionType, int> {
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

    public int GetEmotion(EmotionType type) {
        return emotionValues[type];
    }

    public void PlayMove(IBattleMove move) {
        // Circe's move animation
        if (gameObject.tag == "Player") {
            GameObject newAttack = Instantiate(playerAttackAnimationObject, new Vector3(3, 3, -3), Quaternion.Euler(new Vector3(90, 45, 0)));
            Destroy(newAttack, 1);
        }
        // Opponent's move animation
        if (gameObject.tag == "Enemy") {
            GameObject newAttack = Instantiate(enemyAttackAnimationObject, new Vector3(3, 3, -3), Quaternion.Euler(new Vector3(90, 45, 0)));
            Destroy(newAttack, 1);
        }
    }

    /*
        Performs the move "attacker" against the current object. Needs to take in the move chosen by this
        object as well to calculate effectiveness.
    */
    public void AcceptMove(IBattleMove attacker, IBattleMove receiver) {
        float damage = BattleManager.basePowerForMoves * 
            BattleManager.getTypeChartMultiplier(attacker.GetEmotionType(), 
            receiver.GetEmotionType()) * defenseModifiers[];
        
    }

    public void ShiftEmotions(EmotionType emotion, int value)
    {
        //get the current value of the chose Emotion type, and update it accordingly,
        //based on the min,max, current value, and defense modifier
    }

}
