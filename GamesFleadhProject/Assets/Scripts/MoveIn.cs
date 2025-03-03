using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public class MoveIn : MonoBehaviour
{
    public float intendedX;
    public float intendedY;

    public Rigidbody2D rb;
    public bool stopped = false;
    public Vector2 calcDirection;

    public Bob bob;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        calcDirection = (new Vector2(intendedX, intendedY) - rb.position);
        
    }

    // Update is called once per frame
        
    
    void Update()
    {
        if (new Vector2(intendedX, intendedY) != rb.position)
        {
            calcDirection = (new Vector2(intendedX, intendedY) - rb.position);
            gameObject.transform.Translate(calcDirection * Time.deltaTime);
        }
        else if (!stopped)
        {
            bob.bobbing = true;
            bob.bobbingRB.linearVelocity = new Vector2(0, 0.05f);
            if (transform.position.x != intendedX || transform.position.y != intendedY)
            {
                transform.position = new Vector2(intendedX, intendedY);
            }
            bob.initPos = transform.position;
            stopped = true;
        }
    }
}
