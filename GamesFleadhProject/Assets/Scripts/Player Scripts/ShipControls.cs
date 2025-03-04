using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace Player_Scripts
{
    public class ShipControls : MonoBehaviour
    {
        // Normalized vector representing the direction a player should be facing.
        Vector2 _normalizedDirection;

        // Parent Rigidbody2D and Transform.
        Rigidbody2D _shipRigidbody;
        Transform _parentTransform;

        // Deadzone for ship movement.
        public float deadZone = 0.1f;

        // Ship's thrust force and boost multiplier.
        public float thrustForce = 5f;
        public float boostMultiplier = 1.5f;
        public bool canBoost = true;

        // Input actions.
        InputAction _flyAction;
        InputAction _boostAction;

        // Expected Z rotation based on player input.
        float _zRotation;

        // Additional rotation offset for shaking effect.
        float _shakeOffset;

        // Used to disable movement when hit by a bullet.
        public bool isShot;

        // To save if the players are flying/boosting
        bool isFlying = false;
        bool isBoosting = false;

        // Team assignment to avoid friendly fire.
        public int teamID;

        // Shake parameters.
        public float shakeDuration = 3f; // Shake for three seconds.
        public float shakeMagnitude = 10f; // Maximum degrees to shake.
        public float shakeFrequency = 20f; // How fast to oscillate.

        // --- Boost Meter Variables ---
        [Header("Boost Settings")]
        public float maxBoostTime = 5f;       // Maximum boost capacity (seconds).
        public float boostRegenRate = 0.5f;   // Boost regeneration rate per second.

        float _boostRemaining;    // Current boost energy.
        float _boostHoldTime;     // How long boost button has been held continuously.

        // --- UI Element for Boost Bar ---
        [Header("Boost UI")]
        public Image boostBarImage;

        // --- Boost Particle System ---
        [Header("Boost Particle System")]
        // Reference to a ParticleSystem that plays when boosting.
        public ParticleSystem boostParticles;

        void Start()
        {
            _parentTransform = transform;
            _shipRigidbody = GetComponent<Rigidbody2D>();

            // Find the input actions.
            _flyAction = InputSystem.actions.FindAction("Fly");
            _boostAction = InputSystem.actions.FindAction("BoostFly");

            // Initialize boost values.
            _boostRemaining = maxBoostTime;
            _boostHoldTime = 0f;

            // Ensure boost particles are not playing initially.
            if (boostParticles != null && boostParticles.isPlaying)
                boostParticles.Stop();
        }

        void Update()
        {
            // Ensure that boostRemaining does not exceed maxBoostTime.
            _boostRemaining = Mathf.Min(_boostRemaining, maxBoostTime);

            // Update the boost bar UI.
            if (boostBarImage)
            {
                boostBarImage.fillAmount = _boostRemaining / maxBoostTime;
            }

            // Apply the base rotation plus any shake offset.
            _parentTransform.rotation = Quaternion.Euler(new Vector3(0, 0, _zRotation + _shakeOffset));
        }

        void FixedUpdate()
        {
            // Only apply movement forces if not shot.
            if (!isShot)
            {
                // Check if boost is active.
                if (isBoosting && canBoost && _boostRemaining > 0)
                {
                    //Debug.Log("is boosting");
                    // Increase how long the boost button has been held.
                    _boostHoldTime += Time.fixedDeltaTime;
                    // Drain rate increases the longer boost is held.
                    float drainRate = 1f + _boostHoldTime;
                    _boostRemaining -= drainRate * Time.fixedDeltaTime;
                    if (_boostRemaining <= 0)
                    {
                        _boostRemaining = 0;
                        canBoost = false;
                    }
                
                    // Apply boost force.
                    _shipRigidbody.AddForce(transform.up * (thrustForce * boostMultiplier));
                
                    // Start the particle effect if not already playing.
                    if (boostParticles != null && !boostParticles.isPlaying)
                    {
                        boostParticles.Play();
                    }
                }
                else
                {
                    // Stop the boost particle effect when not boosting.
                    if (boostParticles != null && boostParticles.isPlaying)
                    {
                        boostParticles.Stop();
                    }
                
                    // Reset the boost hold timer when not boosting.
                    _boostHoldTime = 0f;
                
                    // Apply normal thrust if the fly action is pressed.
                    if (isFlying)
                    {
                        _shipRigidbody.AddForce(transform.up * thrustForce);
                    }
                }

                // Regenerate boost when boost button is not held.
                if (!_boostAction.IsPressed())
                {
                    _boostRemaining = Mathf.Min(_boostRemaining + boostRegenRate * Time.fixedDeltaTime, maxBoostTime);
                    if (_boostRemaining > 0)
                        canBoost = true;
                }
            }
        }

        // Update the ship's facing direction based on input.
        void OnMove(InputValue directionLooking)
        {
            Vector2 direction = directionLooking.Get<Vector2>();
            if (direction.magnitude > deadZone)
            {
                _normalizedDirection = direction.normalized;
                // Calculate the angle between the positive Y-axis and the direction vector.
                _zRotation = Vector2.SignedAngle(Vector2.up, _normalizedDirection);
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

        // Coroutine that disables movement for 3 seconds and shakes the ship for the first shakeDuration seconds.
        private IEnumerator HitCooldown()
        {
            isShot = true;
            float elapsed = 0f;

            // Shake the ship for shakeDuration seconds.
            while (elapsed < shakeDuration)
            {
                // Oscillate the shake offset with a sine wave.
                _shakeOffset = Mathf.Sin(elapsed * shakeFrequency) * shakeMagnitude;
                elapsed += Time.fixedDeltaTime;
                yield return new WaitForFixedUpdate();
            }
            // Reset the shake offset.
            _shakeOffset = 0f;

            // Wait out the remainder of the 3-second cooldown.
            yield return new WaitForSeconds(3f - shakeDuration);
            isShot = false;
        }

        // VERY JANKY
        // toggles between the ship flying and the ship not flying
        // everytime the fly button is pressed & depressed
        void OnFly(InputValue buttonPressed)
        {
            if(!isFlying)
            {
                isFlying = true;
            }
            else
            {
                isFlying = false;
            }
        }

        // also janky but still works???
        void OnBoostFly()
        {
            if(!isBoosting)
            {
                isBoosting = true;
            }
            else
            {
                isBoosting = false;
            }
        }
    }
}
