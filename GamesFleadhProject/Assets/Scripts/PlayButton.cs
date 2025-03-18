using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

// If you're using a UI Text element

public class PlayButton : MonoBehaviour
{
    [Tooltip("The name of the scene to load.")]
    public string sceneToLoad;

    [Tooltip("Warning UI element to notify the player (disable by default).")]
    public GameObject warningText;

    public void ButtonClicked() 
    {
        StartCoroutine(Play());
    }

    IEnumerator Play()
    {
        // If we're trying to load the MoonHaulers scene, check for at least 2 placeholders.
        if (sceneToLoad.Equals("MoonHaulers", StringComparison.OrdinalIgnoreCase))
        {
            GameObject[] placeholders = GameObject.FindGameObjectsWithTag("Placeholder");
            if (placeholders.Length < 2)
            {
                // Show a warning that you must have two or more players.
                if (warningText)
                {
                    warningText.SetActive(true);
                }
                else
                {
                    Debug.Log("You must have two or more players!");
                }
                
                // Wait for 1.5 seconds.
                yield return new WaitForSeconds(1.5f);
                
                // Hide the warning text.
                if (warningText)
                {
                    warningText.SetActive(false);
                }
                
                yield break; // Do not continue to load the scene.
            }
        }

        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene(sceneToLoad);
    }
}