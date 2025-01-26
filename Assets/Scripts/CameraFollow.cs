using UnityEngine;

public class CameraFollow : MonoBehaviour
{  public Transform target;
    public float distance = 10f;
    public float height = 5f;
    public float positionSmoothSpeed = 0.1f;
    public float rotationSmoothSpeed = 2f;

    private Vector3 currentVelocity;

    void LateUpdate()
    {
        if (target == null) return;
        Vector3 desiredPosition = target.position - target.forward * distance + Vector3.up * height;
        Vector3 smoothedPosition = Vector3.SmoothDamp(transform.position, desiredPosition, ref currentVelocity, positionSmoothSpeed);
        transform.position = smoothedPosition;
        Quaternion targetRotation = Quaternion.LookRotation(target.position + Vector3.up * 2f - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSmoothSpeed);
    }
}