using UnityEngine;

namespace Game_Managers
{
    public class BoostBarFollower : MonoBehaviour
    {
        [Tooltip("Drag the player's transform here.")]
        public Transform playerTransform;
    
        [Tooltip("Offset from the player's position (in world space) where the boost bar should be placed.")]
        public Vector3 offset = new Vector3(0f, -1f, 0f);
    
        [Tooltip("Fixed rotation for the boost bar in Euler angles.")]
        public Vector3 fixedEulerRotation = Vector3.zero;

        void LateUpdate()
        {
            if (playerTransform)
            {
                // Follow the player's position with a fixed offset.
                transform.position = playerTransform.position + offset;
                // Lock the boost bar's rotation to the fixed value.
                transform.rotation = Quaternion.Euler(fixedEulerRotation);
            }
        }
    }
}