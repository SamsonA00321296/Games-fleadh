using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Game_Managers
{
    public class MoonHaulersSpawner : MonoBehaviour
    {
        [Header("Actual Player Prefab")]
        [Tooltip("This prefab should include the camera, controls, etc.")]
        [SerializeField] private GameObject actualPlayerPrefab;

        [Header("Spawn Points (Optional)")]
        [Tooltip("Assign empty GameObjects as spawn points. If not set, players keep their placeholder position.")]
        [SerializeField] private Transform[] spawnPoints;

        private void Start()
        {
            // Find all placeholder objects (spawned in the Lobby) by tag.
            GameObject[] placeholderObjects = GameObject.FindGameObjectsWithTag("Placeholder");
            int totalPlayers = placeholderObjects.Length;
            Debug.Log("[MoonHaulersSpawner] Found " + totalPlayers + " placeholder(s).");

            List<PlayerInput> newPlayers = new List<PlayerInput>();

            // For each placeholder, spawn an actual player prefab.
            for (int i = 0; i < totalPlayers; i++)
            {
                GameObject placeholder = placeholderObjects[i];
                PlayerInput placeholderInput = placeholder.GetComponent<PlayerInput>();

                if (placeholderInput == null)
                {
                    Debug.LogWarning("Placeholder does not have a PlayerInput component: " + placeholder.name);
                    continue;
                }

                // Get the device used by this placeholder (if any).
                InputDevice device = (placeholderInput.devices.Count > 0) ? placeholderInput.devices[0] : null;

                // Determine spawn point: if you have assigned spawn points, use the corresponding one; else use the placeholder's position.
                Transform spawn;
                if (spawnPoints != null && spawnPoints.Length > i)
                {
                    spawn = spawnPoints[i];
                }
                else
                {
                    spawn = placeholder.transform;
                }

                // Instantiate the actual player prefab using the new Input System.
                PlayerInput newPlayer = PlayerInput.Instantiate(
                    actualPlayerPrefab,
                    playerIndex: placeholderInput.playerIndex,
                    controlScheme: null,
                    pairWithDevice: device
                );

                // Set the position and rotation to that of the spawn point.
                newPlayer.transform.position = spawn.position;
                newPlayer.transform.rotation = spawn.rotation;
                newPlayers.Add(newPlayer);

                // Destroy the placeholder object.
                Destroy(placeholder);
            }

            // Set up splitscreen for the new players.
            SetupSplitScreen(newPlayers);
        }

        /// <summary>
        /// Adjusts each spawned player's camera viewport to create a splitscreen effect.
        /// Supports 2 to 4 players.
        /// </summary>
        private void SetupSplitScreen(List<PlayerInput> players)
        {
            int count = players.Count;

            for (int i = 0; i < count; i++)
            {
                Camera cam = players[i].GetComponentInChildren<Camera>();
                if (cam == null)
                {
                    Debug.LogWarning("Player " + i + " has no Camera component.");
                    continue;
                }

                Rect rect = new Rect(0, 0, 1, 1); // default full screen

                if (count == 2)
                {
                    // Two-player splitscreen: left and right halves.
                    rect.width = 0.5f;
                    rect.height = 1f;
                    rect.x = i * 0.5f;
                    rect.y = 0;
                }
                else if (count == 3)
                {
                    // Three-player splitscreen: two on top, one full-width on bottom.
                    if (i < 2)
                    {
                        rect.width = 0.5f;
                        rect.height = 0.5f;
                        rect.x = i * 0.5f;
                        rect.y = 0.5f;
                    }
                    else
                    {
                        rect.width = 1f;
                        rect.height = 0.5f;
                        rect.x = 0;
                        rect.y = 0;
                    }
                }
                else if (count >= 4)
                {
                    // Four-player splitscreen: 2x2 grid.
                    rect.width = 0.5f;
                    rect.height = 0.5f;
                    int row = i / 2;
                    int col = i % 2;
                    rect.x = col * 0.5f;
                    // In Unity, (0,0) is bottom left; so we set y accordingly.
                    rect.y = 1 - (row + 1) * 0.5f;
                }

                cam.rect = rect;
                Debug.Log($"Set splitscreen for player {i} with viewport rect {rect}");
            }
        }
    }
}
