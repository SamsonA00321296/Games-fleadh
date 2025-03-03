using UnityEngine;

namespace Planet_stuff
{
    public class MoonFixedJointAttacher : MonoBehaviour
    {
        private FixedJoint2D _fixedJoint;
        
        public float rotationalSpeed;
        public GameObject sunPivotObject;

        private bool _isHooked;

        void Start()
        {
            // Cache the FixedJoint2D component attached to this moon prefab.
            _fixedJoint = GetComponent<FixedJoint2D>();
            rotationalSpeed = Random.Range(-15.0f, -5.0f);
        }
       

        // Update is called once per frame
        void Update()
        {
            if (!_isHooked)
            {
                transform.RotateAround(sunPivotObject.transform.position, Vector3.forward, rotationalSpeed * Time.deltaTime);
            }

        }

        void OnTriggerEnter2D(Collider2D other)
        {
            // Check if the other object has the "Hook" tag.
            if (other.CompareTag("Hook"))
            {
                _isHooked = true;
                // Get the Rigidbody2D of the object that entered the trigger.
                Rigidbody2D otherBody = other.GetComponent<Rigidbody2D>();
                InitialSpikeThrust initialSpikeThrust = other.GetComponent<InitialSpikeThrust>();

                // If a Rigidbody2D is found, assign it as the connected body of the FixedJoint2D.
                if (otherBody != null && initialSpikeThrust != null)
                {
                    if (initialSpikeThrust.hasPlanet == false)
                    {
                        _fixedJoint.connectedBody = otherBody;
                        initialSpikeThrust.hasPlanet = true;
                        //Debug.Log("Moon attached to: " + other.gameObject.name);
                    }
                    
                }
                else
                {
                    //Debug.LogWarning("No Rigidbody2D Or initial spike thrust script found on " + other.gameObject.name);
                }
            }
        }
    }
}