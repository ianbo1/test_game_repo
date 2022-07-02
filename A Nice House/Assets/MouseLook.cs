using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public float mouseSensitivity = 1f; // Degrees per second per mouse axis movement
    public Transform playerBodyTr;      // Reference to the player body transform

    float verticalLookAngle = 0f; // The rate of vertical rotation

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; // Don't show and lock the mouse position on the screen
    }

    void Update()
    {
        // Gather mouse input
        float mouseXInput = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime; // Horizontal component of the mouse's velocity
        float mouseYInput = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime; // Vertical component of the mouse's velocity

        // Handle horizontal camera motion
        playerBodyTr.Rotate(Vector3.up * mouseXInput); // Rotate player transform left and right according to horizontal mouse movement

        // Handle vertical camera motion
        verticalLookAngle -= mouseYInput;                              // Modify the look angle according to the mouse input
        verticalLookAngle = Mathf.Clamp(verticalLookAngle, -90f, 90f); // Clamp the look angle to 90 degrees up and down

        transform.localRotation = Quaternion.Euler(verticalLookAngle, 0, 0); // Set the camera's transform rotation to verticalLookAngle along the x-axis. 'local' means relative to the 
    }
}
