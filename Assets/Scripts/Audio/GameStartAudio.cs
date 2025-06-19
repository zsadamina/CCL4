using System.Collections;
using UnityEngine;

public class GameStartAudio : MonoBehaviour
{
    [Header("Audio Sources")]
    public GameObject dropSoundSource;
    public GameObject companionRigTransform;

    [Header("Wwise Events")]
    public AK.Wwise.Event dropEvent;
    public AK.Wwise.Event companionEvent1;
    public AK.Wwise.Event companionEvent2;

    [Header("Timing")]
    public float delay = 3.0f;

    void Start()
    {
        StartCoroutine(PlayIntroSequence());
    }

    private IEnumerator PlayIntroSequence()
    {
        dropEvent.Post(dropSoundSource);

        yield return new WaitForSeconds(delay);

        // ▶️ Play first companion line
        companionEvent2.Post(companionRigTransform);

        // ⏱ Wait for it to finish (adjust duration to match actual line)
        yield return new WaitForSeconds(61f); // change this if needed

        // ▶️ Play second companion line
        companionEvent2.Post(companionRigTransform);
    }

}
