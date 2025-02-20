using Game_Managers;
using UnityEngine;


namespace Player_Scripts
{
    public class PlayerShoot : MonoBehaviour
    {
        [Header("Gun elements")]
        public Transform laserSpawnPoint;
        public GameObject laserPrefab;
        
        [Header("laser elements")]
        public float laserSpeed = 50;
        public float laserCost = 10;

        private void OnAttack()
        {
            if (PlayerSaveData.currentPower >= laserCost)
            {
                PlayerSaveData.currentPower -= laserCost;
                var laser = Instantiate(laserPrefab, laserSpawnPoint.position, laserSpawnPoint.rotation);
                laser.GetComponent<Rigidbody2D>().linearVelocity = laserSpawnPoint.up * laserSpeed;
            }
        }
    }
}
