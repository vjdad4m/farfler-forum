using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class WheelchairController : MonoBehaviour
{
    public float speed = 5.0f;
    public float turnSpeed = 100.0f;

    private Rigidbody rb;
    private Vector2 inputVector;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        inputVector = new Vector2(moveHorizontal, moveVertical).normalized;
    }

    void FixedUpdate()
    {
        if (inputVector.magnitude > 0)
        {
            // Move Forward/Backward
            Vector3 moveDirection = transform.forward * inputVector.y * speed;
            rb.MovePosition(rb.position + moveDirection * Time.fixedDeltaTime);

            // Turn Left/Right
            float turn = inputVector.x * turnSpeed * Time.fixedDeltaTime;
            Quaternion turnRotation = Quaternion.Euler(0f, turn, 0f);
            rb.MoveRotation(rb.rotation * turnRotation);
        }
    }
}