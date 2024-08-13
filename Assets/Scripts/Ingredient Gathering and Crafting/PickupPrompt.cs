using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PickupPrompt : MonoBehaviour
{
    GameObject[] emotionPickups;
    GameObject[] moveTypePickups;
    GameObject emotionIngredientExplanation;
    GameObject moveTypeIngredientExplanation;

    // Start is called before the first frame update
    void Start()
    {
        emotionPickups = GameObject.FindGameObjectsWithTag("EmotionPickup");
        moveTypePickups = GameObject.FindGameObjectsWithTag("MoveTypePickup");
        Debug.Log(emotionPickups.Length + " emotion pickups found.");
        Debug.Log(moveTypePickups.Length + " MoveType pickups found.");
        emotionIngredientExplanation = transform.Find("EmotionIngredientExplanation").gameObject;
        moveTypeIngredientExplanation = transform.Find("MoveTypeIngredientExplanation").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        bool shouldSetActive = false;
        foreach(GameObject pickup in emotionPickups) {
            if (pickup.activeSelf == true && pickup.GetComponent<EmotionTypeIngredient>().isWithinRangeOfPlayer) {
                shouldSetActive = true;
                GameObject textChild = emotionIngredientExplanation.transform.Find("a").gameObject;
                textChild.GetComponent<Text>().text = "\n" + emotionItemName(pickup.emotionType) + ": Used to craft " + 
                    pickup.emotionType.ToString() + " spells";
                break;
            }
        }
        emotionIngredientExplanation.SetActive(shouldSetActive);

        //transparent so it's bad if both are shown. This skips checking for a move type ingredient if the first text box is shown
        if (!shouldSetActive) {
            shouldSetActive = false;
            foreach(GameObject pickup in moveTypePickups) {
                if (pickup.activeSelf == true && pickup.GetComponent<MoveTypeIngredient>().isWithinRangeOfPlayer) {
                    shouldSetActive = true;
                    GameObject textChild = moveTypeIngredientExplanation.transform.Find("a").gameObject;
                    textChild.GetComponent<Text>().text = "\n" + moveTypeItemName(pickup.moveType) + ": Used to craft " + 
                        nameof(pickup.moveType) + " spells";
                    break;
                }
            }
            moveTypeIngredientExplanation.SetActive(shouldSetActive);
        }
    }

    string emotionItemName(EmotionType e) {
        switch (e) {
            case EmotionType.Love:
                return "Cherries";
                break;
            case EmotionType.Wrath:
                return "Spider venom";
                break;
            case EmotionType.Grief:
                return "Milk cap";
                break;
            case EmotionType.Mirth:
                return "Wine";
                break;
            case EmotionType.Null:
                return "--";
                break;
        }
    }

    string moveTypeItemName(MoveType m) {
        switch (m) {
            case MoveType.Shield:
                return "Peach";
                break;
            case MoveType.Transformation:
                return "Butterfly";
                break;
            case MoveType.Enhancement:
                return "Cedar wood";
                break;
            case MoveType.Paralysis:
                return "Amanita mushroom";
                break;
            case MoveType.Pharmaka:
                return "Moly";
                break;
            case MoveType.Damage:
                return "Damage";
                break;
            case MoveType.Null:
                return "--";
                break;
        }
    }
}
