using UnityEngine;
using UnityEngine.UI;


/// <summary>
/// Behavior Script that creates a text prompt whenever the player reaches the bounds of the island
/// </summary>
[RequireComponent(typeof(Collider))]
public class IslandBoundary : MonoBehaviour
{
    public GameObject SystemDialoguePanel;
    Text SystemDialogueText;

    // Start is called before the first frame update
    void Start()
    {
        SystemDialogueText = SystemDialoguePanel.GetComponentInChildren<Text>();
    }


    void turnOffText()
    {
        SystemDialoguePanel.SetActive(false);
    }

    public void ReachedBoundary()
    {
        SystemDialoguePanel.SetActive(true);
        SystemDialogueText.enabled = true;
        SystemDialogueText.text = "Your exile persists. You may not leave Aeaea.";
        Invoke("turnOffText", 5);
    }
}
