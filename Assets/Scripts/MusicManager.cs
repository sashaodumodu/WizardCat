using UnityEngine;
using System.Collections;

public class MusicManager : MonoBehaviour
{
    public static MusicManager instance;

    private AudioSource audioSource;
    private Coroutine fadeCoroutine;

    private float startTime = 0f;
    private float endTime = 0f;
    private float maxVolume = 1f;
    private bool useLoopWindow = false;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        audioSource = GetComponent<AudioSource>();

        if (audioSource == null)
            audioSource = gameObject.AddComponent<AudioSource>();

        audioSource.playOnAwake = false;
        audioSource.loop = false;
        audioSource.volume = 0f;
    }

    void Update()
    {
        if (audioSource.isPlaying && useLoopWindow)
        {
            if (audioSource.time >= endTime)
            {
                audioSource.time = startTime;
            }
        }
    }

    public void PlayMusic(AudioClip sound, float newStartTime, float newEndTime, float newMaxVolume, float fadeInDuration)
    {
        if (audioSource.clip == sound && audioSource.isPlaying)
            return;

        if (fadeCoroutine != null)
            StopCoroutine(fadeCoroutine);

        fadeCoroutine = StartCoroutine(SwapMusic(sound, newStartTime, newEndTime, newMaxVolume, fadeInDuration));
    }

    public void StopMusic(float fadeOutDuration)
    {
        if (fadeCoroutine != null)
            StopCoroutine(fadeCoroutine);

        fadeCoroutine = StartCoroutine(FadeOutAndStop(fadeOutDuration));
    }

    IEnumerator SwapMusic(AudioClip sound, float newStartTime, float newEndTime, float newMaxVolume, float fadeInDuration)
    {
        if (audioSource.isPlaying)
        {
            yield return StartCoroutine(FadeVolume(0f, fadeInDuration));
            audioSource.Stop();
        }

        audioSource.clip = sound;
        audioSource.time = newStartTime;
        audioSource.volume = 0f;
        audioSource.Play();

        startTime = newStartTime;
        endTime = newEndTime;
        maxVolume = newMaxVolume;
        useLoopWindow = endTime > startTime;

        yield return StartCoroutine(FadeVolume(maxVolume, fadeInDuration));
    }

    IEnumerator FadeVolume(float targetVolume, float duration)
    {
        float startingVolume = audioSource.volume;
        float time = 0f;

        while (time < duration)
        {
            time += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(startingVolume, targetVolume, time / duration);
            yield return null;
        }

        audioSource.volume = targetVolume;
    }

    IEnumerator FadeOutAndStop(float fadeOutDuration)
    {
        float startingVolume = audioSource.volume;
        float time = 0f;

        while (time < fadeOutDuration)
        {
            time += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(startingVolume, 0f, time / fadeOutDuration);
            yield return null;
        }

        audioSource.volume = 0f;
        audioSource.Stop();
        audioSource.clip = null;
        useLoopWindow = false;
    }
}