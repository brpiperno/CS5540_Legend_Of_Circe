using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VisualController : MonoBehaviour, IVisualController
{
    public GameObject circe;
    public GameObject opponent;
    public IEmotion playerSystem;
    public IEmotion opponentSystem;
    Dictionary<EmotionType, Slider> playerSliderDictionary;
    Dictionary<EmotionType, Slider> opponentSliderDictionary;
    void Start()
    {
        if (circe == null) {
            circe = GameObject.FindGameObjectWithTag("Player");
        }
        playerSystem = circe.GetComponent<EmotionSystem>();
        opponentSystem = opponent.GetComponent<EmotionSystem>();
        playerSliderDictionary[EmotionType.Grief] = GameObject.Find("Grief bar").GetComponent<Slider>();
        playerSliderDictionary[EmotionType.Love] = GameObject.Find("Love bar").GetComponent<Slider>();
        playerSliderDictionary[EmotionType.Wrath] = GameObject.Find("Wrath bar").GetComponent<Slider>();
        playerSliderDictionary[EmotionType.Mirth] = GameObject.Find("Mirth bar").GetComponent<Slider>();
        opponentSliderDictionary[EmotionType.Grief] = GameObject.Find("Enemy Grief bar").GetComponent<Slider>();
        opponentSliderDictionary[EmotionType.Love] = GameObject.Find("Enemy Love bar").GetComponent<Slider>();
        opponentSliderDictionary[EmotionType.Wrath] = GameObject.Find("Enemy Wrath bar").GetComponent<Slider>();
        opponentSliderDictionary[EmotionType.Mirth] = GameObject.Find("Enemy Mirth bar").GetComponent<Slider>();
    }
    public void setAnimationTrigger(string trigger) {

    }
    public void updateEmotionBarUI(bool updatingPlayerUI, EmotionType affectedEmotion, float newValue) {
        Slider slider;
        if (updatingPlayerUI) {
            slider = playerSliderDictionary[affectedEmotion];
        } else {
            slider = opponentSliderDictionary[affectedEmotion];
        }
        slider.value = newValue;
    }
}
