using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

// Script to move player to desired location when climbing a wall
public class Climber : MonoBehaviour
{
    private CharacterController character;
    public static XRController climbingHand; // access climbingHand without making reference to script
    private ContiniousMovement continiousMovement;

    // Start is called before the first frame update
    void Start()
    {
        character = GetComponent<CharacterController>();
        continiousMovement = GetComponent<ContiniousMovement>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(climbingHand)
        {
            continiousMovement.enabled = false;
            Climb();
        }
        else
        {
            continiousMovement.enabled = true;
        }
    }

    void Climb()
    {
        // Apply opposite velocity of the hand to the player
        // Similar to what happens in real life when climbing!
        InputDevices.GetDeviceAtXRNode(climbingHand.controllerNode).TryGetFeatureValue(CommonUsages.deviceVelocity, out Vector3 velocity);

        // Takes initial rotation of player into account
        character.Move(transform.rotation * -velocity * Time.fixedDeltaTime);
    }
}
