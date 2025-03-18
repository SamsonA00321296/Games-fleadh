using UnityEngine;
using UnityEngine.InputSystem;

namespace Game_Managers
{
    public class PlayerCounter : MonoBehaviour
    {
        public int playersIngame;
        
        [Header("Press A to Join UI")]
        [SerializeField] private GameObject[] pressAObjects;

        [Header("Player Sprites")]
        [SerializeField] private GameObject[] playerSprites;

        private void Awake()
        {
            // Initialize UI states
            for (int i = 0; i < pressAObjects.Length; i++)
            {
                if (pressAObjects[i] != null) 
                    pressAObjects[i].SetActive(true);
            }
            for (int i = 0; i < playerSprites.Length; i++)
            {
                if (playerSprites[i] != null) 
                    playerSprites[i].SetActive(false);
            }
        }

        void OnPlayerJoined(PlayerInput playerInput)
        {
            playersIngame++; // Increment total players

            int index = playerInput.playerIndex; 
            // The first player is index 0, second is 1, etc.

            // Hide "Press A to Join" for that index
            if (index < pressAObjects.Length && pressAObjects[index] != null)
            {
                pressAObjects[index].SetActive(false);
            }

            // Show the corresponding player sprite
            if (index < playerSprites.Length && playerSprites[index] != null)
            {
                playerSprites[index].SetActive(true);
            }
        }
    }
}