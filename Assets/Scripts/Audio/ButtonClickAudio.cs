using UnityEngine;
using UnityEngine.UI;
using AK.Wwise;

[RequireComponent(typeof(Button))]
public class ButtonClickAudio : MonoBehaviour
{
    public AK.Wwise.Event clickSoundEvent; 
    private void Awake()
    {

        GetComponent<Button>().onClick.AddListener(PlayClickSound);
    }

    private void PlayClickSound()
    {
        if (clickSoundEvent != null)
        {
            clickSoundEvent.Post(gameObject);
        }
    }
}
