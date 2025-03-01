using System.Collections;
using UnityEngine;

public class ShipSpikeControls : MonoBehaviour
{
    // How long before the player can shoot/reel in the spike

    public float chainCooldown = 0.5f;

    public float chainRetractSpeed = 0;

    // Spike & chain prefab
    public GameObject SpikePrefab;

    // Is there currently a spike?
    bool spikeOut = false;
    bool canSpike = true;

    // Parent Gameobject
    GameObject parent;

    // Spike and chain gameobject
    GameObject spikeAndChain;

    // Ship GameObject
    GameObject ship;
    // Ship Transform
    Transform shipTransform;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        parent = gameObject.transform.parent.gameObject;
        ship = parent.transform.GetChild(0).gameObject;
        shipTransform = ship.transform;


    }

    // Update is called once per frame
    void Update()
    {
        // checks if the spike should be out, and if there is a spike
        if (!spikeOut && spikeAndChain != null)
        {
            // Moves each chain piece towards the ships center
            foreach (Transform child in spikeAndChain.transform)
            {
                child.position = Vector3.MoveTowards(child.transform.position, shipTransform.position, (chainRetractSpeed * Time.deltaTime));
            }
        }
    }

    void OnSpike()
    {
        // If the player can spike, create spike prefab object
        if (canSpike)
        {
            Instantiate(SpikePrefab, gameObject.transform.parent);
            spikeAndChain = parent.transform.GetChild(1).gameObject;

            spikeOut = true;
            canSpike = false;
        }
        // If not, check if the spike is out. If so queue deletion and start retracting
        else if (spikeOut)
        {
            spikeOut = false;
            StartCoroutine(DestroySpike());
        }
        
    }

    // Destroys the spike and chain after chain-cooldown time
    IEnumerator DestroySpike()
    {
        yield return new WaitForSeconds(chainCooldown);
        canSpike = true;
        Destroy(spikeAndChain);
    }


}
