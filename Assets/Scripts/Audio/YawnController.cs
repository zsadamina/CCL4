using UnityEngine;
using AK.Wwise;

public class YawnController : MonoBehaviour
{
    [Header("Player Stats")]
    public float currentHealth = 100f;

    [Header("Wwise Events")]
    public AK.Wwise.Event normalYawnEvent;
    public AK.Wwise.Event exhaustedYawnEvent;  
    public AK.Wwise.Event fallAsleepEvent;

    private float yawnTimer;
    private float nextYawnInterval = 180f;    
    private bool isAsleep = false;  

    void Start()
    {
        UpdateYawnInterval();                   
    }

    void Update()
    {
        yawnTimer += Time.deltaTime;

        if (yawnTimer >= nextYawnInterval)
        {
            PlayYawn();
            yawnTimer = 0f;
            UpdateYawnInterval();               
        }
    }

    void UpdateYawnInterval()
    {
        Debug.Log("Attempting to play yawn sound (health: " + currentHealth + ")");
        
        if (currentHealth > 70f)
            nextYawnInterval = 180f;            
        else if (currentHealth > 50f)
            nextYawnInterval = 90f;             
        else if (currentHealth > 30f)
            nextYawnInterval = 60f;             
        else
            nextYawnInterval = 30f;            
    }

    void PlayYawn()
{
    if (currentHealth <= 0f)
    {
        if (!isAsleep)
        {
            isAsleep = true;
            fallAsleepEvent.Post(gameObject); 
        }
        return; 
    }

    if (currentHealth > 30f)
    {
        normalYawnEvent.Post(gameObject);
    }
    else
    {
        exhaustedYawnEvent.Post(gameObject);
    }
}

    // Optional helper for other scripts
    public void SetHealth(float value)
    {
        currentHealth = Mathf.Clamp(value, 0f, 100f);
        UpdateYawnInterval();
    }
}
