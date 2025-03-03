using UnityEngine;

namespace Planet_stuff
{
    public class PropelTowardsCenter : MonoBehaviour
    {
        // Assign the central object (target) in the inspector.
        public Transform centerObject;
    
        // Adjust the force magnitude as needed.
        public float forceMagnitude = 10f;

        private void OnTriggerEnter2D(Collider2D other)
        {
            // Check if the colliding object is the player.
            if (other.CompareTag("Player"))
            {
                // Get the Rigidbody2D from the player.
                Rigidbody2D rb = other.GetComponent<Rigidbody2D>();
                if (rb != null && centerObject != null)
                {
                    // Calculate the direction vector from the player to the central object.
                    Vector2 direction = (centerObject.position - other.transform.position).normalized;
                    // Apply an impulse force to propel the player toward the center.
                    rb.AddForce(direction * forceMagnitude, ForceMode2D.Impulse);
                }
            }
        }
    }
}