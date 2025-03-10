using System;
using Planet_stuff;
using Player_Scripts;
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

                MoonFixedJointAttacher moonFixedJointAttacher = other.GetComponent<MoonFixedJointAttacher>();
                GameObject spike = moonFixedJointAttacher.spikeAttached;

                InitialSpikeThrust initialSpikeThrust = spike.GetComponent<InitialSpikeThrust>();
                initialSpikeThrust.hasPlanet = false;

                GameObject spikeParentShip = initialSpikeThrust.parentShip;
                ShipSpikeControls shipSpikeControls = spikeParentShip.GetComponent<ShipSpikeControls>();
                shipSpikeControls.spikeOut = false;
                shipSpikeControls.StartCoroutine(shipSpikeControls.DestroySpike());

                //NEED to disconect moon from spike before scoring
                Destroy(other.gameObject);
                GameObject ind = Instantiate(indicator, gameObject.transform.position, Quaternion.identity);
                ind.GetComponent<ScoreIndicator>().SetValue(value);
                
            }
        }
    }
}
