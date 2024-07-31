using UnityEngine;
using EmotionTypeExtension;

/// <summary>
/// Dictates the behavior of an ingredient pickup in the overworld. which can be combined with other ingredients by an inventory system to create spells.
/// </summary>
[RequireComponent(typeof(Collider))]
public class MoveTypeIngredient : MonoBehaviour
{


    private Collider cldr;
    public MoveType moveType;

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
            Inventory inventory = collided.GetComponent<Inventory>();
            inventory.addMoveIngredient(moveType);
            Destroy(this);
        }
    }
}
