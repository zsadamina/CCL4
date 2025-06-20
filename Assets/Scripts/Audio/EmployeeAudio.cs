using UnityEngine;
using AK.Wwise;

[RequireComponent(typeof(AkGameObj))]
public class EmployeeAudio : MonoBehaviour
{
    [Header("Wwise Events")]
    [SerializeField] private AK.Wwise.Event footstepLoopEvent;
    [SerializeField] private AK.Wwise.Event dialogueEvent;

    [Header("Dialogue Trigger Zone")]
    [SerializeField] private Collider dialogueTriggerZone;
    [SerializeField] private GameObject dialogueSourceObject;

    [Header("Player XR Origin")]
    private GameObject playerObject;
    private uint dialoguePlayingId = 0;

    private void Start()
    {

        playerObject = GameObject.FindWithTag("Player");

        if (footstepLoopEvent.IsValid())
            footstepLoopEvent.Post(gameObject);

        if (dialogueTriggerZone != null)
            dialogueTriggerZone.isTrigger = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == playerObject && dialoguePlayingId == 0)
        {
            dialoguePlayingId = dialogueEvent.Post(dialogueSourceObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == playerObject && dialoguePlayingId != 0)
        {
            AkUnitySoundEngine.StopPlayingID(dialoguePlayingId);
            dialoguePlayingId = 0;
        }
    }

    void OnDisable()
    {
        StopDialogueSound();
    }

    void OnDestroy()
    {
        StopDialogueSound();
    }

    private void StopDialogueSound()
    {
        if (dialoguePlayingId != AkUnitySoundEngine.AK_INVALID_PLAYING_ID)
        {
            AkUnitySoundEngine.StopPlayingID(dialoguePlayingId);
            dialoguePlayingId = AkUnitySoundEngine.AK_INVALID_PLAYING_ID;
        }
    }

}
