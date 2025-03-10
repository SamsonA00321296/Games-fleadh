using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class testMove : MonoBehaviour
{
    private Vector2 _movementInput;

    private Rigidbody2D _rb;
    
    public float thrustForce = 18.0f;
    public float rotationSpeed = 250.0f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _rb = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (_movementInput.y != 0)
        {
            //adds a force to the rigid body to move the player
            Vector2 force = transform.up * (_movementInput.y * thrustForce);
            _rb.AddForce(force);

        }

        // Rotate the ship based on the a or d input
        if (_movementInput.x != 0)
        {
            float rotation = -_movementInput.x * rotationSpeed * Time.fixedDeltaTime;
            _rb.MoveRotation(_rb.rotation + rotation);
        }
    }
    
    private void OnMove(InputValue moveValue)
    {
        // Store movement input (y is the w and s inputs, so the thrust,  x is the a and d inputs, so the rotation)
        _movementInput = moveValue.Get<Vector2>();
    }
    
    
}
