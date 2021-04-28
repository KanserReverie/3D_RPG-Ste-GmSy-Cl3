using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class AndrewMovement : MonoBehaviour
{
    [Header("Character")]
    [SerializeField] private CharacterController controller;
    [SerializeField] private Transform cam;

    [Header("Variables")]
    [SerializeField] private float speed = 6f;
    [SerializeField] private float turnSmoothTime = 0.1f;
    [SerializeField] private float gravity = -80;
    [SerializeField] private float yVelocity;
    [SerializeField] private float jumpSpeed = 4;

    private float turnSmoothVelocity;
    private void Update()
    {
        controller.Move(Movement() + Jump() * Time.deltaTime);
    }
    private Vector3 Movement()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        
        // "normalized" gets the direction and changes it to a circle so that it only is 1 unit of movement in all directions.
        Vector3 direction = new Vector3(horizontal, 0, vertical).normalized;

        if(direction.magnitude >= 0.1f)
        {
            // Gets the angle its moving at.
            float targetAngle = Mathf.Atan2(direction.x, direction.z)* Mathf.Rad2Deg;

            // Smoothly rotates the character.
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y,
                                targetAngle,
                                ref turnSmoothVelocity,
                                turnSmoothTime);

            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;

            return moveDirection * speed;
            // float targetAngle = Math.f
        }
        return Vector3.zero;
    }

    private Vector3 Jump()
    {
        // If grounded it will pust to the ground.
        if(controller.isGrounded)
        {
            yVelocity = -2;
        }
        if(Input.GetButtonDown("Jump") && controller.isGrounded)
        {
            yVelocity += jumpSpeed;
        }

        // Since it is "* Time.deltaTime" x 2 (once here and in update) it will feel like gravity.
        yVelocity += gravity * Time.deltaTime;

        // Terminal Velocity = -20.
        yVelocity = Mathf.Max(yVelocity, -20);

        // Returns speed down.
        return new Vector3(0f, yVelocity, 0f);
    }
}
