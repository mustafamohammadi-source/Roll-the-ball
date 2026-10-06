using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    Vector2 movement;
    Rigidbody r;

    public float acceleration;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        r = GetComponent<Rigidbody>();
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
}
