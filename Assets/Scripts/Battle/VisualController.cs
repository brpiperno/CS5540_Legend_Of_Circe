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

    //references to the player's sliders
    public Slider CirceGriefSlider;
    public Slider CirceLoveSlider;
    public Slider CirceWrathSlider;
    public Slider CirceMirthSlider;

    //references to the enemy's sliders
    public Slider EnemyGriefSlider;
    public Slider EnemyLoveSlider;
    public Slider EnemyWrathSlider;
    public Slider EnemyMirthSlider;

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
        playerSliderDictionary[EmotionType.Grief] = CirceGriefSlider;
        playerSliderDictionary[EmotionType.Love] = CirceLoveSlider;
        playerSliderDictionary[EmotionType.Wrath] = CirceWrathSlider;
        playerSliderDictionary[EmotionType.Mirth] = CirceMirthSlider;
        opponentSliderDictionary[EmotionType.Grief] = EnemyGriefSlider;
        opponentSliderDictionary[EmotionType.Love] = EnemyLoveSlider;
        opponentSliderDictionary[EmotionType.Wrath] = EnemyWrathSlider;
        opponentSliderDictionary[EmotionType.Mirth] = EnemyMirthSlider;
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
