using UnityEngine;

namespace Game_Managers
{
    public class PlayerSaveData : MonoBehaviour
    {
        private static PlayerSaveData _instance; // for the singleton to work

        public const float MaxPower = 100; // the maximum amount of power in the bar, dont make this some stupid number it will prob break something
        public static float currentPower = 0; // the current amount of power

        public static int score; // unused currently but will be used to track the score

        //singleton function, makes sure it isnt destroyed, and is the only instance of itsled in the scene, same as the UI one, could i have made them together? probably. but oh well
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
