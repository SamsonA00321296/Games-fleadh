using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinOnHit : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    // is the game finished
    bool gameFinished = false;

    // The wining teams index
    int winnerTeamNum = -1;

    // Timer GameObject
    public RaceTimer raceTimer;
    public DisplayTeam displayTeam;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && !gameFinished)
        {
            gameFinished = true;
            TeamController teamController = collision.gameObject.GetComponent<TeamController>();
            winnerTeamNum = teamController.teamNum;


            DisplayWinner(winnerTeamNum);
        }
    }

    void DisplayWinner(int teamNum)
    {
        raceTimer.timerActive = false;
        raceTimer.EndRace();
        displayTeam.DisplayWinner(teamNum);
        StartCoroutine(GoToTitle());
    }

    IEnumerator GoToTitle()
    {
        yield return new WaitForSeconds(5f);
        SceneManager.LoadScene("IntermidiateScene");
    }
}
