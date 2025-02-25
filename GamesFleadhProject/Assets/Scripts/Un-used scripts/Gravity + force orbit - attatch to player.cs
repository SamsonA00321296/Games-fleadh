using UnityEngine;

namespace Un_used_scripts
{
    public class Gravity : MonoBehaviour
    {
        [Header("Gravity Settings")]
        public Transform planet;                  // Reference to the planet
        public float gravitationalConstant = 10f; // Strength of gravity
        public float minimumDistance = 2f;        // Safe radius to prevent trapping at the center
        public float captureRadius = 5f;          // Radius where orbit stabilization begins
        public float transitionZoneWidth = 2f;    // Width of the transition zone
        public float tangentialForceMultiplier = 0.3f; // Strength of tangential force adjustment
        public float radialDamping = 0.9f;        // Damping factor for radial velocity
        public float repulsionForce = 5f;         // Radial repulsion force near the minimum distance

        private Rigidbody2D _rb;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
        }

        private void FixedUpdate()
        {
            if (!planet) return;

            // Calculate direction and distance to the planet
            Vector2 directionToPlanet = (Vector2)planet.position - _rb.position;
            float distanceToPlanet = directionToPlanet.magnitude;

            // Apply gravitational force
            ApplyGravity(directionToPlanet, distanceToPlanet);

            // Stabilize orbit if within the capture radius
            if (distanceToPlanet <= captureRadius)
            {
                StabilizeOrbit(directionToPlanet, distanceToPlanet);
            }

            // Apply repulsion force if too close to the planet
            if (distanceToPlanet <= minimumDistance)
            {
                ApplyRepulsion(directionToPlanet);
            }
        }

        private void ApplyGravity(Vector2 directionToPlanet, float distanceToPlanet)
        {
            // Prevent extreme forces when too close
            float safeDistance = Mathf.Max(distanceToPlanet, minimumDistance);

            // Normalize the direction
            Vector2 gravityDirection = directionToPlanet.normalized;

            // Calculate gravitational force magnitude
            float gravityForceMagnitude = gravitationalConstant / (safeDistance * safeDistance);

            // Apply force toward the planet
            _rb.AddForce(gravityDirection * gravityForceMagnitude, ForceMode2D.Force);
        }

        private void ApplyRepulsion(Vector2 directionToPlanet)
        {
            // Normalize the direction
            Vector2 repulsionDirection = -directionToPlanet.normalized;

            // Apply a radial repulsion force
            _rb.AddForce(repulsionDirection * repulsionForce, ForceMode2D.Force);
        }

        private void StabilizeOrbit(Vector2 directionToPlanet, float distanceToPlanet)
        {
            // Normalize the direction to the planet
            Vector2 gravityDirection = directionToPlanet.normalized;

            // Decompose velocity into radial and tangential components
            Vector2 radialVelocity = Vector2.Dot(_rb.linearVelocity, gravityDirection) * gravityDirection;
            Vector2 tangentialVelocity = _rb.linearVelocity - radialVelocity;

            // Calculate the target orbital speed
            float orbitalSpeed = Mathf.Sqrt(gravitationalConstant / Mathf.Max(distanceToPlanet, minimumDistance));

            // Compute transition influence based on distance (smooth ramp within transition zone)
            float influenceFactor = Mathf.InverseLerp(captureRadius, captureRadius - transitionZoneWidth, distanceToPlanet);

            // Apply a tangential force to nudge the velocity toward the desired orbital speed
            Vector2 targetTangentialDirection = Vector2.Perpendicular(gravityDirection).normalized;
            Vector2 tangentialForce = targetTangentialDirection * ((orbitalSpeed - tangentialVelocity.magnitude) * tangentialForceMultiplier * influenceFactor);
            _rb.AddForce(tangentialForce, ForceMode2D.Force);

            // Gradually reduce radial velocity to stabilize the orbit
            radialVelocity *= radialDamping;
            _rb.linearVelocity = radialVelocity + tangentialVelocity;
        }

        // Debug visualization for the capture and transition zones
        private void OnDrawGizmos()
        {
            if (planet != null)
            {
                Gizmos.color = Color.blue;
                Gizmos.DrawWireSphere(planet.position, captureRadius); // Capture radius
                Gizmos.color = Color.cyan;
                Gizmos.DrawWireSphere(planet.position, captureRadius - transitionZoneWidth); // Transition zone
                Gizmos.color = Color.green;
                Gizmos.DrawWireSphere(planet.position, minimumDistance); // Minimum distance
            }
        }
    }
}
