using System;
using System.Collections;
using System.Collections.Generic;
using EasyButtons;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(AudioSource))]
public class SFXManager : MonoBehaviour
{
    [SerializeField] private List<AudioClip> _onPlayerHitSounds = null;
    [SerializeField] private List<AudioClip> _onGameStartSounds = null;
    [SerializeField] private List<AudioClip> _onGameOverSounds = null;

    private AudioSource _audioSource;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        Pickupable.OnPickup.AddListener(PlayPickupSound);
    }

    private void OnDisable()
    {
        Pickupable.OnPickup.RemoveListener(PlayPickupSound);
    }
    public void OnPlayerHit()
    {
        PlayHitSound();
    }
    public void OnGameStarted()
    {
        PlayGameStartSound();
    }
    public void OnGameOver()
    {
        PlayGameOverSound();
    }


    [Button(spacing: ButtonSpacing.After)]
    private void PlayHitSound()
    {
        PlayHitSound(true);
    }

    private void PlayHitSound(bool isAlive)
    {
        if (!isAlive) return;
        if (_onPlayerHitSounds.Count > 0)
            _audioSource.PlayOneShot(_onPlayerHitSounds[Random.Range(0, _onPlayerHitSounds.Count)]);
    }

    [Button]
    private void PlayGameStartSound()
    {
        if (_onGameStartSounds.Count > 0)
            _audioSource.PlayOneShot(_onGameStartSounds[Random.Range(0, _onGameStartSounds.Count)]);
    }

    [Button]
    private void PlayGameOverSound()
    {
        if (_onGameOverSounds.Count > 0)
            _audioSource.PlayOneShot(_onGameOverSounds[Random.Range(0, _onGameOverSounds.Count)]);
    }
    public void PlaySound(AudioClip clip)
    {
        if (clip != null)
        {
            PlaySFXSound(clip);
        }
    }

    private void PlayPickupSound(BasePickupableConfig pickupable)
    {
        if (pickupable.PickupSfx != null)
        {
            PlaySFXSound(pickupable.PickupSfx);
        }
    }

    public void PlaySFXSound(AudioClip clip)
    {
        _audioSource.PlayOneShot(clip);
    }
}
