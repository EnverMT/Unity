using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Alarm : MonoBehaviour
{
    [SerializeField] private float _fadeDuration = 1.5f;
    [SerializeField] private float _minVolume = 0f;
    [SerializeField] private float _maxVolume = 1f;

    private AudioSource _audioSource;
    private Coroutine _coroutine;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        _audioSource.volume = _minVolume;
    }

    public void Activate()
    {
        if (_coroutine != null)        
            StopCoroutine(_coroutine);                    

        _audioSource.Play();
        _coroutine = StartCoroutine(FadeVolume(_audioSource, _maxVolume, _fadeDuration));
    }

    public void Deactivate()
    {
        if (_coroutine != null)
            StopCoroutine(_coroutine);

        StartCoroutine(FadeVolume(_audioSource, _minVolume, _fadeDuration));
    }

    private IEnumerator FadeVolume(AudioSource audioSource, float targetVolume, float fadeDuration)
    {
        while (!Mathf.Approximately(audioSource.volume, targetVolume))
        {
            audioSource.volume = Mathf.MoveTowards(audioSource.volume, targetVolume, Time.deltaTime / fadeDuration);
            yield return null;
        }

        if (audioSource.volume < _minVolume)
            audioSource.Stop();
    }
}
