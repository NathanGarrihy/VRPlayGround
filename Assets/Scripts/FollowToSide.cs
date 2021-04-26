using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowToSide : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;

    // FixedUpdate is called once per fixed update frame
    void FixedUpdate()
    {
        // Assigns an area for the Socker Interactor sphere
        transform.position = target.position + Vector3.up * offset.y +
        Vector3.ProjectOnPlane(target.right, Vector3.up).normalized * offset.x +
        Vector3.ProjectOnPlane(target.forward, Vector3.up).normalized * offset.z;

        // Makes inventory rotate with player head
        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
    }
}
