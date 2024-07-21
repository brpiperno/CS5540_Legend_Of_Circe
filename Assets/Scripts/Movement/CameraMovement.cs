using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Define the position and rotation of the camera, either to follow the player as they explore the island, or zoom into a battle focus.
public class CameraMovement : MonoBehaviour
{
    public Transform playerTransform;
    public BattleManager battleManager; //get a reference to the battle manager to determine if a battle is currently going on

    //TODO: in the future, this script could stop listening while a battle is going on if the battle manager tells it that a battle stops/starts

    //Outside of Battle
    public Vector3 navigationPositionOffset = new Vector3(0, 10, -5); //default local offset from the player

    //In Battle
    public Vector3 battlePositionOffset = new Vector3(3, 1, 3); //default local offset from the player. Camera should be behind the player and to the right.
    public float battleAngleOffset = -15f; //camera should tilt -15 degrees alongthe y axis to also include the enemy in the shot.

    public Quaternion battleQuaternion;

    // Start is called before the first frame update
    void Start()
    {
        playerTransform = playerTransform == null ? GameObject.FindGameObjectWithTag("Player").transform : playerTransform;

        //TODO: define the battle manager 
    }

    // Update is called once per frame
    void Update()
    {
        if (isBattleActive())
        {
            transform.position = playerTransform.position + battlePositionOffset;




            transform.LookAt(playerTransform.position);
            transform.Rotate(Vector3.up, battleAngleOffset);




            //determine the angle around the Y axis between the Camera's z direction and the direction to the player
            Vector3 directionToPlayer = (playerTransform.position - transform.position).normalized;
            float rotationAngle = Vector3.SignedAngle(transform.forward, directionToPlayer, Vector3.up);
            battleQuaternion = Quaternion.AngleAxis(rotationAngle + battleAngleOffset, Vector3.up);
            //transform.rotation *= battleQuaternion;
            //TODO: ask the battle manager for all transforms involved in the battle, and then point to the center instead;
        } else
        {
            transform.position = playerTransform.position + navigationPositionOffset;
            transform.LookAt(playerTransform.position);
        }
    }

    private bool isBattleActive()
    {
        return false;

        //TODO: update this method to ask the Battle Manager
    }
}
