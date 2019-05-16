using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleSpawnType : BaseSpawnType<IdleSpawnTypeConfig>
{
    protected override IdleSpawnTypeConfig Config { get; set; }
    public IdleSpawnType(MonoBehaviour mono, IdleSpawnTypeConfig config) : base(mono, config)
    {
    }

    protected override IEnumerator HandleSpawn(Action<SpawnDto> spawnCallback, Action endCallback)
    {
        while (ShouldSpawn)
        {

            yield return null;
        }
        endCallback();
    }
}
