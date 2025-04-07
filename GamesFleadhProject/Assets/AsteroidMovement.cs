using UnityEngine;

public class AsteroidMovement : MonoBehaviour
{
    private Transform centerObject;
    public float forceMagnitude;
    public Sprite Asteroid2;
    public Sprite Asteroid3;
    public Sprite Asteroid4;
    public Sprite Asteroid1;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        GameObject sunObject = GameObject.FindWithTag("Sun");
        if (sunObject != null)
        {
            centerObject = sunObject.transform;
        }
        else
        {
            Debug.LogError("Center object with tag 'Sun' not found.");
            return;
        }
        forceMagnitude = Random.Range(0.5f, 10.0f);
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        SpriteRenderer sr = GetComponent<SpriteRenderer>();

        if (rb != null && centerObject != null)
        {
            // Calculate the direction vector from the asteroid to the central object.
            Vector2 directionToCenter = (centerObject.position - transform.position).normalized;

            // Calculate a perpendicular vector to create an orbiting motion.
            Vector2 tangentialDirection = new Vector2(-directionToCenter.y, directionToCenter.x);

            // Combine the directions to set the initial velocity.
            rb.linearVelocity = (directionToCenter + tangentialDirection).normalized * forceMagnitude;

            // Set the sprite based on the forceMagnitude
            if (forceMagnitude < 2.5f)
            {
                sr.sprite = Asteroid1;
            }
            else if (forceMagnitude < 5.0f)
            {
                sr.sprite = Asteroid2;
            }
            else if (forceMagnitude < 7.5f)
            {
                sr.sprite = Asteroid3;
            }
            else
            {
                sr.sprite = Asteroid4;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Sun"))
        {
            //// Spawn explosion
            //if (explosionPrefab != null)
            //{
            //    Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            //}

            // Grow the sun
            sunGrowingScript sunGrowth = other.GetComponent<sunGrowingScript>();
            if (sunGrowth != null)
            {
                sunGrowth.Grow();
            }

            Destroy(gameObject); // Destroy the asteroid
        }

        else if(other.CompareTag("BlackHole"))
        {
            // Spawn explosion
            //if (explosionPrefab != null)
            //{
            //    Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            //}
            // Destroy the asteroid
            Destroy(gameObject);
        }
    }
}
