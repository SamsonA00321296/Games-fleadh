using Player_Scripts;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.InputSystem;

public class RaceTimer : MonoBehaviour
{
    public GameObject num1;
    public GameObject num2;
    public GameObject num3;

    public PlayerInputManager playerInputManager;

    public Sprite[] numberSprites;

    public float seconds = 5;
    private bool countingDown = true;

    public bool timerActive = false;
    public bool gameStarted = false;

    private GameObject[] playerList;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (timerActive)
        {
            if (countingDown)
            {
                // Add new players to the list
                playerList = GameObject.FindGameObjectsWithTag("Player");

                // Decrement Timer
                seconds -= Time.deltaTime;

                // If timer = 0, start counting up and start race
                if (seconds <= 0)
                {
                    seconds = 0;
                    countingDown = false;

                    StartRace();
                }


            }
            else
            {
                seconds += Time.deltaTime;
            }

            convertSecondsToDisplay(seconds);
        }
        else
        {
            // Add new players to the list
            playerList = GameObject.FindGameObjectsWithTag("Player");

            if (playerList.Length != 0 && ! gameStarted)
            {
                timerActive = true;
                gameStarted = true;
                
            }

            
        }

        convertSecondsToDisplay(seconds);


    }

    void convertSecondsToDisplay(float seconds)
    {
        int secondsInt = (int)seconds;

        int minutes = secondsInt / 60;
        secondsInt = secondsInt % 60;

        if (minutes <= 9)
        {
            num3.GetComponent<SpriteRenderer>().sprite = numberSprites[minutes];
        }

        num1.GetComponent<SpriteRenderer>().sprite = numberSprites[secondsInt/10];

        num2.GetComponent<SpriteRenderer>().sprite = numberSprites[secondsInt % 10];
    }


    void StartRace()
    {
        Debug.Log("Starting Race!");

        Debug.Log(playerList.Length);

        playerInputManager.DisableJoining();

        foreach (GameObject player in playerList)
        {
            Debug.Log("Giving Player Thrust");

            ShipControls shipControls = player.GetComponent<ShipControls>();
            shipControls.thrustForce = 30;
        }
    }

    public void EndRace()
    {
        foreach (GameObject player in playerList)
        {
            Debug.Log("Revoking Player Thrust");

            ShipControls shipControls = player.GetComponent<ShipControls>();
            shipControls.thrustForce = 0;
        }
    }
}
