using UnityEngine;
using System.Collections;
using AK.Wwise;
using Wwise;

public class PASystemAudioManager : MonoBehaviour
{
    [Header("Announcement Timing")]
    public float minDelayBetweenAnnouncements = 120f;
    public float maxDelayBetweenAnnouncements = 240f;

    [Header("Wwise Objects in Scene")]
    public GameObject speakerObject;

    [Header("Wwise Events")]
    public AK.Wwise.Event musicEvent;
    public AK.Wwise.Event announcementEvent;

    [Header("RTPC")]
    public RTPC musicVolumeRTPC;

    [Header("Mix Settings")]
    [Range(0, 100)] public float normalMusicVolume = 100f;
    [Range(0, 100)] public float fadedMusicVolume = 0f;
    public float fadeDuration = 1.0f;

    private bool isAnnouncementPlaying = false;
    private bool announcementFinished = false;

    void Start()
    {
        musicVolumeRTPC.SetValue(speakerObject, normalMusicVolume);
        musicEvent.Post(speakerObject);

        StartCoroutine(AnnouncementLoop());
    }

    public void TriggerAnnouncement()
    {
        if (!isAnnouncementPlaying)
            StartCoroutine(HandleAnnouncement());
    }

    private IEnumerator HandleAnnouncement()
    {
        isAnnouncementPlaying = true;

        // 1. Fade music out
        yield return StartCoroutine(FadeRTPC(musicVolumeRTPC, normalMusicVolume, fadedMusicVolume, fadeDuration));

        // 2. Play announcement and wait for it to finish via callback
        announcementFinished = false;

       AkUnitySoundEngine.PostEvent(
        announcementEvent.Id,
        speakerObject,
        (uint)AkCallbackType.AK_EndOfEvent,
        OnAnnouncementEnd,
        null);

        yield return new WaitUntil(() => announcementFinished);

        // 3. Fade music back in
        yield return StartCoroutine(FadeRTPC(musicVolumeRTPC, fadedMusicVolume, normalMusicVolume, fadeDuration));

        isAnnouncementPlaying = false;
    }

    private void OnAnnouncementEnd(object cookie, AkCallbackType type, AkCallbackInfo info)
    {
        if (type == AkCallbackType.AK_EndOfEvent)
        {
            announcementFinished = true;
        }
    }

    private IEnumerator FadeRTPC(RTPC rtpc, float from, float to, float duration)
    {
        float elapsed = 0f;
        while (elapsed < duration)
        {
            float value = Mathf.Lerp(from, to, elapsed / duration);
            rtpc.SetValue(speakerObject, value);
            elapsed += Time.deltaTime;
            yield return null;
        }
        rtpc.SetValue(speakerObject, to);
    }

    private IEnumerator AnnouncementLoop()
    {
        while (true)
        {
            float waitTime = Random.Range(minDelayBetweenAnnouncements, maxDelayBetweenAnnouncements);
            yield return new WaitForSeconds(waitTime);

            if (!isAnnouncementPlaying)
                StartCoroutine(HandleAnnouncement());
        }
    }
}
