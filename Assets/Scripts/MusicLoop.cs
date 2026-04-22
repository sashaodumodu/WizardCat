using UnityEngine;

public class MusicLoop : MonoBehaviour
{
    [Header("Audio")]
    public AudioClip sound;
    public float startTime = 0f;
    public float endTime = 30f;
    public float maxVolume = 1f;

    [Header("Fade")]
    public float fadeInDuration = 1.5f;
    public float fadeOutDuration = 1.5f;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        MusicManager.instance.PlayMusic(sound, startTime, endTime, maxVolume, fadeInDuration);
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        MusicManager.instance.StopMusic(fadeOutDuration);
    }
}