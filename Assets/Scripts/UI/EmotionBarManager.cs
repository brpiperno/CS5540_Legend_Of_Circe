using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


/*
This script is not used anywhere else, I didn't see it so I think I re-implemented it
*/


//Script to be added to a slider that adjusts the bar's level when called.
public class EmotionBarManager : MonoBehaviour
{
    //Unity doesn't provide the ability to populate data into a dictionary, so instead we will get references when we start and then insert them into the dictionary
    public Slider wrathSlider;
    public Slider griefSlider;
    public Slider loveSlider;
    public Slider mirthSlider;
    
    private Dictionary<EmotionType, Slider> sliders = new Dictionary<EmotionType, Slider>();
    public EmotionSystem emotionValue; //get a reference to the object's emotion values that it should track
    
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
        }
    }


    public void UpdateEmotionBar()
    {
        foreach (EmotionType emotion in sliders.Keys)
        {
            sliders[emotion].value = emotionValue.GetEmotion(emotion);
        }
    }
}
