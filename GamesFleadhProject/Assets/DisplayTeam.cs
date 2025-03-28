using UnityEngine;
using UnityEngine.UI;

public class DisplayTeam : MonoBehaviour
{
    Animator winAnimator;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public Sprite[] winImages;
    void Start()
    {
        winAnimator = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DisplayWinner (int winnerIndex)
    {
        gameObject.GetComponent<Image>().sprite = winImages[winnerIndex];
        winAnimator.SetTrigger("Win");
    }
}
