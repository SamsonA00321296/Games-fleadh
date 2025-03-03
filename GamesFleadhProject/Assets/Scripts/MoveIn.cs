using System.Collections;
using UnityEngine;

public class MoveIn : MonoBehaviour
{
    public float xInitVelocity;
    public float yInitVelocity;
    public float decrRate;
    public float decrMagnitude;
    public float intendedX;
    public float intendedY;

    public Rigidbody2D rb;

    public Bob bob;
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
            bob.bobbing = true;
            bob.bobbingRB.linearVelocity = new Vector2(0, 0.05f);
            if (transform.position.x != intendedX || transform.position.y != intendedY)
            {
                transform.position = new Vector2(intendedX, intendedY);
            }
            bob.initPos = transform.position;

        }
        
    }
    void Update()
    {
        
    }
}
