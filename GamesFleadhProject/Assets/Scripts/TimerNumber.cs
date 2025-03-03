using UnityEngine;

public class TimerNumber : MonoBehaviour
{
    public TimerScript timerScript;
    
    public Sprite[] numbers;

    public SpriteRenderer sr;

    public int level;

    public int levelAbove;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        int value = (timerScript.timer / level)%(levelAbove/level);
        
        sr.sprite = numbers[value];
    }
}
