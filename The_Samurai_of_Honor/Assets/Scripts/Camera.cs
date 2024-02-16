using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    public Transform target;
    public float smoothSpeed = 0.125f;
    public Vector3 offset;

    public float rotationSpeed = 5.0f;
    public Vector3 angleOffset;

    void FixedUpdate()
    {
        Vector3 desiredPosition = target.position + offset;

        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

        transform.position = smoothedPosition;

        Quaternion targetRotation = Quaternion.LookRotation(target.position - transform.position);

        targetRotation.eulerAngles += angleOffset;
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }
}
