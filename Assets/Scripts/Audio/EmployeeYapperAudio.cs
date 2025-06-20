using UnityEngine;
using AK.Wwise;

[RequireComponent(typeof(AkGameObj))]
public class EmployeeYapperAudio : MonoBehaviour
{
    [Header("Wwise Events")]
    [SerializeField] private AK.Wwise.Event footstepLoopEvent;
    [SerializeField] private AK.Wwise.Event dialogueEvent;

    [Header("Player XR Origin")]
    private GameObject playerObject;

    [Header("Dialogue Source")]
    [SerializeField] private GameObject dialogueSourceObject;

    [Header("Trigger Settings")]
    [SerializeField] private float triggerDistance = 4.0f;
    [SerializeField] private float triggerActivationDelay = 1.0f;

    private uint dialoguePlayingId = AkUnitySoundEngine.AK_INVALID_PLAYING_ID;
    private bool isPlayerInside = false;
    private bool triggerActive = false;

    void Start()
    {
        
        playerObject = GameObject.FindWithTag("Player");
        
        if (footstepLoopEvent.IsValid())
            footstepLoopEvent.Post(dialogueSourceObject != null ? dialogueSourceObject : gameObject);

        if (dialogueSourceObject == null)
            dialogueSourceObject = gameObject;

        Invoke(nameof(ActivateTrigger), triggerActivationDelay);
    }

    void ActivateTrigger() => triggerActive = true;

    void Update()
    {
        if (!triggerActive || playerObject == null) return;

        float distance = Vector3.Distance(playerObject.transform.position, transform.position);
        bool isNowInside = distance <= triggerDistance;

        if (isNowInside && !isPlayerInside)
        {
            isPlayerInside = true;
            Debug.Log("Dialogue START (Distance check)");
            if (dialogueEvent.IsValid())
                dialoguePlayingId = dialogueEvent.Post(dialogueSourceObject);
        }
        else if (!isNowInside && isPlayerInside)
        {
            isPlayerInside = false;
            Debug.Log("Dialogue STOP (Distance check)");
            if (dialoguePlayingId != AkUnitySoundEngine.AK_INVALID_PLAYING_ID)
            {
                AkUnitySoundEngine.StopPlayingID(dialoguePlayingId);
                dialoguePlayingId = AkUnitySoundEngine.AK_INVALID_PLAYING_ID;
            }
        }
    }
}
