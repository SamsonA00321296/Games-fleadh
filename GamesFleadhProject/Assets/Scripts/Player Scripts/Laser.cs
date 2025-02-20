using UnityEngine;

namespace Player_Scripts
{
    public class Laser : MonoBehaviour
    {
        [Header("Laser")] 
        public float upTime = 3f;

        private void Awake()
        {
            Destroy(gameObject, upTime);
        }

        void OnCollisionEnter2D(Collision2D collision)
        {
            Destroy(gameObject);
        }
    }
}
