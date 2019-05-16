using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPickupable : BasePickupable<SpawnPickupableConfig>
{
    protected override SpawnPickupableConfig Config { get; set; }
    private EnemySpawner _spawner;
    public SpawnPickupable(GameObject mono, SpawnPickupableConfig config) : base(mono, config)
    {
        _spawner = GameObject.FindObjectOfType<EnemySpawner>();
        Config = config;
    }
    public override void Use(GameObject target, Action safeToDeleteCallback)
    {
        base.Use(target, null);
        if (_spawner)
        {
            BaseSpawnTypeConfig spawnTypeConfig = Config.SpawnTypeConfig;
            _spawner.ForceNewSpawnType(spawnTypeConfig);
        }

        safeToDeleteCallback();
    }
}
