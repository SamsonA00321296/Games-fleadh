using UnityEngine;

public class ScoreNumber : MonoBehaviour
{
    public int score;
    
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
        int value = (score / level)%(levelAbove/level);
        
        sr.sprite = numbers[value];
    }
}
