using UnityEngine;
using UnityEngine.UI;

namespace Game_Managers
{
    public class BorderToggle : MonoBehaviour
    {
        [SerializeField] private Image border;
        [SerializeField] private PlayerCounter counter;

        [SerializeField] private Image p3;
        [SerializeField] private Image p4;

        private void Update()
        {
            if (counter.playersIngame > 4)
            {
                border.enabled = true;
                if (counter.playersIngame == 6)
                {
                    p3.enabled = true;
                    p4.enabled = false;
                }
                else
                {
                    p3.enabled = true;
                    p4.enabled = true;
                }
            }
            else
            {
                border.enabled = false;
                p3.enabled = false;
                p4.enabled = false;
            }
        }
    }
}
