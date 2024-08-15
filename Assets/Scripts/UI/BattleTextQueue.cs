using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;



[RequireComponent(typeof(TextMeshProUGUI))]
/// <summary>
/// Queue that is used to manage text to be displayed to the player
/// </summary>
public class BattleTextQueue : MonoBehaviour
{

    private Queue<string> queue = new Queue<string>();
    public float minDuration = 3.0f; //with an average reading speed of 4 words per second, the duration will be clamped to a minimum 3 words.
    public float maxDuration = 20.0f;
    public float msgActiveTimer = 0.0f; //how long has the current message been displayed?
    public float currentDuration = 3.0f; //duration for the current message;
    private TextMeshProUGUI textfield;
    public Button nextButton;

    void Awake()
    {
        textfield = GetComponent<TextMeshProUGUI>();
        if (nextButton == null)
        {
            nextButton = transform.parent.Find("NextButton").GetComponent<Button>();
        }
    }

    // Update is called once per frame
    /*
    void Update()
    {
        //msgActiveTimer += Time.deltaTime;
        if (msgActiveTimer >= currentDuration && queue.TryDequeue(out string msg))
        {
            textfield.text = msg;
            currentDuration = calculateDuration(msg);
            msgActiveTimer = 0.0f;
        }
    }
    */

    public void Dequeue()
    {
        bool hasNext = queue.TryDequeue(out string result);
        textfield.text = hasNext ? result : "";
        updateButton();

    }

    /// <summary>
    /// Add a message to the queue. For priority messages, clear the queue and 
    /// </summary>
    /// <param name="text"></param>
    /// <param name="resetQueue"></param>
    public void Enqueue(string text, bool resetQueue)
    {
        if (text.Length == 0)
        {
            return;
        } else if (resetQueue) {
            queue.Clear();
            queue.Enqueue(text);
            msgActiveTimer = currentDuration; //run out the timer so that the message is displayed the nextframe
        } else
        {
            queue.Enqueue(text);
        }
        if (textfield.text == "")
        {
            Dequeue();
        }
        updateButton();
    }

    public void Enqueue(string text)
    {
        Enqueue(text, false);

    }

    private float calculateDuration(string text)
    {
        //average reading speed is approximately 25 characters per second
        return Mathf.Clamp(text.Length / 25, minDuration, maxDuration);
    }

    private void updateButton()
    {
        nextButton.interactable = queue.Count > 0;
    }

}
