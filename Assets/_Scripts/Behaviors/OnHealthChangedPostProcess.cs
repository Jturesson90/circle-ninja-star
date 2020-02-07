using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Drolegames.Events;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class OnHealthChangedPostProcess : MonoBehaviour
{
    [SerializeField] private IntVariable _health = null;

    [Header("Vignette")]
    [SerializeField] private float _vignetteMaxIntensity = 0;
    private float _vignetteStartIntensity = 0f;

    [Header("Color Grading")]
    [SerializeField] private float _colorGradingMaxSaturation = 0;
    private float _colorGradingStartSaturation = 0f;

    private PostProcessVolume _postVolume;
    private Vignette _vignette;
    private ColorGrading _colorGrading;

    private void Awake()
    {
        _postVolume = GetComponent<PostProcessVolume>();
        _postVolume.profile.TryGetSettings(out _vignette);
        _postVolume.profile.TryGetSettings(out _colorGrading);

        _vignetteStartIntensity = _vignette.intensity;
        _colorGradingStartSaturation = _colorGrading.saturation;
    }
    void OnEnable()
    {
        _health.SubscribeOnValueChanged(OnValueChanged);
    }

    void OnDisable()
    {
        _health.UnsubscribeOnValueChanged(OnValueChanged);
    }

    private void OnValueChanged(int value)
    {
        value = Mathf.Clamp(value, 0, 100);
        float invertPercent = (100 - value) / 100f;

        float vignetteIntensity = OnHealthChangedBase.GetIntensity(_vignetteStartIntensity, _vignetteMaxIntensity, invertPercent);
        if (_vignette)
        {
            DOTween.To(() => _vignette.intensity.value, x => _vignette.intensity.value = x, vignetteIntensity, 0.5f);
        }

        float colorGradingSaturation = OnHealthChangedBase.GetIntensity(_colorGradingStartSaturation, _colorGradingMaxSaturation, invertPercent);
        if (_colorGrading)
        {
            DOTween.To(() => _colorGrading.saturation.value, x => _colorGrading.saturation.value = x, colorGradingSaturation, 0.5f);
        }

    }


}
