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
    public bool hasBlockAnimation;
    GameObject opponent;
    private Animator anim;

    public void Start() {
        opponent = GameObject.FindGameObjectWithTag("Enemy");
        anim = opponent.GetComponent<Animator>();
    }

    public IEnumerator setAnimationTrigger(EmotionType emotion, MoveType moveType) {
        //Camera.main.GetComponent<AudioSource>().pitch = moveSFXPitch;
        if (hasBlockAnimation) {
            yield return new WaitForSeconds(1f);
        }
        AudioSource.PlayClipAtPoint(playerMoveSFX, Camera.main.transform.position);
        GameObject attack = Instantiate(playerAttackEffect, 
            playerAttackEffectPosition,
            Quaternion.Euler(playerAttackEffectRotation));
        if (moveType == MoveType.Damage)
        {
            var ps = attack.GetComponent<ParticleSystem>();
            var newColor = ps.main;
            newColor.startColor = emotion.GetColor();
        }
        //Camera.main.GetComponent<AudioSource>().pitch = moveSFXPitch;
        //AudioSource.PlayClipAtPoint(playerMoveSFX, Camera.main.transform.position);

    }

    public void updateEmotionBarUI() {
        //do nothing for now. EmotionBarManager acts independently and checks each frame
        //TODO: integrate EmotionBarManager into this class or at least control when the emotion bar manager starts checking
    }

    public void updateEmotionWheelSelection(EmotionType emotion)
    {
        //turn off all others
        setEmotionWheelVisibility(false);

        switch (emotion)
        {
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
        upArrow.SetActive(isVisible);
        leftArrow.SetActive(isVisible);
        rightArrow.SetActive(isVisible);
        downArrow.SetActive(isVisible);
    }

    public void PlayEnemySpellCastAnimation() {
        anim.SetTrigger("spellCast");
    }

    public void PlayEnemyBlockAnimation() {
        Debug.Log(gameObject.name + " hasBlockAnimation: " + hasBlockAnimation);
        if (opponent.GetComponent<VisualController>().hasBlockAnimation) {
            anim.SetTrigger("block");
        }
    }
}
