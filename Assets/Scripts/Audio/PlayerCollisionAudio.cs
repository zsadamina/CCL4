using UnityEngine;
using AK.Wwise;

[RequireComponent(typeof(Collider), typeof(Rigidbody), typeof(AkGameObj))]
public class PlayerCollisionAudio : MonoBehaviour
{
    [Header("Wwise Collision Sound")]
    public AK.Wwise.Event collisionSound;

    [Tooltip("Minimum time between bump sounds (seconds)")]
    public float soundCooldown = 0.3f;

    private float lastSoundTime = -999f;

    private void OnTriggerEnter(Collider other)
    {
        if (collisionSound == null) return;

        if (Time.time - lastSoundTime >= soundCooldown)
        {
            Debug.Log($"[Trigger] Player touched: {other.name}");
            collisionSound.Post(gameObject);
            lastSoundTime = Time.time;
        }
    }
}
