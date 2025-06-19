using UnityEngine;
using AK.Wwise;

public class YawnController : MonoBehaviour
{
    [Header("Player Stats")]
    public float currentHealth = 100f;

    [Header("Wwise Events")]
    public AK.Wwise.Event normalYawnEvent;
    public AK.Wwise.Event exhaustedYawnEvent;   // add in Wwise later

    private float yawnTimer;
    private float nextYawnInterval = 180f;      // default fallback

    void Start()
    {
        UpdateYawnInterval();                   // set correct first interval
    }

    void Update()
    {
        yawnTimer += Time.deltaTime;

        if (yawnTimer >= nextYawnInterval)
        {
            PlayYawn();
            yawnTimer = 0f;
            UpdateYawnInterval();               // refresh timing after each yawn
        }
    }

    void UpdateYawnInterval()
    {
        Debug.Log("Attempting to play yawn sound (health: " + currentHealth + ")");
        
        if (currentHealth > 70f)
            nextYawnInterval = 180f;            // 3 min
        else if (currentHealth > 50f)
            nextYawnInterval = 90f;             // 1.5 min
        else if (currentHealth > 30f)
            nextYawnInterval = 60f;             // 1 min
        else
            nextYawnInterval = 30f;             // 1 min (exhausted sound)
    }

    void PlayYawn()
    {
        if (currentHealth > 30f)
            normalYawnEvent.Post(gameObject);
        else
            exhaustedYawnEvent.Post(gameObject); // switch to tired yawns
    }

    // Optional helper for other scripts
    public void SetHealth(float value)
    {
        currentHealth = Mathf.Clamp(value, 0f, 100f);
        UpdateYawnInterval();
    }
}
