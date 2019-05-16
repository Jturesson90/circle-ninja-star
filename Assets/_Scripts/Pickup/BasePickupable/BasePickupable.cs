using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Drolegames.Events;

public abstract class BasePickupable<P> : Pickupable where P : BasePickupableConfig
{
    protected abstract P Config { get; set; }
    private GameObject _gameObject;

    public BasePickupable(GameObject gameObject, P config)
    {
        _gameObject = gameObject;
        Config = config;

        var renderer = gameObject.GetComponent<SpriteRenderer>();
        if (renderer && Config.SpriteMaterial)
        {
            renderer.material = Config.SpriteMaterial;
        }
    }
    public override void Use(GameObject target, Action safeToDeleteCallback)
    {
        OnPickup?.Invoke(Config);
        if (safeToDeleteCallback != null)
        {
            safeToDeleteCallback();
        }
    }
}