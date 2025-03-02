using UnityEngine;
using UnityEngine.InputSystem;

namespace Player_Scripts
{
    public class LaserShooter : MonoBehaviour
    {
        // The bullet prefab to instantiate.
        public GameObject bulletPrefab;

        // Two spawn points from which bullets are fired.
        public Transform bulletSpawnPoint1;
        public Transform bulletSpawnPoint2;

        // Speed at which the bullet will travel.
        public float bulletSpeed = 20f;

        // Index to alternate between spawn points (0 or 1).
        private int _spawnIndex = 0;

        

        // Instantiates the bullet from one of the spawn points and applies velocity.
        void OnShoot()
        {
            Transform chosenSpawnPoint = (_spawnIndex == 0) ? bulletSpawnPoint1 : bulletSpawnPoint2;
        
            // Instantiate the bullet at the chosen spawn point's position and rotation.
            GameObject bullet = Instantiate(bulletPrefab, chosenSpawnPoint.position, chosenSpawnPoint.rotation);
        
            // Get the Rigidbody2D component from the instantiated bullet.
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            if (rb)
            {
                // Set the bullet's velocity in the direction the chosen spawn point is facing.
                rb.linearVelocity = chosenSpawnPoint.up * bulletSpeed;
            }
            else
            {
                Debug.LogWarning("The bullet prefab is missing a Rigidbody2D component.");
            }

            // Alternate the spawn index for the next shot.
            _spawnIndex = (_spawnIndex == 0) ? 1 : 0;
        }
    }
}