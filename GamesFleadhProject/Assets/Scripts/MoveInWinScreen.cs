using System.Collections;
using UnityEngine;

public class MoveInWinScreen : MonoBehaviour
{
    public float xInitVelocity;
    public float yInitVelocity;
    public float decrRate;
    public float decrMagnitude;

    public Rigidbody2D rb;
    public WinnerDecide decide;
    
    public SpriteRenderer sr;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb.linearVelocity = new Vector2(xInitVelocity, yInitVelocity);
        StartCoroutine(DecreaseSpeed());
    }

    // Update is called once per frame
    IEnumerator DecreaseSpeed()
    {
        if (rb.linearVelocity.magnitude != 0)
        {
            rb.linearVelocity *= 1-decrMagnitude;
            yield return new WaitForSeconds(decrRate);
            if (rb.linearVelocity.magnitude < 0.05 && rb.linearVelocity.magnitude > -0.05)
            {
                rb.linearVelocity *= 0;
            }
            StartCoroutine(DecreaseSpeed());
        }
        else
        {
            decide.ShowScores();
        }
        
    }
    void Update()
    {
        
    }

    public void SetStats(Sprite winner, WinnerDecide decide)
    {
        sr.sprite = winner;
        this.decide = decide;
    }
}
