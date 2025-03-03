using UnityEngine;

public class ScoreIndicator : MonoBehaviour
{
    private Rigidbody2D rb;
    public SpriteRenderer sr;
    public float moveSpeed;
    public float fadeSpeed;
    public Sprite[] sprites;
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        rb.linearVelocity = new Vector2(0, moveSpeed);
        sr = gameObject.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, sr.color.a - fadeSpeed * Time.deltaTime);
        if (sr.color.a <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void SetValue(int value)
    {
        if (value <= 3)
        {
            sr.sprite = sprites[value-1];
        }
        else if (value == 5)
        {
            sr.sprite = sprites[3];
        }
    }
}
