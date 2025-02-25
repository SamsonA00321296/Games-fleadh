using System.Collections;
using Game_Managers;
using Gravity_and_orbit_scripts;
using Un_used_scripts;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player_Scripts
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerMovement : MonoBehaviour
    {
        [Header("Movement")]
    
        public float thrustForce = 18.0f; // Force applied for thrust
        public float rotationSpeed = 250.0f; // Speed of rotation (degrees per second)
        public float boostForce = 1000.0f; // speed of trust boost

        public float boostcost = 50f; // Amount of boost needed to actually boost obviously
    
        private Rigidbody2D _rb;
        private Vector2 _movementInput;
        private float _rotationInput;
        private bool _isBoosting;
    
        private PlanetGravity _planetGravity;
    
        [Header("Scripts")]
    
        public CircularPath circularPath;
    
        [Header("References")]
    
        public PointEffector2D pointEffector;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();

            // Set initial drag and mass for the floating effect
            _rb.linearDamping = 0.5f; // Adjust drag for desired floating effect
            _rb.angularDamping = 2.0f; // Prevents excessive spinning
            
        }

        private void FixedUpdate()
        {
            if (PlayerSaveData.currentPower < 100)
            {
                PlayerSaveData.currentPower += 0.125f;
            }
            
            // Apply thrust based on movement input
            if (_movementInput.y != 0)
            {
                Vector2 force = transform.up * (_movementInput.y * thrustForce);
                _rb.AddForce(force);

                if (_isBoosting)
                {
                    Vector2 boost = transform.up * (_movementInput.y * boostForce);
                    Debug.Log("boost: " + boost);
                    _rb.AddForce(boost);
                }

            }

            // Rotate the ship based on rotation input
            if (_movementInput.x != 0)
            {
                float rotation = -_movementInput.x * rotationSpeed * Time.fixedDeltaTime;
                _rb.MoveRotation(_rb.rotation + rotation);
            }
        }

        private void OnMove(InputValue moveValue)
        {
            // Store movement input (y = thrust, x = rotation)
            _movementInput = moveValue.Get<Vector2>();
        }

        private void OnBoost(InputValue boostValue)
        {
            if (PlayerSaveData.currentPower >= boostcost)
            {
                PlayerSaveData.currentPower -= boostcost;
                _isBoosting = true;
                if (circularPath.inOrbit)
                {
                    circularPath.StopPath();
                    pointEffector.forceMagnitude = -100f;
                }
                StartCoroutine(StopBoost());
            }
        }

        IEnumerator StopBoost()
        {
            yield return new WaitForSeconds(1f);
            _isBoosting = false;
        }

        public void GoOrbit()
        {
            Debug.Log("Orbit");
            circularPath.StartPath();
            pointEffector.forceMagnitude = 0f;
        }
        
    }
}