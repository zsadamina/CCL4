using System.Collections;
using UnityEngine;

public class GameStartAudio : MonoBehaviour
{
    [Header("Audio Sources")]
    public GameObject dropSoundSource;
    public GameObject companionRigTransform;

    [Header("Wwise Events")]
    public AK.Wwise.Event dropEvent;
    public AK.Wwise.Event companionEvent;

    [Header("Timing")]
    public float delayBeforeCompanionTalk = 3.0f;

    void Start()
    {
        StartCoroutine(PlayIntroSequence());
    }

    private IEnumerator PlayIntroSequence()
    {
        dropEvent.Post(dropSoundSource);

        yield return new WaitForSeconds(delayBeforeCompanionTalk);

        companionEvent.Post(companionRigTransform);
    }
}
