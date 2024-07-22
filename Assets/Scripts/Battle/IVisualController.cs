using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IVisualController
{
    void setAnimationTrigger(string trigger);
    void updateEmotionBarUI(bool updatingPlayerUI, EmotionType affectedEmotion, float damage);
}
