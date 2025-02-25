using UnityEngine;
using System.Collections.Generic;

public class MoonSpawningInner : MonoBehaviour
{

    private Dictionary<string, List<Vector3>> quadrantSpawnPoints = new Dictionary<string, List<Vector3>>();
    private Dictionary<string, List<Vector3>> avaliableSpawnPoints = new Dictionary<string, List<Vector3>>();

    //Change the ints through public variables in the inspector
    public int minMoonsPerQuadrant = 3;
    public int maxMoonsPerQuadrant = 4;

    public GameObject moon;
    public GameObject GoldMoon;


    public GameObject InnerMoonsParent;
    public int moonGoldChance = 30;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //Calls both methods to initialize the spawn points and spawn the moons
        InitializeSpawnPoints();
        SpawnMoons();
    }

    void InitializeSpawnPoints()
    {
        //Four parts of the circle split into 90 degrees each so that there is more equal spread of the moons
        string[] quadrants = { "TopLeft", "TopRight", "BottomLeft", "BottomRight" };

        foreach (string quadrant in quadrants)
        {
            quadrantSpawnPoints[quadrant] = GenerateQuadrantSpawnPoints(quadrant);
            avaliableSpawnPoints[quadrant] = new List<Vector3>(quadrantSpawnPoints[quadrant]);
        }
    }

    List<Vector3> GenerateQuadrantSpawnPoints(string quadrant)
    {
        //List is saved as a list of Vector3s for easier manipulation
        List<Vector3> spawnPoints = new List<Vector3>();

        for (int i = 0; i < 12; i++)
        {
            float angle = Random.Range(0f, 90f); // Each quadrant covers 90 degrees
            float radianAngle = angle * Mathf.Deg2Rad;
            float distance = Random.Range(6.0f, 9.0f); // Ensure moons spawn within 6-9 units of the center sun(change as needed)

            // Calculate X and Y using the quadrant's range
            float x = Mathf.Cos(radianAngle) * distance;
            float y = Mathf.Sin(radianAngle) * distance;

            // Calculate the actual X and Y based on the quadrant
            if (quadrant == "TopLeft") x = -Mathf.Abs(x);
            if (quadrant == "BottomLeft") { x = -Mathf.Abs(x); y = -Mathf.Abs(y); }
            if (quadrant == "BottomRight") y = -Mathf.Abs(y);

            spawnPoints.Add(new Vector3(x, y, 0f));
        }

        return spawnPoints;
    }

    void SpawnMoons()
    {


        foreach (var quadrant in avaliableSpawnPoints.Keys)
        {
            int moonCount = Random.Range(minMoonsPerQuadrant, maxMoonsPerQuadrant + 1);

            for (int i = 0; i < moonCount; i++)
            {
                if (avaliableSpawnPoints[quadrant].Count == 0) break;

                // Selects a random spawn point from the list(Quadrant) of available spawn points;
                int index = Random.Range(0, avaliableSpawnPoints[quadrant].Count);
                Vector3 spawnPosition = avaliableSpawnPoints[quadrant][index];

                //Removes the spawn point from the list so it can't be used again
                avaliableSpawnPoints[quadrant].RemoveAt(index);

                //Spawnas a gold moon if the random number is less than the moonGoldChance
                GameObject moonToSpawn = (Random.Range(0, 100) < moonGoldChance) ? GoldMoon : moon;

                // Instantiate Moons underneath the parent object (This is set as a public gameobject)

                Instantiate(moonToSpawn, spawnPosition, Quaternion.identity, InnerMoonsParent.transform);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}