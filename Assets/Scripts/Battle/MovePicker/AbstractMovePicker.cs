using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(IEmotion))]
[RequireComponent(typeof(IVisualController))]
public abstract class AbstractMovePicker : MonoBehaviour, IMovePicker
{
    protected bool isAskingForPlayInput = false;
    protected bool isEmotionChosen = false;
    public IVisualController visualController;
    public IEmotion userEmotionSystem;

    // Start is called before the first frame update
    void Start()
    {
        {
            if (userEmotionSystem == null)
            {
                userEmotionSystem = GetComponent<IEmotion>();
            }
            if (visualController == null)
            {
                visualController = GetComponent<VisualController>();
            }
        }
    }

    public void MoveRequested()
    {
        isAskingForPlayInput = true;
    }
}
