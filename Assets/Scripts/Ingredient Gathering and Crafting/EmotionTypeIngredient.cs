using UnityEngine;
using EmotionTypeExtension;

/// <summary>
/// Dictates the behavior of an ingredient pickup in the overworld. which can be combined with other ingredients by an inventory system to create spells.
/// </summary>
[RequireComponent(typeof(Collider))]
public class EmotionIngredient : MonoBehaviour
{
       
    
    private Collider cldr;
    public EmotionType emotionType;
    public AudioClip collectedSFX;

    // Start is called before the first frame update
    void Start()
    {
        cldr = GetComponent<Collider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        GameObject collided = other.gameObject;
        if (other.CompareTag("Player"))
        {
            //Add itself to the player's inventory
            AudioSource.PlayClipAtPoint(collectedSFX, Camera.main.transform.position);
            Inventory inventory = collided.GetComponent<Inventory>();
            inventory.addEmotionIngredient(emotionType);

            Destroy(this);
        }
    }
}
