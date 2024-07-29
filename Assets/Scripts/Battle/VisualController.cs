using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using EmotionTypeExtension;

/// <summary>
/// Simple implementation of an IVisualController intended for either a single NPC or player.
/// Upon method calls, it updates the following:
/// - An animator controller
/// - Instantiates particle systems for IBattleMoves
/// - Updates any 2D UIs (emotion bars or player-specific UI)
/// </summary>

[RequireComponent(typeof(IEmotion))]
public class VisualController : MonoBehaviour, IVisualController
{
    private IEmotion userEmotion;
    public GameObject playerAttackEffect;
    public Vector3 playerAttackEffectPosition;
    public Vector3 playerAttackEffectRotation;

    public void setAnimationTrigger(EmotionType emotion, MoveType moveType) {
        GameObject attack = Instantiate(playerAttackEffect, 
            playerAttackEffectPosition,
            Quaternion.Euler(playerAttackEffectRotation));
        //TODO: Invoke setAskInput at 2 seconds
        //TODO: Invoke removehighlight at 2 seconds
        if (moveType == MoveType.Damage)
        {
            var ps = attack.GetComponent<ParticleSystem>();
            var newColor = ps.main;
            newColor.startColor = emotion.GetColor();
        }
    }

    public void updateEmotionBarUI() {
        //do nothing for now. EmotionBarManager acts independently and checks each frame
        //TODO: integrate EmotionBarManager into this class or at least control when the emotion bar manager starts checking
    }

    public void updateEmotionWheelSelection(EmotionType emotion)
    {
        //TODO: highlight chosen emotion
    }

    public void setEmotionWheelVisibility(bool isVisible)
    {
        //TODO: turn emotion wheel on/off
    }
}
