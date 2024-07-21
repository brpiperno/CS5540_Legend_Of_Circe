using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VisualController : MonoBehaviour, IVisualController
{
    public GameObject circe;
    public GameObject opponent;
    public EmotionInterface playerSystem;
    public EmotionInterface opponentSystem;
    Dictionary<EmotionType, Slider> sliderDictionary;
    void Start()
    {
        if (circe == null) {
            circe = GameObject.FindGameObjectWithTag("Player");
        }
        playerSystem = circe.GetComponent<EmotionSystem>();
        opponentSystem = opponent.GetComponent<EmotionSystem>();
        
    }
    public void setAnimationTrigger(string trigger) {

    }
    public void updateEmotionBarUI() {

    }
}
