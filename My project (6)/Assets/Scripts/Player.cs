using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    Vector2 movement;
    Rigidbody r;
    Collider playerCollider;
    float jumpBoostExpiresAt;

    public float acceleration;

    [SerializeField] float jumpForce = 5f;
    [SerializeField] float boostedJumpForce = 10f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        r = GetComponent<Rigidbody>();
        playerCollider = GetComponent<Collider>();
    }

    // Update is called once per frame
    void Update()
    {
        r.AddForce(Vector3.forward * movement.y * acceleration);
    }

    void OnMove(InputValue value)
    {
        movement = value.Get<Vector2>();
    }

    void OnJump(InputValue value)
    {
        if (!value.isPressed || !IsGrounded())
        {
            return;
        }

        float force = Time.time < jumpBoostExpiresAt ? boostedJumpForce : jumpForce;
        r.AddForce(Vector3.up * force, ForceMode.Impulse);
    }

    public void ActivateJumpBoost(float duration)
    {
        if (Time.time >= jumpBoostExpiresAt)
        {
            jumpBoostExpiresAt = Time.time + Mathf.Clamp(duration, 0f, 10f);
        }
    }

    bool IsGrounded()
    {
        return Physics.Raycast(
            playerCollider.bounds.center,
            Vector3.down,
            playerCollider.bounds.extents.y + 0.1f,
            Physics.DefaultRaycastLayers,
            QueryTriggerInteraction.Ignore);
    }
}
