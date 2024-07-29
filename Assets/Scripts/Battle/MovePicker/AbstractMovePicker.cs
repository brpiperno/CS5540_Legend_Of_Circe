using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EmotionSystem))]
[RequireComponent(typeof(VisualController))]
public abstract class AbstractMovePicker : MonoBehaviour, IMovePicker
{
    protected bool isAskingForPlayInput = false;
    protected VisualController visualController;
    protected EmotionSystem userEmotionSystem;

    // Start is called before the first frame update
    void Start()
    {
        userEmotionSystem = GetComponent<EmotionSystem>();
        visualController = GetComponent<VisualController>();
    }

    public void MoveRequested()
    {
        isAskingForPlayInput = true;
    }
}
