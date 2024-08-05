using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EmotionSystem))]
public abstract class AbstractMovePicker : MonoBehaviour, IMovePicker
{
    protected bool isAskingForPlayInput = false;
    protected EmotionSystem userEmotionSystem;

    // Start is called before the first frame update
    protected void Start()
    {
        userEmotionSystem = GetComponent<EmotionSystem>();
    }

    public void MoveRequested()
    {
        isAskingForPlayInput = true;
        //Debug.Log("AbstractMovePicker: moveRequested reached");
    }

    public void UpdateLastMoveRecieved(IBattleMove received)
    {
        //Do nothing most of the time
    }
}
