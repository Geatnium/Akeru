using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource bgmSource, seSource, bigSESource;

    public static void PlayOneShot(AudioClip audioClip)
    {
        GameObject.FindWithTag("AudioManager").GetComponent<AudioManager>()._PlayOneShot(audioClip);
    }

    private void _PlayOneShot(AudioClip audioClip)
    {
        if (audioClip != null)
            seSource.PlayOneShot(audioClip);
    }

    public static void PlayOneShotBig(AudioClip audioClip)
    {
        GameObject.FindWithTag("AudioManager").GetComponent<AudioManager>()._PlayOneShotBig(audioClip);
    }

    private void _PlayOneShotBig(AudioClip audioClip)
    {
        if (audioClip != null)
            bigSESource.PlayOneShot(audioClip);
    }
}
