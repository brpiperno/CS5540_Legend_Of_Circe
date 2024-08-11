using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(Collider))] //adding a trigger collider for overworld boundary detection;
//[RequireComponent(typeof(Animator))]

public class ThirdPersonController : MonoBehaviour
{

    CharacterController controller;
    Vector3 input;
    public float moveSpeed = 1.5f;
    public float jumpHeight = 2f;
    Vector3 moveDirection;
    public float gravity = 9.81f;
    public float airControl = 1.0f;

    Animator animator;

    //Animation variables
    //private int animState;
    //private Animator animator;
    //public string animStateTriggerName = "animState";


    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        animator.SetInteger("state", 0);
    }

    // Update is called once per frame
    void Update()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        input = (transform.right * moveHorizontal + transform.forward * moveVertical).normalized * moveSpeed;

        //rotate the character in the direction of the camera
        float cameraYawRotation = Camera.main.transform.eulerAngles.y;
        //Debug.Log("cameraYawRotation is " + cameraYawRotation);
        Quaternion directionToFace = Quaternion.Euler(0f, cameraYawRotation, 0f);
        transform.rotation = Quaternion.Lerp(transform.rotation, directionToFace, Time.deltaTime * 10);


        if (controller.isGrounded)
        {
            if (moveHorizontal != 0 || moveVertical != 0) {
                animator.SetInteger("state", 1);
            } else {
                animator.SetInteger("state", 0);
            }
            moveDirection = input;
            bool toJump = Input.GetButton("Jump");
            moveDirection.y = toJump ? Mathf.Sqrt(2 * jumpHeight * gravity) : 0.0f;
            //animState = toJump ? 2 : input.magnitude >= 0.01 ? 1 : 0; //if jumping use 2. Else use 1 if input is nonzero. Else use 0;

        }
        else
        {
            input.y = moveDirection.y;
            moveDirection = Vector3.Lerp(moveDirection, input, Time.deltaTime * airControl);
        }

        moveDirection.y -= gravity * Time.deltaTime; //constantly apply gravity downwards
        controller.Move(moveDirection * Time.deltaTime);
        //animator.SetInteger(animStateTriggerName, animState);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Boundary"))
        {
            other.gameObject.GetComponent<IslandBoundary>().ReachedBoundary();
        }
    }
}
