using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;

    public Vector3 offset = new Vector3(0f, 3f, -6f);
    public float smoothSpeed = 5f;
    public bool followRotation = true;

    void LateUpdate()
    {
        if (target == null) return;

        // Calculate the desired camera position
        Vector3 desiredPosition = target.position
                                + target.rotation * offset;

        // Smoothly move the camera
        transform.position = Vector3.Lerp(
            transform.position,
            desiredPosition,
            smoothSpeed * Time.deltaTime
        );

        // Look at the player
        if (followRotation)
        {
            Quaternion targetRotation = Quaternion.LookRotation(
                target.position - transform.position
            );

            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                targetRotation,
                smoothSpeed * Time.deltaTime
            );
        }
    }
}