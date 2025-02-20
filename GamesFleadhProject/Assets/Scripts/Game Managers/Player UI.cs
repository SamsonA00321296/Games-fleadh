using UnityEngine;
using UnityEngine.UI;

namespace Game_Managers
{
    public class PlayerUI : MonoBehaviour
    {
        private static PlayerUI _instance; // for the singleston to work
        
        public Image powerBar; // image of the power bar "boost" object in scene

        private void Update()
        {
            
            if (powerBar)
            {
                //just updates the images fill amount with the current power if the power bar exists
                powerBar.fillAmount = PlayerSaveData.currentPower / PlayerSaveData.MaxPower;
            }
        }
        
        //singleton method so we can switch scenes and stuff and still have ui work
        private void Awake()
        {
            if (_instance == null)
            {
                // sets this object 
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
