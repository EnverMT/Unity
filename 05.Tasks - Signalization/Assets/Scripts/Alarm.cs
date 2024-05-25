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
    }

    public void Activate()
    {
        _audioSource.Play();

        if (_coroutine != null)
            StopCoroutine(_coroutine);

        _coroutine = StartCoroutine(FadeVolume(0, _maxVolume));
    }

    public void Deactivate()
    {
        if (_coroutine != null)
            StopCoroutine(_coroutine);

        StartCoroutine(FadeVolume(1f, _minVolume));
    }

    private IEnumerator FadeVolume(float startVolume, float targetVolume)
    {
        float elapsed = 0f;
        _audioSource.volume = startVolume;

        while (elapsed < _fadeDuration)
        {
            elapsed += Time.deltaTime;
            _audioSource.volume = Mathf.Lerp(startVolume, targetVolume, elapsed / _fadeDuration);

            Debug.Log($"Current volume={_audioSource.volume}");

            yield return null;
        }

        _audioSource.volume = targetVolume;

        if (_audioSource.volume <= _minVolume)
            _audioSource.Stop();
    }
}
