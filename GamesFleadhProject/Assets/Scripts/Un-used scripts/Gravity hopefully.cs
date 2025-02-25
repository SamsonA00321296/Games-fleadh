using UnityEngine;

namespace Un_used_scripts
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlanetGravity : MonoBehaviour
    {
        [Header("Gravity Settings")]
        public float gravitationalConstant = 1.0f; // Controls the strength of gravity
        public float gravityFalloff = 0.5f; // Adjusts how quickly gravity weakens with distance
        public float minimumDistanceThreshold = 0.1f; // Minimum distance to avoid excessive force
        public float orbitSpeed = 2.0f; // Speed of orbit when above a planet

        private Rigidbody2D _rb;
        private GameObject[] _planets;
        private Rigidbody2D[] _planetRigidbodies;

        private bool _isInputActive = true; // Tracks if input is active

        private void Start()
        {
            _planets = GameObject.FindGameObjectsWithTag("Planets");
            _planetRigidbodies = new Rigidbody2D[_planets.Length];

            for (int i = 0; i < _planets.Length; i++)
            {
                _planetRigidbodies[i] = _planets[i].GetComponent<Rigidbody2D>();
            }
        }

        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
        }

        private void FixedUpdate()
        {
            if (_planets.Length == 0)
                return;

            // Calculate gravitational force from each planet
            Vector2 totalForce = Vector2.zero;
            GameObject closestPlanet = null;
            float closestDistanceSquared = float.MaxValue;

            for (int i = 0; i < _planets.Length; i++)
            {
                Rigidbody2D planetRb = _planetRigidbodies[i];
                if (!planetRb)
                    continue;

                Vector2 directionToPlanet = _planets[i].transform.position - transform.position;
                float distanceSquared = directionToPlanet.sqrMagnitude;

                if (distanceSquared < closestDistanceSquared)
                {
                    closestDistanceSquared = distanceSquared;
                    closestPlanet = _planets[i];
                }

                if (distanceSquared > minimumDistanceThreshold * minimumDistanceThreshold) // Avoid excessive force when too close
                {
                    float distance = Mathf.Sqrt(distanceSquared);
                    float gravityStrength = gravitationalConstant * planetRb.mass / Mathf.Pow(distance, gravityFalloff);
                    totalForce += directionToPlanet.normalized * gravityStrength;
                }
                else
                {
                    // Stop applying force if within the minimum threshold
                    _rb.linearVelocity = Vector2.zero;
                    return;
                }
            }

            // Apply the gravitational force to the ship if input is active
            if (_isInputActive)
            {
                _rb.AddForce(totalForce);
            }
            else if (closestPlanet)
            {
                // Orbit logic when input is not active
                Vector2 directionToPlanet = closestPlanet.transform.position - transform.position;
                Vector2 tangent = new Vector2(-directionToPlanet.y, directionToPlanet.x).normalized;
                _rb.linearVelocity = tangent * orbitSpeed;
            }
        }

        public void SetInputActive(bool isActive)
        {
            _isInputActive = isActive;
        }
    }
}
