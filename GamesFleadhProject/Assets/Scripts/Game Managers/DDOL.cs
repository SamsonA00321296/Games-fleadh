using UnityEngine;

namespace Game_Managers
{
    public class DDOL : MonoBehaviour
    {
        

        private void Awake()
        {
            
                DontDestroyOnLoad(gameObject);
        }
    }
}