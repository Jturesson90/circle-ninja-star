using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class MuteButton : MonoBehaviour
{
    public Image imageUI;
    [Space()]
    public AudioMixer mixer;
    public Sprite muteSprite;
    public Sprite unMuteSprite;

    private float _volume;

    const string masterVolume = "MasterVolume";
    const string soundState = "sound-state";

    private void Awake()
    {
        mixer.GetFloat(masterVolume, out _volume);
        if (GetIsMuted())
        {
            Mute();
        }
        else
        {
            UnMute();
        }
  
    }

    public void Toggle()
    {
        if (GetIsMuted())
            UnMute();
        else
            Mute();
    }

    private bool GetIsMuted()
    {
        return PlayerPrefs.GetInt(soundState, 0) == 1;
    }

    private void Mute()
    {
        PlayerPrefs.SetInt(soundState, 1);
        imageUI.sprite = unMuteSprite;
        mixer.DOSetFloat(masterVolume, -80f, 0.3f);
    }

    private void UnMute()
    {
        PlayerPrefs.SetInt(soundState, 0);
        mixer.DOSetFloat(masterVolume, _volume,  0.3f);
        imageUI.sprite = muteSprite;
    }
}
