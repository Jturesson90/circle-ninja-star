using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class RotationSequenceSpawnType : BaseSpawnType<RotationSequenceSpawnTypeConfig>
{
    protected override RotationSequenceSpawnTypeConfig Config { get; set; }

    public RotationSequenceSpawnType(MonoBehaviour mono, RotationSequenceSpawnTypeConfig config) : base(mono, config)
    {
        Config = config;
    }

    protected override IEnumerator HandleSpawn(Action<SpawnDto> spawnCallback, Action endCallback)
    {
        var waitTime = new WaitForSeconds(60f / Config.BeatsPerMinute);
        var rotation = Random.Range(Config.RotationMin, Config.RotationMax);
        while (ShouldSpawn)
        {
            var spawn = new SpawnDto
            {
                RotationType = Config.RotationType,
                Degrees = Random.Range(Config.DegreeMin, Config.DegreeMax),
                Rotation = rotation,
                ShrinkSpeed = Config.ShrinkSpeed,
                CanBeOverridden = Config.CanBeOverridden,
                RotationSpeed = Config.RotationSpeed
            };
            rotation += Config.DegreesPerStep;

            spawnCallback(spawn);

            yield return waitTime;
        }
        endCallback();
    }
}
