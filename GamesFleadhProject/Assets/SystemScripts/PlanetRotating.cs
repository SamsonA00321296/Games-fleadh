using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetRotating : MonoBehaviour
{

    public float rotationalSpeed;
    public GameObject sunPivotObject;
    // Start is called before the first frame update
    void Start()
    {

        rotationalSpeed = Random.Range(-15.0f, -5.0f);
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.RotateAround(sunPivotObject.transform.position, Vector3.forward, rotationalSpeed * Time.deltaTime);

    }

   // void InnerMoon Planets()
    //{
    //    var position = new Vector3(Random.Range(-10.0f, 10.0f), 0, Random.Range(-10.0f, 10.0f));21
    //}
}
