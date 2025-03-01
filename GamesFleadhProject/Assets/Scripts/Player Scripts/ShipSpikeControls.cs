using System.Collections;
using UnityEngine;

public class ShipSpikeControls : MonoBehaviour
{
    // How long before the player can shoot/reel in the spike

    public float chainCooldown = 0.5f;

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
        
    }

    private void FixedUpdate()
    {
        if (!spikeOut && spikeAndChain != null)
        {
            foreach (Transform child in spikeAndChain.transform)
            {
                child.position = Vector3.MoveTowards(spikeAndChain.transform.position, shipTransform.position, 0.1f * Time.deltaTime);
            }
        }
    }

    void OnSpike()
    {
        if (canSpike)
        {
            Instantiate(SpikePrefab, gameObject.transform.parent);
            spikeAndChain = parent.transform.GetChild(1).gameObject;

            spikeOut = true;
            canSpike = false;
        }
        else if (spikeOut)
        {
            spikeOut = false;
            StartCoroutine(DestroySpike());
        }
        
    }
    IEnumerator DestroySpike()
    {
        yield return new WaitForSeconds(chainCooldown);
        canSpike = true;
        Destroy(spikeAndChain);
    }


}
