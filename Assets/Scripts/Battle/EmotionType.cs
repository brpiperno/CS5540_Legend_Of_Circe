using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EmotionType
{
    Love,
    Wrath,
    Grief,
    Mirth
}

// Get the name of the EmotionType by doing
    //Enum.GetName(EmotionType.Love);

// How you do enum methods in C#:
public static class EmotionTypeMethods {
    // Call this by doing EmotionType.Love.getColor()
    public static Color getColor(this EmotionType emotionType) {
        switch (emotionType) {
            case EmotionType.Love:
                return new Color(0.9411765f, 0.3333333f, 0.8207547f);
            case EmotionType.Wrath:
                return new Color(1f, 0.2352941f, 0f);
            case EmotionType.Grief:
                return new Color(0.2941177f, 0, 1f);
            case EmotionType.Mirth:
                return new Color(0.3921569f, 0.8823529f, 0.2941177f);
            default:
                return new Color(0, 0, 0);
        }
    }
    // Call this by doing EmotionType.Love.getEffectivenessAgainst(defender)
    // With one parameter just like that
    public static float getEffectivenessAgainst(this EmotionType attacker, EmotionType defender) {
        return 1;
    }
}
