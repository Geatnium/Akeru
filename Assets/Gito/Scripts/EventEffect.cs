using UnityEngine;

public class EventEffect : MonoBehaviour
{
    private void Subject(string message)
    {
        Helper.ShowSubject(message);
    }

    private void Audio(AudioClip audioClip)
    {
        AudioManager.PlayOneShot(audioClip);
    }

    private void BigAudio(AudioClip audioClip)
    {
        AudioManager.PlayOneShotBig(audioClip);
    }
}
