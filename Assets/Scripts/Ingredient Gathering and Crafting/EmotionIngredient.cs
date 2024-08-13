using UnityEngine;
using EmotionTypeExtension;

/// <summary>
/// Dictates the behavior of an ingredient pickup in the overworld. which can be combined with other ingredients by an inventory system to create spells.
/// </summary>
[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Renderer))]
public class EmotionIngredient : MonoBehaviour
{
    //private Collider cldr;
    public EmotionType emotionType;
    public AudioClip collectedSFX;
    //public float turnOnTime = 5.0f; //How long after instantiation that the collider is enabled
    //private bool turnedOn = false;
    //private float startedTime;

    public float playerDetectionDistance = 10f;

    private Renderer rndrer;
    Transform player;
    Transform canvas;

    // Start is called before the first frame update
    void Start()
    {
        //cldr = GetComponent<Collider>();
        //startedTime = Time.time;
        rndrer = GetComponent<Renderer>();
        Color color = emotionType.GetColor();
        color.a = 0.2f;
        rndrer.material.color = color;

        player = GameObject.FindGameObjectWithTag("Player").transform;
        canvas = GameObject.FindGameObjectWithTag("Canvas").transform;
    }

    void Update() {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        if (distanceToPlayer < playerDetectionDistance) {
            Transform child = canvas.Find("EmotionIngredientExplanation");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        GameObject collided = other.gameObject;
        if (other.CompareTag("Player"))
        {
            //Add itself to the player's inventory
            AudioSource.PlayClipAtPoint(collectedSFX, Camera.main.transform.position);
            Inventory inventory = collided.GetComponent<Inventory>();
            if (inventory == null)
            {
                inventory = collided.GetComponentInChildren<Inventory>();
            }
            if (inventory == null)
            {
                inventory = GameObject.FindFirstObjectByType<Inventory>();
            }
            inventory.addEmotionIngredient(emotionType, gameObject);
            gameObject.SetActive(false);
        }
    }
}
