using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class VisualController : IVisualController
{
    public GameObject circe;
    public GameObject opponent;
    public IEmotion playerSystem;
    public IEmotion opponentSystem;
    public GameObject upArrow;
    public GameObject leftArrow;
    public GameObject rightArrow;
    public GameObject downArrow;

    void Start()
    {
        if (circe == null) {
            circe = GameObject.FindGameObjectWithTag("Player");
        }
        playerSystem = circe.GetComponent<EmotionSystem>();
        opponentSystem = opponent.GetComponent<EmotionSystem>();
        upArrow = GameObject.FindGameObjectWithTag("UpArrow");
        leftArrow = GameObject.FindGameObjectWithTag("LeftArrow");
        rightArrow = GameObject.FindGameObjectWithTag("RightArrow");
        downArrow = GameObject.FindGameObjectWithTag("DownArrow");
    }
    public void setAnimationTrigger(string trigger) {

    }
    public void HighlightArrow(int direction) {
        switch (direction) {
            case 0:
                upArrow.SetActive(true);
                Debug.Log("Set up arrow to active");
                break;
            case 1:
                leftArrow.SetActive(true);
                break;
            case 2:
                rightArrow.SetActive(true);
                break;
            case 3:
                downArrow.SetActive(true);
                break;
            default:
                throw new ArgumentException("Invalid arrow direction given for highlight animation.");
        }
    }
    public void RemoveHighlight() {
        upArrow.SetActive(false);
        leftArrow.SetActive(false);
        rightArrow.SetActive(false);
        downArrow.SetActive(false);
    }
}
