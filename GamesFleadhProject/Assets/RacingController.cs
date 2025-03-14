using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class RacingController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public float waitTillStart;

    public GameObject inputControllerManager;
    void Start()
    {
        StartCoroutine(StartGame(waitTillStart));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator StartGame(float delay)
    {
        yield return new WaitForSeconds(delay);
        PlayerInputManager playerInputManager = inputControllerManager.GetComponent<PlayerInputManager>();
        playerInputManager.DisableJoining();
    }
}
