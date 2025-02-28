using UnityEngine;

public class InitialSpikeThrust : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public float thrustStrenght = 0f;

    // Objects RigidBody
    Rigidbody2D spikeRigidbody;

    // Spikes parent (chain + spike holder)
    GameObject parentGameObject;

    // Spikes parent ship
    GameObject parentShip;
    HingeJoint2D shipHindge;
    void Start()
    {
        spikeRigidbody = gameObject.GetComponent<Rigidbody2D>();

        parentGameObject = this.transform.parent.gameObject;

        // spike -> grabber full -> ship
        parentShip = this.transform.parent.gameObject.transform.parent.gameObject;

        // Ships hindge
        shipHindge = parentShip.GetComponent<HingeJoint2D>();
        shipHindge.connectedBody = parentGameObject.GetComponent<Rigidbody2D>();

        // Sets chains position to the ships position
        parentGameObject.transform.position = parentShip.transform.position;
        //Followed by rotation
        parentGameObject.transform.rotation = parentShip.transform.rotation;


        spikeRigidbody.linearVelocity = transform.up * thrustStrenght * 10;
        parentShip.GetComponent<Rigidbody2D>().linearVelocity = transform.up * thrustStrenght * -0.1f;
        
    }

    // Update is called once per frame
    void Update()
    {
        //spikeRigidbody.linearVelocity = transform.up * thrustStrenght;
    }
}
