using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class ShipControls : MonoBehaviour
{
    // Normalized vector representing the direction a player should be facing
    Vector2 normalizedDirection;

    // Parent Rigidbody2D and Transform
    Rigidbody2D shipRigidbody;
    Transform parentTransform;

    // Deadzone for ship movement
    public float deadZone = 0.1f;

    // Ship's thrust force and boost multiplier
    public float thrustForce = 5f;
    public float boostMultiplier = 1.5f;
    public bool canBoost = true;

    // Input actions
    InputAction flyAction;
    InputAction boostAction;

    // Expected Z rotation based on player input
    float zRotation = 0f;

    // Additional rotation offset for shaking effect
    float shakeOffset = 0f;

    // Used to disable movement when hit by a bullet
    public bool isShot;

    // Team assignment to avoid friendly fire
    public int teamID;

    // Shake parameters
    public float shakeDuration = 3f; // Shake for one second
    public float shakeMagnitude = 10f; // Maximum degrees to shake
    public float shakeFrequency = 20f; // How fast to oscillate

    void Start()
    {
        parentTransform = GetComponent<Transform>();
        shipRigidbody = GetComponent<Rigidbody2D>();

        // Find the action for flying and boosting
        flyAction = InputSystem.actions.FindAction("Fly");
        boostAction = InputSystem.actions.FindAction("BoostFly");
    }

    void Update()
    {
        // Apply the base rotation plus any shake offset.
        parentTransform.rotation = Quaternion.Euler(new Vector3(0, 0, zRotation + shakeOffset));
    }

    void FixedUpdate()
    {
        // Only apply movement forces if not shot.
        if (!isShot)
        {
            if (boostAction.IsPressed() && canBoost)
            {
                shipRigidbody.AddForce(transform.up * thrustForce * boostMultiplier);
            }
            else if (flyAction.IsPressed())
            {
                shipRigidbody.AddForce(transform.up * thrustForce);
            }
        }
    }

    // Update the ship's facing direction based on input.
    void OnMove(InputValue directionLooking)
    {
        Vector2 direction = directionLooking.Get<Vector2>();
        if (direction.magnitude > deadZone)
        {
            normalizedDirection = direction.normalized;
            // Calculate the angle between the positive Y-axis and the direction vector.
            zRotation = Vector2.SignedAngle(Vector2.up, normalizedDirection);
        }
    }

    // When colliding with a bullet, disable movement and start the shake effect.
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bullet"))
        {
            if (!isShot)
            {
                StartCoroutine(HitCooldown());
                
            }
        }
    }

    // Coroutine that disables movement for 3 seconds and shakes the ship for the first second.
    private IEnumerator HitCooldown()
    {
        isShot = true;
        float elapsed = 0f;

        // Shake the ship for shakeDuration seconds.
        while (elapsed < shakeDuration)
        {
            // Using a sine wave to oscillate the shake offset.
            shakeOffset = Mathf.Sin(elapsed * shakeFrequency) * shakeMagnitude;
            elapsed += Time.fixedDeltaTime;
            yield return new WaitForFixedUpdate();
        }
        // Reset the shake offset.
        shakeOffset = 0f;

        // Wait out the remainder of the 3-second cooldown.
        yield return new WaitForSeconds(3f - shakeDuration);
        isShot = false;
    }
}
