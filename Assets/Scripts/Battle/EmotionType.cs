using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EmotionTypeExtension
{
    public static class EmotionTypeExtensions
    {
        public static Color GetColor(this EmotionType type)
        {
            switch (type)
            {
                case EmotionType.Love:
                    return new Color(0.9411765f, 0.3333333f, 0.8207547f);
                case EmotionType.Wrath:
                    return new Color(1f, 0.2352941f, 0f);
                case EmotionType.Grief:
                    return new Color(0.2941177f, 0, 1f);
                case EmotionType.Mirth:
                    return new Color(0.3921569f, 0.8823529f, 0.2941177f);
                default:
                    throw new System.ArgumentException("Did not account for new EmotionType");
            }
        }

        public static float GetEffectivenessAgainst(this EmotionType attacker, EmotionType defender)
        {
            if ((int)defender >= 4 || (int)attacker >= 4)
            {
                throw new System.ArgumentException("Did not account for all emotion types");
            }
            //define a 2D matrix, using the int of each type as an index for rows and columns
            float[,] effectiveness = {
                { 2f, 0.5f, 2f, 0.5f }, //Love Effectiveness
                { 2.0f, 1.0f, 0.5f, 1.0f}, //Wrath Effectiveness
                { 1.0f, 0.5f, 1.0f, 2.0f},  //Grief Effectiveness
                { 1.0f, 2.0f, 0.5f, 1.0f} //Mirth Effectiveness
            };
            return effectiveness[(int)attacker, (int)defender];
        }

        public static string ToString(this EmotionType emotion)
        {
            switch (emotion)
            {
                case EmotionType.Love:
                    return "Love";
                case EmotionType.Wrath:
                    return "Wrath";
                case EmotionType.Grief:
                    return "Grief";
                case EmotionType.Mirth:
                    return "Mirth";
                default:
                    throw new System.ArgumentException("Unexpected EmotionType");
            }
        }
    }

    public enum EmotionType
    {
        Love = 0, //assign the int appropriate to each enum
        Wrath = 1,
        Grief = 2,
        Mirth = 3,
        Null = 4
    }
}
