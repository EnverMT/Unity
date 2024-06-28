using UnityEngine;
using UnityEngine.Audio;


public class AudioController : MonoBehaviour
{
    [SerializeField] private AudioMixerGroup _mixerGroup;

    public void ToggleMasterAudioMute(bool enabled)
    {
        if (enabled)
            _mixerGroup.audioMixer.SetFloat(Params.Audio.Volume.MasterVolume, 0);
        else
            _mixerGroup.audioMixer.SetFloat(Params.Audio.Volume.MasterVolume, -80);
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
        return Mathf.Log10(Mathf.Clamp(volume, 0.001f, 1f)) * 20;
    }
}
