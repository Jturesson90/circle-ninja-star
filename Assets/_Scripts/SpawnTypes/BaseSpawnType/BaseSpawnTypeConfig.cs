using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public abstract class BaseSpawnTypeConfig : ScriptableObject
{
    [Header("Base Spawn Type Config")]
    [SerializeField] float _delayBeforeSpawning = 1f;
    [SerializeField] float _spawnDuration = 60f;
    [SerializeField] float _shrinkSpeed = 3f;
    [SerializeField] float _transitionSpeedDuration = 1f;
    [SerializeField] Ease _transitionEaseType = Ease.InOutSine;
    [SerializeField] bool _canBeOverridden = true;

    public float DelayBeforeSpawning => _delayBeforeSpawning;
    public float SpawnDuration => _spawnDuration;
    public float ShrinkSpeed => _shrinkSpeed;
    public float TransitionSpeedDuration => _transitionSpeedDuration;
    public Ease TransitionEaseType => _transitionEaseType;
    public bool CanBeOverridden => _canBeOverridden;

    public abstract SpawnType GetSpawnType(MonoBehaviour behaviour);
}
