#define BLOOM
#define CHROMATIC

using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using DG.Tweening;

[RequireComponent(typeof(PostProcessVolume))]
public class OnPlayerHitPostProcess : MonoBehaviour
{
    [Header("General animation")]
    [SerializeField] private float _animationAttackDuration = 0.1f;
    [SerializeField] private float _animationReleaseDuration = 1f;

    [Header("Chromatic")]
    [SerializeField] private float _chromaticIntensity = 1f;

    [Header("Bloom")]
    [SerializeField] private float _bloomIntensity = 1f;

    private PostProcessVolume _postVolume;
    private ChromaticAberration _chromatic;
    private Bloom _bloom;

    private float _chromaticStartIntensity = 0f;
    private float _bloomStartIntensity = 0f;

    private void Awake()
    {
        _postVolume = GetComponent<PostProcessVolume>();
        _postVolume.profile.TryGetSettings(out _chromatic);
        _postVolume.profile.TryGetSettings(out _bloom);

        _chromaticStartIntensity = _chromatic.intensity.value;
        _bloomStartIntensity = _bloom.intensity.value;
    }

    public void OnPlayerGotHit()
    {
#if BLOOM
        if (_bloom)
        {
            DOTween.Sequence()
                   .Append(DOTween.To(() => _bloom.intensity.value, x => _bloom.intensity.value = x, _bloomIntensity, _animationAttackDuration))
                          .Append(DOTween.To(() => _bloom.intensity.value, x => _bloom.intensity.value = x, _bloomStartIntensity, _animationReleaseDuration));
        }
#endif
#if CHROMATIC
        if (_chromatic)
        {
            DOTween.Sequence()
                   .Append(DOTween.To(() => _chromatic.intensity.value, x => _chromatic.intensity.value = x, _chromaticIntensity, _animationAttackDuration))
                          .Append(DOTween.To(() => _chromatic.intensity.value, x => _chromatic.intensity.value = x, _chromaticStartIntensity, _animationReleaseDuration));

        }
#endif

    }
}
