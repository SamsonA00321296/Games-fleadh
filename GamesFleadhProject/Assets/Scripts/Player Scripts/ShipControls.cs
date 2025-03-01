using UnityEngine;
using UnityEngine.InputSystem;

public class ShipControls : MonoBehaviour
{


    // Normalized vector repersenting the direction a player should be facing
    Vector2 normalizedDirection;

    // Parent rigidbody
    Rigidbody2D shipRigidbody;

    // Parent Transform
    Transform parentTransform;

    // Deadzone for ship movement
    public float deadZone = 0.1f;

    // Ships thrustForce
    public float thrustForce = 5;

    // Boost Mulitplier
    public float boostMultiplier = 1.5f;
    public bool canBoost = true;

    // Used to hold various actions
    InputAction flyAction;
    InputAction boostAction;

    // Exspected Z rotation of the ship
    float zRotation = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        parentTransform = gameObject.GetComponent<Transform>();
        shipRigidbody = gameObject.GetComponent<Rigidbody2D>();

        // Find the action for flying and boosting
        flyAction = InputSystem.actions.FindAction("Fly");
        boostAction = InputSystem.actions.FindAction("BoostFly");
    }

    // Update is called once per frame
    void Update()
    {

        parentTransform.rotation = (Quaternion.Euler(new Vector3(0,0,zRotation)));
    }

    private void FixedUpdate()
    {
        if (boostAction.IsPressed() && canBoost)
        {
            shipRigidbody.AddForce(transform.up * thrustForce * boostMultiplier);
        }
        else if (flyAction.IsPressed())
        {
            shipRigidbody.AddForce(transform.up * thrustForce);
            //Debug.Log("FLYING!");
        }
    }

    // Yes ik it would make more sense to use OnLook
    void OnMove(InputValue directionLooking)
    {

        // Only pull & process the direction looking vector whenever its magnetude is greater then 0.1
        // this reperensent the joystick being moved a miniscule amount
        // This avoids issues with the joystick boundcing back, and doesnt screw up the ships rotation
        // when joystick is idle

        if (directionLooking.Get<Vector2>().magnitude > deadZone)
        {
            normalizedDirection = directionLooking.Get<Vector2>().normalized;

            // Finds the angle between positive y-axis, and vector representing where the player is looking
            zRotation = Vector2.SignedAngle(new Vector2(0, 1), normalizedDirection);

            //Debug.Log(normalizedDirection);
        }
    }
}
