using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Alarm : MonoBehaviour
{
    [SerializeField] private float _fadeDuration = 1.5f;
    [SerializeField] private float _minVolume = 0f;
    [SerializeField] private float _maxVolume = 1f;

    private AudioSource _audioSource;


    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public void Activate()
    {
        _audioSource.Play();

        StartCoroutine(Fade(0, _maxVolume));
    }

    public void Deactivate()
    {
        StartCoroutine(Fade(1f, _minVolume));
    }

    private IEnumerator Fade(float startVolume, float targetVolume)
    {
        float elapsed = 0f;

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
