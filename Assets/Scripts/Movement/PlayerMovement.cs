using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float rotationSpeed = 10f;
    CharacterController controller;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
    }


    // Update is called once per frame
    void Update()
    {
        //Rotate player on left/right
        //move forwards or backwards on left right
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        //input = (transform.right * moveHorizontal + transform.forward * moveVertical).normalized * moveSpeed * Time.deltaTime;
        Vector3 moveDirection = transform.forward * moveVertical;
        moveDirection.y -= 9.81f * Time.deltaTime; //constantly apply gravity down
        controller.Move(moveDirection * Time.deltaTime * moveSpeed);
        transform.Rotate(Vector3.up, moveHorizontal * Time.deltaTime * rotationSpeed);
    }
}
