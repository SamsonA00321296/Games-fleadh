using System.Collections;
using UnityEngine;

public class SwitchBoostDirection : MonoBehaviour
{
    float delay;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        string currentTime = System.DateTime.Now.ToString();

        string lastDigit = currentTime.Substring(currentTime.Length - 1, 1);

        float lastNumber = float.Parse(lastDigit);

        lastNumber /= 2;
        lastNumber += 2;

        Debug.Log(lastNumber);

        //Debug.Log(lastDigit);

        StartCoroutine(SwitchDirection(lastNumber));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator SwitchDirection(float delay)
    {
        yield return new WaitForSeconds(delay);

        transform.Rotate(new Vector3(0,0,180));
        StartCoroutine(SwitchDirection(delay));
    }
}
