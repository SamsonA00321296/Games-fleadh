using System.Collections;
using System.Threading;
using UnityEngine;

public class TimerScript : MonoBehaviour
{
    public int timer;
    public bool active = true;

    public WinnerDecide scorekeeper;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(Countdown());
    }

    // Update is called once per frame
    void Update()
    {
        if (timer > 0 && !active)
        {
            active = true;
            StartCoroutine(Countdown());
        }
    }

    IEnumerator Countdown()
    {
        if (timer > 0)
        {
            yield return new WaitForSeconds(1);
            timer--;
            StartCoroutine(Countdown());
        }
        else
        {
            active = false;
            scorekeeper.DecideWinner();
        }
        
    }
}
