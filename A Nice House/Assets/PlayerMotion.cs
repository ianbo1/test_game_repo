using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMotion : MonoBehaviour {
    public float walkSpeed;           // Walking speed in m/s
    public float gravitationalAcc;    // Acceleration due to gravity in m/s^2
    public float maxSprintMultiplier; // Maximum multiplier that modifies player speed when sprinting
    public float jumpHeight;           // vertical speed of the player as they leave the ground

    private float verticalSpeed;            // Vertical component of the velocity. Notice that it can be negative (downwards) despite being a "speed"
    private CharacterController controller; // The player's character controller component
    private float jumpSpeed;                // The speed of the players jump. This is calculated using jumpHeight

    void Start() {
        controller = gameObject.GetComponent<CharacterController>(); // Connect the controller variable to the character controller component of the player
        jumpSpeed = Mathf.Sqrt(2f * gravitationalAcc * jumpHeight);  // Calculate the speed for the jump, given the height we want to reach
    }

    void Update() {
        // Collect inputs from the user
        float forwardInput = Input.GetAxis("Vertical");    // Get the forward/backward input
        float sidewaysInput = Input.GetAxis("Horizontal"); // Get the left/right input
        float jumpInput = Input.GetAxis("Jump");           // Get the jump input (space)
        float sprintInput = Input.GetAxis("Sprint");       // Get sprint input (shift)

        // Calculate the player velocity
        Vector3 velocity = Vector3.Normalize(forwardInput * transform.forward + sidewaysInput * transform.right) * (sprintInput * maxSprintMultiplier + (1 - sprintInput)) * walkSpeed; // Turn inputs into a normalized "walking direction" vector, and multiply by walk speed

        if (controller.isGrounded) {                             // If the player is touching the ground
            if (jumpInput == 1) verticalSpeed = jumpSpeed;       // If the user is jumping, set the vertical speed to the jump speed
            else verticalSpeed = 0;                              // Otherwise set the vertical speed to zero
        }
        else verticalSpeed -= gravitationalAcc * Time.deltaTime; // Otherwise, decrease the vertical speed according to gravity

        velocity += verticalSpeed * transform.up; // Add the vertical component to the velocity

        // Move the player
        controller.Move(velocity * Time.deltaTime); // Move the controller by the given distance
    }
}
