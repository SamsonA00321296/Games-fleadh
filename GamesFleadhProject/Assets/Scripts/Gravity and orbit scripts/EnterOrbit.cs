using Player_Scripts;
using UnityEngine;

namespace Gravity_and_orbit_scripts
{
    public class EnterOrbit : MonoBehaviour
    {
    
        [Header("Scripts")]
    
        public PlayerMovement player; // reference to the player movement script
    
        [Header("references")] 
    
        public GameObject target; // refernce to the target, should be the player ship

        // starts the orbit once player collides with the center of gravity
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.name.Equals(target.name))
            {
                player.GoOrbit();
            }
        }
    
    }
}
