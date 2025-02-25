using System.Collections;
using Game_Managers;
using Gravity_and_orbit_scripts;
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
        public float boostForce = 1000.0f; // speed of thrust boost

        public float boostcost = 50f; // Amount of boost needed to actually boost obviously
    
        private Rigidbody2D _rb; //player rigid body
        private Vector2 _movementInput; // movement input vector for moving and thigns. w and s input
        private float _rotationInput; // rotation input for turning, a and d input
        private bool _isBoosting; // bool to apply boost force
    
        [Header("Scripts")]
    
        //public CircularPath circularPath; // a reference to the circular path script, the one used for the manual player orbit
    
        [Header("References")]
    
        public PointEffector2D pointEffector; // the planets point effector for gravity, can be ignored if not using the circluar path script

        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();

          // i havent played with these values too much so knock yourself out tryna find better settings if you want
            _rb.linearDamping = 0.5f; // Adjust drag for the floaty kinda effect when the ship moves
            _rb.angularDamping = 2.0f; // Prevents excessive spinning when the ship tries to turn
            
        }

        private void FixedUpdate()
        {
            if (PlayerSaveData.currentPower < 100)
            {
                //increases power if bar is less than 100
                PlayerSaveData.currentPower += 0.125f;
            }
            
            // Apply thrust based on movement input
            if (_movementInput.y != 0)
            {
                //adds a force to the rigid body to move the player
                Vector2 force = transform.up * (_movementInput.y * thrustForce);
                _rb.AddForce(force);
                
                // if the player is boosting then add a greater force to the player then the above code
                if (_isBoosting)
                {
                    Vector2 boost = transform.up * (_movementInput.y * boostForce);
                    Debug.Log("boost: " + boost);
                    _rb.AddForce(boost);
                }

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

        private void OnBoost(InputValue boostValue)
        {
            
            //checks if you have enough boost too boost obv and then takes away the cost and sets the variable to true so the force can be applied above
            if (PlayerSaveData.currentPower >= boostcost)
            {
                PlayerSaveData.currentPower -= boostcost;
                _isBoosting = true;
                
                /*
                //this basically lets you  break out of the orbit started in the circluar path script and will turn back on the gravity
                if (circularPath && pointEffector)
                {
                    if (circularPath.inOrbit)
                    {
                        circularPath.StopPath();
                        pointEffector.forceMagnitude = -100f;
                    }
                }
                */
                // starts a timer to turn the isboosting variable back off
                StartCoroutine(StopBoost());
            }
        }

        IEnumerator StopBoost()
        {
            // stops boosing after a second, make a float variable for this if you care, i dont
            yield return new WaitForSeconds(1f);
            _isBoosting = false;
        }

        /*
        public void GoOrbit()
        {
            // this basically just starts the orbit from circular path, its only here because i was testing it initially on a keybind, can be moved if you care, i dont
            if (circularPath)
            {
                Debug.Log("Orbit");
                circularPath.StartPath();
                pointEffector.forceMagnitude = 0f;
            }
        }
        */
        
    }
}