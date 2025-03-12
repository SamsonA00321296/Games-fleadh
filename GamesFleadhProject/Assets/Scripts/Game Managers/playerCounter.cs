using UnityEngine;

namespace Game_Managers
{
    public class PlayerCounter : MonoBehaviour
    {
        public static PlayerCounter Instance { get; private set; }
        public int playersIngame;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        void OnPlayerJoined()
        {
            playersIngame++;
        }
    }
}