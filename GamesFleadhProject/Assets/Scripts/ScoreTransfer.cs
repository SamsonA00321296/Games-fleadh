using UnityEngine;

public class ScoreTransfer : MonoBehaviour
{
    public ScoreNumber t1num1;
    public ScoreNumber t1num2;
    
    public ScoreNumber t2num1;
    public ScoreNumber t2num2;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TransferScore(int score1, int score2)
    {
        t1num1.score = score1;
        t1num2.score = score1;
        
        t2num1.score = score2;
        t2num2.score = score2;
    }
}
