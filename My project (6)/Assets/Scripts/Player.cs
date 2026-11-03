using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    private Vector2 movement;
    private Rigidbody r;
    private Collider playerCollider;

    private float jumpBoostExpiresAt;

    [SerializeField] private float speed = 5f;
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private float boostedJumpForce = 10f;

    private void Start()
    {
        r = GetComponent<Rigidbody>();
        playerCollider = GetComponent<Collider>();
    }

    private void Update()
    {
        UpdateMovement();
    }

    private void FixedUpdate()
    {
        Vector3 moveDirection = GetMovementDirection();

        Vector3 velocity = r.linearVelocity;
        velocity.x = moveDirection.x * speed;
        velocity.z = moveDirection.z * speed;

        r.linearVelocity = velocity;
    }

    private void UpdateMovement()
    {
        movement = Vector2.zero;

        if (Keyboard.current != null)
        {
            if (Keyboard.current.wKey.isPressed ||
                Keyboard.current.upArrowKey.isPressed)
                movement.y += 1f;

            if (Keyboard.current.sKey.isPressed ||
                Keyboard.current.downArrowKey.isPressed)
                movement.y -= 1f;

            if (Keyboard.current.dKey.isPressed ||
                Keyboard.current.rightArrowKey.isPressed)
                movement.x += 1f;

            if (Keyboard.current.aKey.isPressed ||
                Keyboard.current.leftArrowKey.isPressed)
                movement.x -= 1f;
        }

        movement = Vector2.ClampMagnitude(movement, 1f);
    }

    private Vector3 GetMovementDirection()
    {
        Transform cam = Camera.main != null
            ? Camera.main.transform
            : transform;

        Vector3 forward = cam.forward;
        Vector3 right = cam.right;

        forward.y = 0f;
        right.y = 0f;

        forward.Normalize();
        right.Normalize();

        return (forward * movement.y + right * movement.x).normalized
            * movement.magnitude;
    }

    public void OnJump(InputValue value)
    {
        if (!value.isPressed || !IsGrounded())
            return;

        float force = Time.time < jumpBoostExpiresAt
            ? boostedJumpForce
            : jumpForce;

        if (r.linearVelocity.y < 0f)
        {
            r.linearVelocity = new Vector3(
                r.linearVelocity.x, 0f, r.linearVelocity.z);
        }

        r.AddForce(Vector3.up * force, ForceMode.Impulse);
    }

    public void ActivateJumpBoost(float duration)
    {
        if (Time.time >= jumpBoostExpiresAt)
        {
            jumpBoostExpiresAt =
                Time.time + Mathf.Clamp(duration, 0f, 10f);
        }
    }

    private bool IsGrounded()
    {
        return Physics.Raycast(
            playerCollider.bounds.center,
            Vector3.down,
            playerCollider.bounds.extents.y + 0.1f,
            Physics.DefaultRaycastLayers,
            QueryTriggerInteraction.Ignore);
    }
}