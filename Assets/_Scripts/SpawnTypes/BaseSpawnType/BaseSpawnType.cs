using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public abstract class BaseSpawnType<C> : SpawnType where C : BaseSpawnTypeConfig
{
    private bool _shouldSpawn;
    private float _spawnStartTime;
    protected bool ShouldSpawn
    {
        get
        {
            if (!_shouldSpawn) return false;
            bool exceededDuration = Time.timeSinceLevelLoad - _spawnStartTime > Config.SpawnDuration;

            return !exceededDuration;
        }
        set { _shouldSpawn = value; }
    }
    protected abstract C Config { get; set; }
    protected MonoBehaviour Mono { get; private set; }

    protected abstract IEnumerator HandleSpawn(Action<SpawnDto> spawnCallback, Action endCallback);

    protected BaseSpawnType(MonoBehaviour monoBehaviour, C config)
    {
        Mono = monoBehaviour;
        Config = config;
    }
    public override void Start(Action<SpawnDto> onSpawnedNewEnemy, Action onSpawnEndCallback)
    {
        DOTween
        .To(
            () => Enemy.ShrinkSpeed,
            x => Enemy.ShrinkSpeed = x,
            Config.ShrinkSpeed,
            Config.TransitionSpeedDuration)
        .SetEase(Config.TransitionEaseType).SetDelay(Config.DelayBeforeSpawning);

        _spawnStartTime = Time.timeSinceLevelLoad;
        ShouldSpawn = true;

        Mono.StartCoroutine(HandleSpawn(onSpawnedNewEnemy, onSpawnEndCallback));
    }

    public override void Stop() => ShouldSpawn = false;
}
