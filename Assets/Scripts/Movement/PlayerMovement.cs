using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Vector3 input;
    public float moveSpeed = 5f;

    // Update is called once per frame
    void Update()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        input = (transform.right * moveHorizontal + transform.forward * moveVertical).normalized * moveSpeed * Time.deltaTime;
        transform.position += input;
    }
}
