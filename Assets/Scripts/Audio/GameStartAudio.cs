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
    [SerializeField] Animator companionAnimator;

    [Header("Timing")]
    public float delay = 3.0f;

    void Start()
    {
        StartCoroutine(PlayIntroSequence());
    }

    private IEnumerator PlayIntroSequence()
    {
        yield return new WaitForSeconds(1.0f); 
        dropEvent.Post(dropSoundSource);

        yield return new WaitForSeconds(delay);
        companionAnimator.SetBool("IsTalking", true);

        // ▶️ Play first companion line
        companionEvent1.Post(companionRigTransform);

        // ⏱ Wait for it to finish (adjust duration to match actual line)
        yield return new WaitForSeconds(61f); // change this if needed

        // ▶️ Play second companion line
        companionEvent2.Post(companionRigTransform);

        yield return new WaitForSeconds(20f);
        companionAnimator.SetBool("IsTalking", false);
    }

}
