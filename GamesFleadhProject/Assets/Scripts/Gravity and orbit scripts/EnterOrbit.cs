using Player_Scripts;
using UnityEngine;

namespace Gravity_and_orbit_scripts
{
    public class EnterOrbit : MonoBehaviour
    {
    
        [Header("Scripts")]
    
        public PlayerMovement player;
    
        [Header("references")] 
    
        public GameObject target;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.name.Equals(target.name))
            {
                player.GoOrbit();
            }
        }
    
    }
}
