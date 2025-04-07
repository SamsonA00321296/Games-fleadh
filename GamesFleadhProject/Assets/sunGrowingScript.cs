using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class sunGrowingScript : MonoBehaviour
{
    public float growthSpeed = 0.1f;
    public float maxSize = 6.0f;
    public GameObject blackHole;
    public bool hasReachedMaxSize = false;

    // Black hole collapse
    public float collapseDuration = 1.5f;
    public float finalScale = 0.1f;
    public float maxSpinSpeed = 1000f;

    private Rigidbody2D rb;

    // UI Elements
    public Slider SunProgressionSlider;
    private Image fillImage;
    //Sun visual updating part:
    private SpriteRenderer spriteRenderer;
    public TextMeshProUGUI[] colorChangingTexts; //An array of colour texts to change colour (this is amazing btw wow)

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        if (SunProgressionSlider != null)
        {
            fillImage = SunProgressionSlider.fillRect.GetComponent<Image>();

        }
        spriteRenderer = GetComponent<SpriteRenderer>();
        Grow();
    }

    void Update()
    {
        // Optional: Pulse slider fill when sun is near max size
        if (SunProgressionSlider != null && fillImage != null)
        {
            float currentScale = transform.localScale.x;
            float t = Mathf.InverseLerp(0f, maxSize, currentScale);

            if (t > 0.9f)
            {
                float pulse = Mathf.Sin(Time.time * 10f) * 0.1f + 1f;
                fillImage.transform.localScale = Vector3.one * pulse;
            }
            else
            {
                fillImage.transform.localScale = Vector3.one; // reset scale
            }
        }
    }

    public void Grow()
    {
        if (hasReachedMaxSize) return;

        Vector3 newScale = transform.localScale + Vector3.one * growthSpeed;

        if (newScale.x <= maxSize)
        {
            transform.localScale = newScale;
        }
        else
        {
            transform.localScale = Vector3.one * maxSize;
            collapseIntoBlackHole();
        }

        // Update slider visuals and sun colourin:
        if (SunProgressionSlider == null || fillImage == null) return;

        float scale = transform.localScale.x;
        float t = Mathf.InverseLerp(0f, maxSize, scale);

        Color newColor = GetStarColorByProgress(t);
        fillImage.color = newColor;
        SunProgressionSlider.value = scale;
        //Sun colour updater
        if (spriteRenderer != null)
        {
            spriteRenderer.color = newColor;
            Debug.Log("Sun color updated to: " + newColor);
        }

        //Text colour updater:
        if (colorChangingTexts != null)
        {
            foreach (var text in colorChangingTexts)
            {
                if (text != null)
                    text.color = newColor;
            }
        }
    }

    Color GetStarColorByProgress(float t)
    {
        if (t < 0.2f) return Color.Lerp(new Color(0.4f, 0.6f, 1f), Color.white, t / 0.2f); // Blue to White
        else if (t < 0.4f) return Color.Lerp(Color.white, Color.yellow, (t - 0.2f) / 0.2f); // White to Yellow
        else if (t < 0.6f) return Color.Lerp(Color.yellow, new Color(1f, 0.6f, 0.2f), (t - 0.4f) / 0.2f); // Yellow to Orange
        else return Color.Lerp(new Color(1f, 0.6f, 0.2f), Color.red, (t - 0.6f) / 0.4f); // Orange to Red
    }

    void collapseIntoBlackHole()
    {
        hasReachedMaxSize = true;
        StartCoroutine(CollapseAnimation());

    }

    IEnumerator CollapseAnimation()
    {
        float time = 0f;
        Vector3 startScale = transform.localScale;

        float initialSpin = rb != null ? rb.angularVelocity : 0f;
        float targetSpin = maxSpinSpeed;

        while (time < collapseDuration)
        {
            float t = time / collapseDuration;

            transform.localScale = Vector3.Lerp(startScale, Vector3.one * finalScale, t);

            if (rb != null)
                rb.angularVelocity = Mathf.Lerp(initialSpin, targetSpin, t);

            time += Time.deltaTime;
            yield return null;
        }

        transform.localScale = Vector3.one * finalScale;
        if (rb != null) rb.angularVelocity = targetSpin;

        // Disable and destroy spawners
        GameObject[] spawners = GameObject.FindGameObjectsWithTag("Spawner");
        foreach (GameObject spawner in spawners)
        {
            spawner.GetComponent<AsteroidSpawner>().enabled = false;
            Destroy(spawner);
        }

        

        // Destroy the sun
        Destroy(gameObject);

        Instantiate(blackHole, transform.position, Quaternion.identity);

        Debug.Log("Activating restart buttons");

        // Activate restart buttons
        GameObject[] restartButtons = GameObject.FindGameObjectsWithTag("restartButton");
        foreach (GameObject button in restartButtons)
        {

            button.SetActive(true);
            Debug.Log("Found buttons: " + restartButtons.Length);
        }
        Destroy(gameObject);
    }
}
