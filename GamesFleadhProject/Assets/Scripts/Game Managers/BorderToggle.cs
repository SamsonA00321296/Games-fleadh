using System;
using UnityEngine;
using UnityEngine.UI;

namespace Game_Managers
{
    public class BorderToggle : MonoBehaviour
    {
        public Image border;
        public PlayerCounter counter;

        private void Update()
        {
            if (counter.playersIngame > 2)
            {
                border.enabled = true;
            }
            else
            {
                border.enabled = false;
            }
        }
    }
}
