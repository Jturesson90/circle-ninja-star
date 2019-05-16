using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class NormalSpawnType : BaseSpawnType<NormalSpawnTypeConfig>
{
    protected override NormalSpawnTypeConfig Config { get; set; }

    public NormalSpawnType(MonoBehaviour mono, NormalSpawnTypeConfig config) : base(mono, config)
    {
        Config = config;
    }

    protected override IEnumerator HandleSpawn(Action<SpawnDto> spawnCallback, Action endCallback)
    {
        var wait = new WaitForSeconds(60f / Config.BeatsPerMinute);
        yield return new WaitForSeconds(Config.DelayBeforeSpawning);
        ShouldSpawn = true;
        while (ShouldSpawn)
        {
            var enemy = new SpawnDto
            {
                RotationType = Config.RotationType,
                Degrees = Random.Range(Config.DegreeMin, Config.DegreeMax),
                Rotation = Random.Range(Config.RotationMin, Config.RotationMax),
                CanBeOverridden = Config.CanBeOverridden,
                ShrinkSpeed = Config.ShrinkSpeed,
                RotationSpeed = Config.RotationSpeed
            };
            spawnCallback(enemy);

            yield return wait;
        }
        endCallback();
    }
}
