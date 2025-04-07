using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
    public float spawnInterval;
    public float minSpawnInterval = 2.0f;
    public float maxSpawnInterval = 5.0f;
    public GameObject asteroidPrefab;
    public Transform asteroidParent;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spawnInterval = Random.Range(minSpawnInterval, maxSpawnInterval);
    }

    // Update is called once per frame
    void Update()
    {
        if (spawnInterval > 0)
        {
            spawnInterval -= Time.deltaTime;
        }
        else
        {
            SpawnAsteroid();
            spawnInterval = Random.Range(minSpawnInterval, maxSpawnInterval);
        }
    }

    public void SpawnAsteroid()
    {
        // Determine the Y position, either +5 or -5 relative to the parent spawner
        float yOffset = (Random.value < 0.5f) ? 5.0f : -5.0f;
        Vector3 spawnPosition = new Vector3(transform.position.x, transform.position.y + yOffset, 0);

        GameObject asteroid = Instantiate(asteroidPrefab, spawnPosition, Quaternion.identity, asteroidParent);
        Rigidbody2D rb = asteroid.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.AddForce(new Vector2(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f)).normalized * Random.Range(5.0f, 10.0f), ForceMode2D.Impulse);
        }
    }
}
