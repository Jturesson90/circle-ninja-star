using System.Collections;
using System.Collections.Generic;
using EasyButtons;
using UnityEngine;
using UnityEngine.Audio;
using DG.Tweening;
using Drolegames;
using Drolegames.Events;

public class OnHealthChangedAudioMix : MonoBehaviour
{
    public AudioMixer audioMixer;
    [MinMax(0, 2)]
    public Vector2 pitchMinMax = new Vector2(0.8f, 1);
    public float MinPitch => pitchMinMax.x;
    public float MaxPitch => pitchMinMax.y;
    [SerializeField] private IntVariable _healthVariable = null;

    public float animationDuration = 1f;

    void Awake()
    {

    }
    private void OnEnable()
    {
        _healthVariable.SubscribeOnValueChanged(ChangePitchVolume);
    }

    private void OnDisable()
    {
        _healthVariable.UnsubscribeOnValueChanged(ChangePitchVolume);
    }

    private void ChangePitchVolume(int healthPercent)
    {
        healthPercent = Mathf.Clamp(healthPercent, 0, 100);
        float intensity = OnHealthChangedBase.GetIntensity(MinPitch, MaxPitch, healthPercent / 100f);
        audioMixer.DOSetFloat("VoicePitch", intensity, animationDuration);
    }

    [Button]
    public void ClearPitch()
    {
        audioMixer.ClearFloat("VoicePitch");
    }
}
