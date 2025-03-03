using System;
using UnityEngine;

public class ScoreSystem : MonoBehaviour
{
    public int score;

    private int value;

    public GameObject indicator;

    public String moonTag;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == moonTag)
        {
            if (other.GetComponent<ValueScript>().launched)
            {
                value = other.GetComponent<ValueScript>().value;
                score += value;
                Destroy(other.gameObject);
                GameObject ind = Instantiate(indicator, gameObject.transform.position, Quaternion.identity);
                ind.GetComponent<ScoreIndicator>().SetValue(value);
                
            }
        }
    }
}
