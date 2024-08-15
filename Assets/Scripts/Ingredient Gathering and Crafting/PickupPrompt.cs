using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using EmotionTypeExtension;
using TMPro;

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
        emotionIngredientExplanation.SetActive(false);
        moveTypeIngredientExplanation.SetActive(false);
        bool shouldSetActive = false;
        foreach(GameObject pickup in emotionPickups) {
            EmotionTypeIngredient pickupScript = pickup.GetComponent<EmotionTypeIngredient>();
            if (pickup.activeSelf == true && pickupScript.isWithinRangeOfPlayer) {
                shouldSetActive = true;
                GameObject textChild = emotionIngredientExplanation.transform.Find("a").gameObject;
                /*if (textChild == null) {
                    print("Line 35");
                }
                if (textChild.GetComponent<TextMeshProUGUI>() == null) {
                    print("Line 38");
                }
                Debug.Log("Should not be null: " + emotionItemName(pickupScript.emotionType));
                if (pickupScript.emotionType == null) {
                    print("Line 42");
                }*/
                textChild.GetComponent<TextMeshProUGUI>().text = "\n" + emotionItemName(pickupScript.emotionType) + ": Used to craft " + 
                    pickupScript.emotionType.ToString() + " potions";
                break;
            }
        }
        emotionIngredientExplanation.SetActive(shouldSetActive);

        //transparent so it's bad if both are shown. This skips checking for a move type ingredient if the first text box is shown
        if (!shouldSetActive) {
            shouldSetActive = false;
            foreach(GameObject pickup in moveTypePickups) {
                MoveTypeIngredient pickupScript = pickup.GetComponent<MoveTypeIngredient>();
                if (pickup.activeSelf == true && pickupScript.isWithinRangeOfPlayer) {
                    shouldSetActive = true;
                    GameObject textChild = moveTypeIngredientExplanation.transform.Find("a").gameObject;
                    if (pickupScript.moveType == MoveType.Pharmaka) {
                        textChild.GetComponent<TextMeshProUGUI>().text = "\n" + moveTypeItemName(pickupScript.moveType) + ": Used to craft Pharmaka";
                    } else {
                        textChild.GetComponent<TextMeshProUGUI>().text = "\n" + moveTypeItemName(pickupScript.moveType) + 
                            ": Used to craft Potion of " + potionName(pickupScript.moveType);
                    }
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
            case EmotionType.Wrath:
                return "Spider venom";
            case EmotionType.Grief:
                return "Milk cap";
            case EmotionType.Mirth:
                return "Wine";
        }
        return "--";
    }

    string moveTypeItemName(MoveType m) {
        switch (m) {
            case MoveType.Shield:
                return "Peach";
            case MoveType.Transformation:
                return "Butterfly";
            case MoveType.Enhancement:
                return "Cedar wood";
            case MoveType.Paralysis:
                return "Amanita mushroom";
            case MoveType.Pharmaka:
                return "Moly";
            case MoveType.Damage:
                return "Damage";
        }
        return "--";
    }

    string potionName(MoveType m) {
        switch (m) {
            case MoveType.Shield:
                return "Defense";
            case MoveType.Transformation:
                return "Transformation";
            case MoveType.Enhancement:
                return "Healing";
            case MoveType.Paralysis:
                return "Paralysis";
        }
        return "--";
    }
}
