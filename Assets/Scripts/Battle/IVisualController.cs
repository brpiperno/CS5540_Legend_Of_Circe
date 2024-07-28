using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EmotionTypeExtension;

/// <summary>
/// Interface for script that controls UI and animations for an Emotion System.
/// This also handles player specific UI updates (this should be moved to a child interface)
/// </summary>
public interface IVisualController
{
    /// <summary>
    /// Set the animation controller of this character according to the given trigger.
    /// </summary>
    /// <param name="trigger"></param>
    void setAnimationTrigger(string trigger);
    
    /// <summary>
    /// Update the emotion bar UI for this character;
    /// </summary>
    void updateEmotionBarUI();

    /// <summary>
    /// Method specific to Player Characters, to update what emotion was chosen.
    /// </summary>
    /// <param name="emotion"></param>
    void updateEmotionWheelSelection(EmotionType emotion);

    /// <summary>
    /// Method specific to Player Characters, to show hide the emotion wheel.
    /// </summary>
    /// <param name="isVisible">true if the emotion wheel should be visible</param>
    void setEmotionWheelVisibility(bool isVisible);
}
