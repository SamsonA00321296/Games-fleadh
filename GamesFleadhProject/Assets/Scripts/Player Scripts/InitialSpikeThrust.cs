using UnityEngine;

public class InitialSpikeThrust : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public float thrustStrenght = 0f;

    // Multiplies into thrustStrenght to calculate kickback
    public float kickbackMultiplier = 1f;

    // Objects RigidBody
    Rigidbody2D spikeRigidbody;

    // Spikes parent (chain + spike holder)
    GameObject parentGameObject;

    // Hinges on the parent object
    HingeJoint2D[] parentHinges;

    

    // Spikes parent ship
    GameObject parentShip;

    // parent ships rigidbody
    Rigidbody2D parentShipRigidbody;

    //HingeJoint2D shipHindge;
    void Start()
    {
        spikeRigidbody = gameObject.GetComponent<Rigidbody2D>();

        // Assing the parent gameobject
        parentGameObject = this.transform.parent.gameObject;
        // And its hinges
        parentHinges = parentGameObject.GetComponents<HingeJoint2D>();

        //Assign the parent ship object
        // spike -> grabber full -> Ship & spike holder -> ship
        GameObject parentHolder = this.transform.parent.gameObject.transform.parent.gameObject;
        parentShip = parentHolder.transform.GetChild(0).gameObject;

        
        
        parentShipRigidbody = parentShip.GetComponent<Rigidbody2D>();

        parentHinges[1].connectedBody = parentShipRigidbody;

        // Sets chains position to the ships position
        parentGameObject.transform.position = parentShip.transform.position;
        //Followed by rotation
        parentGameObject.transform.rotation = parentShip.transform.rotation;
        // And finally its linearVelocity
        spikeRigidbody.linearVelocity = parentShipRigidbody.linearVelocity;

        spikeRigidbody.AddForce(transform.up * thrustStrenght * 1f, ForceMode2D.Impulse) ;

        // Apply kickback to the parent ship
        parentShip.GetComponent<Rigidbody2D>().AddForce(parentShip.transform.up * thrustStrenght * -kickbackMultiplier, ForceMode2D.Impulse);
        
    }

    // Update is called once per frame
    void Update()
    {
        //spikeRigidbody.linearVelocity = transform.up * thrustStrenght;
    }
}
