using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinnerDecide : MonoBehaviour
{
    public ScoreSystem planet1;
    public ScoreSystem planet2;
    public TimerScript timer;

    public int extraTime;
    public GameObject scoreboard;
    public GameObject winScreen;
    public Sprite[] winSprites;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DecideWinner()
    {
        if (planet1.score > planet2.score)
        {
            Debug.Log("Player 1 wins!");
            Destroy(timer.gameObject);
            GameObject winMessage = Instantiate(winScreen, new Vector3(transform.position.x, transform.position.y+5, transform.position.z), Quaternion.identity);
            winMessage.GetComponent<MoveInWinScreen>().SetStats(winSprites[0], this);
        }
        else if (planet2.score > planet1.score)
        {
            Debug.Log("Player 2 wins!");
            Destroy(timer.gameObject);
            GameObject winMessage = Instantiate(winScreen, new Vector3(0, 5, 0), Quaternion.identity);
            winMessage.GetComponent<MoveInWinScreen>().SetStats(winSprites[1], this);
        }
        else
        {
            timer.timer += extraTime;
        }
    }

    public void ShowScores()
    {
        GameObject scores = Instantiate(scoreboard, transform.position, Quaternion.identity);
        scores.GetComponent<ScoreTransfer>().TransferScore(planet1.score, planet2.score);
        StartCoroutine(EndDelay());
    }

    IEnumerator EndDelay()
    {
        yield return new WaitForSeconds(10f);
        SceneManager.LoadScene("TitleScreen");
    }
}
