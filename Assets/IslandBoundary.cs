using UnityEngine;


/// <summary>
/// Behavior Script that creates a text prompt whenever the player reaches the bounds of the island
/// </summary>
[RequireComponent(typeof(Collider))]
public class IslandBoundary : MonoBehaviour
{
    Collider cd;
    public GameObject attemptToEscapeText;

    // Start is called before the first frame update
    void Start()
    {
         cd = GetComponent<Collider>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        //make the text prompt visible
        attemptToEscapeText.SetActive(true);
        Invoke("turnOffText", 5);
    }

    private void turnOffText()
    {
        attemptToEscapeText.SetActive(false);
    }
}
