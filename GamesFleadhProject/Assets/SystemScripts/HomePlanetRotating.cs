using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HomePlanetRotating : MonoBehaviour
{

    public float orbitalRotationalSpeed;
    public Transform systemCenterPivot;
    private Vector3 planetStartingPosition;
    public Vector3 planetEndingPosition;

    // Duration of the transition between the starting and ending positions
    public float planetRotationTime = 180.0f;

    // This here keeps track of transition progress saved as a float: (0.0 to 1.0)
    private float rotationProgress = 0.0f;

    private Vector3 currentPosition;
   



    // Start is called before the first frame update
    void Start()
    {
        planetStartingPosition = transform.position;
        currentPosition = transform.position;

    }

    // Update is called once per frame
    void Update()
    {
        if ((rotationProgress < 1.0f))
        {
            rotationProgress += Time.deltaTime / planetRotationTime;
            currentPosition = Vector3.Lerp(planetStartingPosition, planetEndingPosition, rotationProgress);
        }

        //Old rotation code:
        //transform.RotateAround(sunCenterPivot.transform.position, Vector3.forward, orbitSpeed * Time.deltaTime);



        //This part here calculates the new position of the planet in a circular orbit:
        float angle = Time.time * orbitalRotationalSpeed;
        float x = Mathf.Cos(angle) * currentPosition.magnitude;
        float y = Mathf.Sin(angle) * currentPosition.magnitude;

        transform.position = systemCenterPivot.position + new Vector3(x, y, 0);

    }

}
