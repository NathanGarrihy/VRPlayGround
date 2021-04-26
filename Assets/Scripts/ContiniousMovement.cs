using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class ContiniousMovement : MonoBehaviour
{
    // Variables for player movement
    public float startingSpeed = 2;
    private float speed;
    public XRNode inputSource;
    public float gravity = -9.81f;
    public LayerMask groundLayer;
    public float additionalHeight = 0.2f;

    private float fallingSpeed;
    private Vector2 inputAxis;
    private CharacterController character;
    private XRRig rig;
    private bool isGrounded;
    private bool isSprinting;

    // Start is called before the first frame update
    void Start()
    {
        character = GetComponent<CharacterController>();
        rig = GetComponent<XRRig>();
        speed = startingSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        // Checks for device input
        InputDevice device = InputDevices.GetDeviceAtXRNode(inputSource);
        device.TryGetFeatureValue(CommonUsages.primary2DAxis, out inputAxis);
        device.TryGetFeatureValue(CommonUsages.primary2DAxisClick, out isSprinting);

    }

    private void FixedUpdate()
    {
        CapsuleFollowHeadset();
        // Get direction head is facing
        Quaternion headYaw = Quaternion.Euler(0, rig.cameraGameObject.transform.eulerAngles.y, 0);
        // Moves player based off device input
        Vector3 direction = headYaw* new Vector3(inputAxis.x, 0, inputAxis.y);



        // Check for sprint option
        if(isSprinting)
        {
            speed = startingSpeed * 2;
        }
        else
        {
            speed = startingSpeed;
        }

        character.Move(direction * speed * Time.fixedDeltaTime);

        // Gravity
        isGrounded = CheckIfGrounded();
        if (isGrounded)
            fallingSpeed = 0;
        else
            fallingSpeed += gravity * Time.fixedDeltaTime;

        character.Move(Vector3.up * fallingSpeed * Time.fixedDeltaTime);
    }

    // Makes camera follow when moving without using touch pad (moving head)
    void CapsuleFollowHeadset()
    {
        character.height = rig.cameraInRigSpaceHeight + additionalHeight;
        Vector3 capsuleCenter = transform.InverseTransformPoint(rig.cameraGameObject.transform.position);
        character.center = new Vector3(capsuleCenter.x, character.height / 2 + character.skinWidth, capsuleCenter.z);
    }

    bool CheckIfGrounded()
    {
        // checks if player is on the ground
        // Spherecast is a raycast with certain thickness
        // Ensures better accuracy around edges of platforms
        Vector3 rayStart = transform.TransformPoint(character.center);
        float rayLength = character.center.y + 0.01f;
        bool hasHit = Physics.SphereCast(rayStart, character.radius, Vector3.down, out RaycastHit hitInfo, rayLength, groundLayer);
        return hasHit;
    }
}
