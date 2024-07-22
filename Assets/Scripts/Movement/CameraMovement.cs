using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Define the position and rotation of the camera, either to follow the player as they explore the island, or zoom into a battle focus.
public class CameraMovement : MonoBehaviour
{
    //public Transform playerTransform;
    private BattleManager battleManager; //get a reference to the battle manager to determine if a battle is currently going on
    public Camera BattleCam; //used when the player is in battle.
    public Camera NavCam; //used when the player is navigating through the island.


    // Start is called before the first frame update
    void Start()
    {
        battleManager = FindFirstObjectByType<BattleManager>();
        
        NavCam.enabled = (battleManager == null);
        NavCam.gameObject.GetComponent<AudioListener>().enabled = (battleManager != null);

        BattleCam.enabled = (battleManager != null);
        BattleCam.gameObject.GetComponent<AudioListener>().enabled = (battleManager != null);
    }
}
