using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using EmotionTypeExtension;
using System;

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

    public GameObject upArrow;
    public GameObject leftArrow;
    public GameObject rightArrow;
    public GameObject downArrow;
    public AudioClip playerMoveSFX;
    public float moveSFXPitch;

    public void setAnimationTrigger(EmotionType emotion, MoveType moveType) {
        GameObject attack = Instantiate(playerAttackEffect, 
            playerAttackEffectPosition,
            Quaternion.Euler(playerAttackEffectRotation));
        //TODO: Invoke setAskInput at 2 seconds
        //TODO: Invoke removehighlight at 2 seconds
            // Is called in PlayerMovePicker?

        if (moveType == MoveType.Damage)
        {
            ParticleSystem.MainModule ps = attack.GetComponent<ParticleSystem>().main;
            ps.startColor = emotion.GetColor();
        }
        Camera.main.GetComponent<AudioSource>().pitch = moveSFXPitch;
        AudioSource.PlayClipAtPoint(playerMoveSFX, Camera.main.transform.position);

    }
    public void updateEmotionBarUI() {
        //do nothing for now. EmotionBarManager acts independently and checks each frame
        //TODO: integrate EmotionBarManager into this class or at least control when the emotion bar manager starts checking
    }

    public void updateEmotionWheelSelection(EmotionType emotion)
    {
        switch (emotion) {
            case EmotionType.Grief:
                upArrow.SetActive(true);
                break;
            case EmotionType.Love:
                leftArrow.SetActive(true);
                break;
            case EmotionType.Wrath:
                rightArrow.SetActive(true);
                break;
            case EmotionType.Mirth:
                downArrow.SetActive(true);
                break;
            default:
                throw new ArgumentException("Invalid emotion given for emotion wheel selection animation.");
        }
    }

    // Returns all arrows to their original color.
    public void RemoveHighlight() {
        upArrow.SetActive(false);
        leftArrow.SetActive(false);
        rightArrow.SetActive(false);
        downArrow.SetActive(false);
    }

    public void setEmotionWheelVisibility(bool isVisible)
    {
        //TODO: turn emotion wheel on/off
    }
}
