using UnityEngine;

namespace Game_Managers
{
    public class FixedRotation : MonoBehaviour
    {
        // Set this to the fixed rotation you want in Euler angles (e.g., 0,0,0).
        public Vector3 fixedEulerRotation = Vector3.zero;
    
        void LateUpdate()
        {
            // Override the boost bar’s global rotation.
            transform.rotation = Quaternion.Euler(fixedEulerRotation);
        }
    }
}