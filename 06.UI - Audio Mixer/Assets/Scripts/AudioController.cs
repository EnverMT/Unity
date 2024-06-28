using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Audio;


public class AudioController : MonoBehaviour
{
    [SerializeField] private AudioMixerGroup _mixerGroup;

    private const float MaxVolume = 0f;
    private const float MinVolume = -80f;

    private const float NormalizedMinVolume = 0.0001f;
    private const float NormalizedMaxVolume = 1f;
    private const float NormalizedMultiplier = 20f;

    private void OnValidate()
    {
        Assert.IsNotNull(_mixerGroup);
    }

    public void ToggleMasterAudioMute(bool enabled)
    {
        if (enabled)
            _mixerGroup.audioMixer.SetFloat(Params.Audio.Volume.MasterVolume, MaxVolume);
        else
            _mixerGroup.audioMixer.SetFloat(Params.Audio.Volume.MasterVolume, MinVolume);
    }

    public void ChangeOverallVolume(float volume)
    {
        _mixerGroup.audioMixer.SetFloat(Params.Audio.Volume.OverallVolume, GetNormalizedVolume(volume));
    }

    public void ChangeButtonVolume(float volume)
    {
        _mixerGroup.audioMixer.SetFloat(Params.Audio.Volume.ButtonVolume, GetNormalizedVolume(volume));
    }

    public void ChangeBackgroundMusicVolume(float volume)
    {
        _mixerGroup.audioMixer.SetFloat(Params.Audio.Volume.BackgroundVolume, GetNormalizedVolume(volume));
    }

    private float GetNormalizedVolume(float volume)
    {
        return Mathf.Log10(Mathf.Clamp(volume, NormalizedMinVolume, NormalizedMaxVolume)) * NormalizedMultiplier;
    }
}
