
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public Vector3 offset = new Vector3(0f, 4f, -7f);
    public float smoothSpeed = 8f;
    public Vector3 lookOffset = new Vector3(0f, 1.5f, 0f);

    void LateUpdate()
    {
        if (target == null) return;

        // Follow the player using a fixed world-space offset
        Vector3 targetPosition = target.position + offset;

        transform.position = Vector3.Lerp(
            transform.position,
            targetPosition,
            1f - Mathf.Exp(-smoothSpeed * Time.deltaTime)
        );

        // Look at the player without orbiting around them
        Vector3 lookPosition = target.position + lookOffset;
        Vector3 direction = lookPosition - transform.position;

        if (direction.sqrMagnitude > 0.001f)
        {
            transform.rotation = Quaternion.LookRotation(
                direction,
                Vector3.up
            );
        }
    }
}
