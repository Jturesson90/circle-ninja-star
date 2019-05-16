using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickupable : BasePickupable<HealthPickupableConfig>
{
    protected override HealthPickupableConfig Config { get; set; }

    public HealthPickupable(GameObject mono, HealthPickupableConfig config) : base(mono, config)
    {
        Config = config;
    }

    public override void Use(GameObject target, Action safeToDeleteCallback)
    {
        base.Use(target, null);
        target.GetComponent<RecoveringHealth>()?
        .Heal(Config.Heal);

        safeToDeleteCallback();
    }
}
