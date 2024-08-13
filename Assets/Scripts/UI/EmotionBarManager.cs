using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using EmotionTypeExtension;
using TMPro;
using TMPro.Examples;


//Script to be added to a slider that adjusts the bar's level when called.
public class EmotionBarManager : MonoBehaviour
{
    public float sliderSpeed = 10;

    //Unity doesn't provide the ability to populate data into a dictionary, so instead we will get references when we start and then insert them into the dictionary
    public Slider wrathSlider;
    public Slider griefSlider;
    public Slider loveSlider;
    public Slider mirthSlider;

    public TextMeshProUGUI wrathDefenseText;
    public TextMeshProUGUI griefDefenseText;
    public TextMeshProUGUI loveDefenseText;
    public TextMeshProUGUI mirthDefenseText;

    private Dictionary<EmotionType, Slider> sliders = new Dictionary<EmotionType, Slider>();
    private Dictionary<EmotionType, Text> barValues = new Dictionary<EmotionType, Text>();
    private Dictionary<EmotionType, TextMeshProUGUI> defenseModifiers; 
    public EmotionSystem emotionSystem; //get a reference to the object's emotion values that it should track

    private bool isUpdatingUI = true; //whether the emotion bars are currently being updated

    // Start is called before the first frame update
    void Start()
    {
        sliders.Add(EmotionType.Love, loveSlider);
        sliders.Add(EmotionType.Wrath, wrathSlider);
        sliders.Add(EmotionType.Grief, griefSlider);
        sliders.Add(EmotionType.Mirth, mirthSlider);

        foreach (EmotionType type in sliders.Keys)
        {
            //set the maximum (100) and minimum (0)
            sliders[type].minValue = 0;
            sliders[type].maxValue = 100;
            sliders[type].value = 100;
        }

        barValues.Add(EmotionType.Grief, griefSlider.transform.Find("Value").gameObject.GetComponent<Text>());
        barValues.Add(EmotionType.Love, loveSlider.transform.Find("Value").gameObject.GetComponent<Text>());
        barValues.Add(EmotionType.Wrath, wrathSlider.transform.Find("Value").gameObject.GetComponent<Text>());
        barValues.Add(EmotionType.Mirth, mirthSlider.transform.Find("Value").gameObject.GetComponent<Text>());

        defenseModifiers = new Dictionary<EmotionType, TextMeshProUGUI>() {
            {EmotionType.Grief, griefDefenseText },
            {EmotionType.Love, loveDefenseText   },
            {EmotionType.Mirth, mirthDefenseText },
            {EmotionType.Wrath, wrathDefenseText }
        };
    }

    private void Update()
    {
        if (isUpdatingUI)
        {
            
            foreach (EmotionType emotion in sliders.Keys)
            {
                float current = sliders[emotion].value;
                float actual = emotionSystem.GetEmotionValue(emotion);
                
                if (current != actual)
                {
                    //sliders[emotion].value = actual;
                    float setTo = Mathf.Clamp(Mathf.Lerp(current, actual, Time.deltaTime * sliderSpeed), 0, 100);
                    sliders[emotion].value = setTo;
                    barValues[emotion].text = System.Math.Round(setTo, 0).ToString() + "/" + BattleManager.maxSliderValue.ToString();
                }
                else
                {
                    //allBarsFinalized = allBarsFinalized && current == actual; //bars are finalized only if current == actual for all emotions
                }
                //Debug.Log(name + ": slider value: " + current + " actual value: " + actual);
            }
        }
    }

    //public method to be called by an IEmotion to update the bars according to the IEmotion's Values
    public void UpdateEmotionBar()
    {
        isUpdatingUI = true;
    }

    public bool isChanging()
    {
        return isUpdatingUI;
    }

    public void updateDefenseModifierDisplay(EmotionType emotion, int modifier)
    {
        defenseModifiers[emotion].text = modifier.ToString() + "x";
    }
}
