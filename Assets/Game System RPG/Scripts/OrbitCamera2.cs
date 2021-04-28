using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]

// To get a cammera that orbits input object.
public class OrbitCamera2 : MonoBehaviour
{
    #region Focus.
    // Thing we are focusing on.
    [SerializeField]
    Transform focus = default;

    // How far away.
    [SerializeField]
    float distance = 5f;

    [SerializeField, Min(0f)]
    float focusRadius = 1f;
    Vector3 focusPoint, previousFocusPoint;

    [SerializeField, Range(0f, 1f)]
    float focusCentering = 0.5f;
    #endregion

    #region Orbit.
    Vector2 orbitAngles = new Vector2(45f, 0f);
    [SerializeField, Range(1f, 360f)]
    float rotationSpeed = 90f;
    [SerializeField, Range(-89f, 89f)]
    float minVerticalAngle = -30f, maxVerticalAngle = 60f;
    #endregion

    #region Align.
    [SerializeField, Min(0f)]
    float alignDelay = 5f;
    float lastManualRotationTime;
    #endregion

    private void Awake()
    {
        focusPoint = focus.position;
        transform.localRotation = Quaternion.Euler(orbitAngles);
    }

    private void OnValidate()
    {
        if (maxVerticalAngle < minVerticalAngle)
        {
            maxVerticalAngle = minVerticalAngle;
        }
    }

    // LateUpdate comes after the update thus to follow the player.
    // Mainly used for cameras.
    void LateUpdate()
    {
        UpdateFocusPoint();
        //ManualRotation();
        //ConstraingAngles();
        Quaternion lookRotation = Quaternion.Euler(orbitAngles);
        if (ManualRotation() || AutomaticRotation())
        {
            ConstraingAngles();
            lookRotation = Quaternion.Euler(orbitAngles);
        }
        else
        {
            lookRotation = transform.localRotation;
        }
        Vector3 lookDirection = lookRotation * Vector3.forward;
        Vector3 lookPosition = focusPoint - lookDirection * distance;
        transform.SetPositionAndRotation(lookPosition, lookRotation);
    }

    bool ManualRotation()
    {
        Vector2 input = new Vector2(-Input.GetAxis("Mouse Y"), Input.GetAxis("Mouse X"));
        const float e = 0.001f;
        if (input.x < -e || input.x > e || input.y < -e || input.y > e)
        {
            orbitAngles += rotationSpeed * Time.unscaledDeltaTime * input;
            lastManualRotationTime = Time.unscaledDeltaTime;
            return true;
        }
        return false;
    }

    bool AutomaticRotation()
    {
        if (Time.unscaledTime - lastManualRotationTime < alignDelay)
        {
            return false;
        }

        Vector2 movement = new Vector2(focusPoint.x - previousFocusPoint.x, focusPoint.z - previousFocusPoint.z);

        // To save processing power we sqare it.
        // The magnitude is normally square rooted so we are sqaring it before it does this.
        float movementDeltaSqr = movement.sqrMagnitude;

        if (movementDeltaSqr < 0.0001f)
        {
            return false;
        }
        float headingAngle = GetAngle(movement / Mathf.Sqrt(movementDeltaSqr));
        float rotationChange = rotationSpeed * Time.unscaledTime;
        orbitAngles.y = Mathf.MoveTowardsAngle(orbitAngles.y, headingAngle, rotationChange);
        return true;
    }
    static public float GetAngle(Vector2 direction)
    {
        float angle = Mathf.Acos(direction.y) * Mathf.Rad2Deg;
        return direction.x < 0f ? 360 - angle : angle;
    }

    void UpdateFocusPoint()
    {
        previousFocusPoint = focusPoint;

        Vector3 targetPoint = focus.position;
        if (focusRadius > 0f)
        {
            float distance = Vector3.Distance(targetPoint, focusPoint);
            float t = 1f;


            if (distance > 0.01 && focusCentering > 0f)
            {
                t = Mathf.Pow(1f - focusCentering, Time.unscaledDeltaTime);  // <-unscaledDeltaTime always same, even if paused
            }

            if (distance > focusRadius)
            {
                // focusPoint = Vector3.Lerp(targetPoint, focusPoint, focusRadius/distance);
                t = Mathf.Min(t, focusRadius / distance);
            }
            focusPoint = Vector3.Lerp(targetPoint, focusPoint, t);
        }
        else
        {
            focusPoint = targetPoint;
        }
    }

    void ConstraingAngles()
    {
        orbitAngles.x = Mathf.Clamp(orbitAngles.x, minVerticalAngle, maxVerticalAngle);

        if (orbitAngles.y < 0f)
        {
            orbitAngles.y += 360f;
        }
        else if (orbitAngles.y > 360f)
        {
            orbitAngles.y -= 360f;
        }
    }
}
