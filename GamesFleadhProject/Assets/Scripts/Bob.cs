using System;
using System.Collections;
using UnityEngine;

public class Bob : MonoBehaviour
{
    public bool bobbing = false;
    public Vector3 initPos;

    public float bobbingSpeed;
    
    
    public Rigidbody2D bobbingRB;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
        StartCoroutine(Bobbing());
    }

    IEnumerator Bobbing()
    {
        
        if (bobbing)
        {
            if (bobbingRB.position.y <= initPos.y)
            {
                bobbingRB.linearVelocity = new Vector2(bobbingRB.linearVelocity.x, bobbingRB.linearVelocity.y+(bobbingSpeed*0.01f));
            }
            else if (bobbingRB.position.y >= initPos.y)
            {
                bobbingRB.linearVelocity = new Vector2(bobbingRB.linearVelocity.x, bobbingRB.linearVelocity.y-(bobbingSpeed*0.01f));
            }

            if (bobbingRB.linearVelocity.y > bobbingSpeed * 0.1f)
            {
                bobbingRB.linearVelocity = new Vector2(bobbingRB.linearVelocity.x, bobbingSpeed * 0.1f);
            }
            else if (bobbingRB.linearVelocity.y < bobbingSpeed * -0.1f)
            {
                bobbingRB.linearVelocity = new Vector2(bobbingRB.linearVelocity.x, bobbingSpeed * -0.1f);
            }
        }
        yield return new WaitForSeconds(0.1f);
        StartCoroutine(Bobbing());
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
