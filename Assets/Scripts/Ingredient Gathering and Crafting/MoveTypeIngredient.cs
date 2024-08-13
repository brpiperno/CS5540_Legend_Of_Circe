using UnityEngine;
using EmotionTypeExtension;

/// <summary>
/// Dictates the behavior of an ingredient pickup in the overworld. which can be combined with other ingredients by an inventory system to create spells.
/// </summary>
[RequireComponent(typeof(Collider))]
public class MoveTypeIngredient : MonoBehaviour
{
    //private Collider cldr;
    public MoveType moveType;
    public AudioClip collectedSFX;
    //public float turnOnTime = 5.0f; //How long after instantiation that the collider is enabled
    //private bool turnedOn = false;
    //private float startedTime;

    // Start is called before the first frame update
    void Start()
    {
        //cldr = GetComponent<Collider>();
        //startedTime = Time.time;
    }

    /*private void Update()
    {
        if (!turnedOn && Time.time >= startedTime + turnOnTime)
        {
            cldr.enabled = true;
            cldr.isTrigger = true;
            turnedOn = true;
        }
    }*/

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
            inventory.addMoveIngredient(moveType, this);
            gameObject.SetActive(false);
        }
    }
}
