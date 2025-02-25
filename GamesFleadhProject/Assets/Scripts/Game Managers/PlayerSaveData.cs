using UnityEngine;

namespace Game_Managers
{
    public class PlayerSaveData : MonoBehaviour
    {
        private static PlayerSaveData _instance;

        public const float MaxPower = 100;
        public static float currentPower = 0;

        public static int score;

        private void Awake()
        {
            if (_instance == null)
            {
                _instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}
