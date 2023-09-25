using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class MuteToggle : MonoBehaviour
{
    public enum MuteOptions
    {
        Master,
        BGM,
        SFX
    }

    [SerializeField] AudioMixer mixer;
    [SerializeField] MuteOptions muteOption;

    public void ToggleMute(bool isOn)
    {
        switch (muteOption)
        {
            case MuteOptions.Master:
                mixer.SetFloat("masterVolume", isOn ? -80f : -40f);
                break;
            case MuteOptions.BGM:
                mixer.SetFloat("bgmVolume", isOn ? -80f : 0f);
                break;
            case MuteOptions.SFX:
                mixer.SetFloat("sfxVolume", isOn ? -80f : 0f);
                break;
        }
    }
}
