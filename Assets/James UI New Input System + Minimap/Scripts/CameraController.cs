using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// For new Input System.
using UnityEngine.InputSystem;

namespace ThirdPersonController
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField, Range(0, 3)] private float sensitivity = 0.5f;
        [SerializeField] private Vector2 verticalLookBounds = new Vector2(-90f, 45f);

        // To be able to change in inspector for others.
        [SerializeField] private InputActionReference look;

        [SerializeField] private Transform myPlayer;

        private Vector2 myRotation = Vector2.zero;
        private Vector3 myVelocity = Vector3.zero;

        [Header("Collisions")]
        [SerializeField] private Transform myCamera;
        // The distance from the player the camera will sit.
        [SerializeField, Min(0.1f)] private float cameraDistance = 3f;
        // How smooth the camer will be.
        [SerializeField, Range(0.05f, 0.2f)] private float damping = 0.1f;
        // The radius that the camera has a collider of and offsets it's position from hit things
        [SerializeField, Range(0.1f, 0.5f)] private float colliderRadius = 0.25f;

        // Start is called before the first frame update
        void Start()
        {
            look.action.performed += OnLookPerformed;

            // Start the camera at the maximum distance from the player.
            myCamera.localPosition = new Vector3(0, 0, -cameraDistance);
            // DO!
        }

        // Update is called once per frame
        void Update()
        {
            transform.localRotation = Quaternion.AngleAxis(myRotation.y, Vector3.left);
            myPlayer.localRotation = Quaternion.AngleAxis(myRotation.x, Vector3.up);

            Vector3 newPos = Vector3.SmoothDamp(myCamera.position, CalculatePosition(), ref myVelocity, damping);

            myCamera.position = newPos;
        }

        private Vector3 CalculatePosition()
        {
            // Calculate the default position of the camera using the target.
            Vector3 newPos = transform.position - transform.forward * cameraDistance;

            // Using the inverse of the forward for the direction.
            Vector3 direction = -transform.forward;

            if(Physics.Raycast(transform.position, direction, out RaycastHit hit, cameraDistance))
            {
                // Set the newPosition to slightly offset from the hit collider.
                newPos = hit.point + transform.forward * colliderRadius;
            }
            return newPos;
        }
        private void OnLookPerformed(InputAction.CallbackContext _context)
        {
            // The actual value of the input (thumbstick pos/mouse delta).
            Vector2 value = _context.ReadValue<Vector2>();

            // Add the input values.
            myRotation += value * sensitivity;

            myRotation.y = Mathf.Clamp(myRotation.y, verticalLookBounds.x, verticalLookBounds.y);
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, transform.position - transform.forward * cameraDistance);

            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position - transform.forward * cameraDistance, colliderRadius);
        }


    }
}