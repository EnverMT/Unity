using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Alarm : MonoBehaviour
{
    private AudioSource _audioSource;
    private float _fadeDuration = 1.5f;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();        
    }

    public void Activate()
    {
        _audioSource.Play();

        StartCoroutine(Fade(0, 1f));
    }

    public void Deactivate()
    {
        StartCoroutine(Fade(1f, 0));
    }

    private IEnumerator Fade(float startVolume, float targetVolume)
    {
        float elapsed = 0f;

        while (elapsed < _fadeDuration)
        {
            elapsed += Time.deltaTime;
            _audioSource.volume = Mathf.Lerp(startVolume, targetVolume, elapsed / _fadeDuration);

            yield return null;
        }

        _audioSource.volume = targetVolume;

        if (_audioSource.volume == 0)
            _audioSource.Stop();

    }
}
