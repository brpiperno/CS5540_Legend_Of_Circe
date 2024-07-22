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
    //public Canvas canvas;
    Dictionary<EmotionType, Slider> playerSliderDictionary;
    Dictionary<EmotionType, Slider> opponentSliderDictionary;
    void Start()
    {
        if (circe == null) {
            circe = GameObject.FindGameObjectWithTag("Player");
        }
        playerSystem = circe.GetComponent<EmotionSystem>();
        opponentSystem = opponent.GetComponent<EmotionSystem>();
        /* Debugging
        GameObject isValidGameObject = GameObject.Find("Grief bar");
        if (isValidGameObject == null) {
            Debug.Log("Could not find the object");
        }
        Slider doesThisWorkToo = GameObject.Find("Grief bar").GetComponent<Slider>();
        if (doesThisWorkToo == null) {
            Debug.Log("Could not find the slider component");
        }*/
        //playerSliderDictionary[EmotionType.Grief] = canvas.transform.FindChild("Grief bar").gameObject.GetComponent<Slider>();
        playerSliderDictionary[EmotionType.Grief] = gameObject.transform.Find("Grief bar").GetComponent<Slider>();
        playerSliderDictionary[EmotionType.Love] = gameObject.transform.Find("Love bar").GetComponent<Slider>();
        playerSliderDictionary[EmotionType.Wrath] = gameObject.transform.Find("Wrath bar").GetComponent<Slider>();
        playerSliderDictionary[EmotionType.Mirth] = gameObject.transform.Find("Mirth bar").GetComponent<Slider>();
        opponentSliderDictionary[EmotionType.Grief] = gameObject.transform.Find("Enemy Grief bar").GetComponent<Slider>();
        opponentSliderDictionary[EmotionType.Love] = gameObject.transform.Find("Enemy Love bar").GetComponent<Slider>();
        opponentSliderDictionary[EmotionType.Wrath] = gameObject.transform.Find("Enemy Wrath bar").GetComponent<Slider>();
        opponentSliderDictionary[EmotionType.Mirth] = gameObject.transform.Find("Enemy Mirth bar").GetComponent<Slider>();
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
