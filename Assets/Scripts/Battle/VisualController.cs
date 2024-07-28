using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class VisualController : MonoBehaviour, IVisualController
{
    public GameObject circe;
    public GameObject opponent;
    public IEmotion playerSystem;
    public IEmotion opponentSystem;

    void Start()
    {
        if (circe == null) {
            circe = GameObject.FindGameObjectWithTag("Player");
        }
        playerSystem = circe.GetComponent<EmotionSystem>();
        opponentSystem = opponent.GetComponent<EmotionSystem>();
    }
    public void setAnimationTrigger(string trigger) {

    }
}
