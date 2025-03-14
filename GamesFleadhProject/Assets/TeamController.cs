using Player_Scripts;
using UnityEngine;
using UnityEngine.InputSystem;

public class TeamController : MonoBehaviour
{

    // The arrow holder 
    public GameObject arrow;

    // Sprite renderes of arrows children
    Transform[] arrowRenderes;

    // This players player input
    PlayerInput playerInput;

    // The player index
    int playerIndex;

    // The team id
    int teamNum;

    // Teams Home Planet
    GameObject homeplanet;

    // The ship controls script
    ShipControls shipControls;

    // Is the ship for the racer gamemode?
    public bool isRacer = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    
    void Start()
    {
        playerInput = gameObject.GetComponent<PlayerInput>();
        shipControls = gameObject.GetComponent<ShipControls>();

        // Gets the players index from the playerInput attached to the ship
        playerIndex = playerInput.playerIndex;

        Debug.Log(playerIndex);

        if (playerIndex % 2 == 0)
        {
            teamNum = 0;
            homeplanet = GameObject.FindGameObjectWithTag("HomePlant");
        }
        else
        {
            teamNum = 1;
            homeplanet = GameObject.FindGameObjectWithTag("HomeRobot");
        }

        if(!isRacer)
        {
            //Gets an array of the subArrows
            arrowRenderes = arrow.GetComponentsInChildren<Transform>();
            arrowRenderes[teamNum + 1].gameObject.SetActive(false);
        }
        


            //Debug.Log(homeplanet);
            shipControls.teamID = teamNum;

        // Teleports ship to their homeplanet on creation
        gameObject.transform.position = homeplanet.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 directionToHome = (homeplanet.transform.position - transform.position);
        //Debug.DrawRay(transform.position, directionToHome);

        Vector3 normalizedDirection = directionToHome.normalized;
        // Calculate the angle between the positive Y-axis and the direction vector.
        float zRotation = Vector2.SignedAngle(Vector2.up, normalizedDirection);
        arrow.transform.rotation = Quaternion.Euler(new Vector3(0, 0, zRotation));
    }
}
