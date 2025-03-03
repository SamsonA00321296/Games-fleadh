using UnityEngine;

namespace Player_Scripts
{
    public class DestroyBullet : MonoBehaviour
    {
        // Automatically destroy the bullet after 3 seconds.
        void Start()
        {
            Destroy(gameObject, 3f);
        }

        // If the bullet collides with any object, destroy it.
        private void OnCollisionEnter2D(Collision2D collision)
        {
            Destroy(gameObject);
        }

        // If you're using trigger colliders, you can also include this method.
        private void OnTriggerEnter2D(Collider2D other)
        {
            Destroy(gameObject);
        }
    }
}