using UnityEngine;

public class CenterCamera : MonoBehaviour
{
    public GameObject cameraFocus;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.position = new Vector3(cameraFocus.transform.position.x, cameraFocus.transform.position.y, gameObject.transform.position.z);
    }
}
