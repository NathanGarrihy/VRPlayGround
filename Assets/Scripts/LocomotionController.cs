using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

// Handles Teleportation Ray
public class LocomotionController : MonoBehaviour
{
    public XRController leftTeleportRay;
    public XRController rightTeleportRay;
    public InputHelpers.Button teleportActivationButton;
    public float activationThreshold = 0.1f;

    // disable teleportation if hovering over menu ui
    public XRRayInteractor leftInteractorRay;
    public XRRayInteractor rightInteractorRay;

    public bool enableRightTeleport { get; set; } = true;
    public bool enableLeftTeleport { get; set; } = true;

    // Update is called once per frame
    void Update()
    {
        // disable teleportation if hovering over menu ui
        Vector3 pos = new Vector3();
        Vector3 norm = new Vector3();
        int index = 0;
        bool validTarget = false;
        bool isLeftInteractorRayHovering;
        bool isRightInteractorRayHovering;

        // Sets teleport ray active when needed
        if (leftTeleportRay )
        {
            // 'ref' variables are changed inside the function and can be used afterwards
            isLeftInteractorRayHovering = leftInteractorRay.TryGetHitInfo(ref pos, ref norm, ref index, ref validTarget);
            leftTeleportRay.gameObject.SetActive(enableLeftTeleport && CheckIfActivatied(leftTeleportRay) && !isLeftInteractorRayHovering);        }
        if (rightTeleportRay)
        {
            isRightInteractorRayHovering = rightInteractorRay.TryGetHitInfo(ref pos, ref norm, ref index, ref validTarget);
            rightTeleportRay.gameObject.SetActive(enableRightTeleport && CheckIfActivatied(rightTeleportRay) && !isRightInteractorRayHovering);
        }
    }

    public bool CheckIfActivatied(XRController controller)
    {
        InputHelpers.IsPressed(controller.inputDevice, teleportActivationButton, out bool isActivated, activationThreshold);
        return isActivated;
    }
}
