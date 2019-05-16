using System;
using System.Collections;
using System.Collections.Generic;
using Drolegames.Events;
using UnityEngine;
public class Pickup : MonoBehaviour
{
    private bool _executedTrigger = false;
    [SerializeField] private BasePickupableConfig _config;
    private Pickupable _pickupable;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_executedTrigger) { return; }
        _pickupable?.Use(collision.transform.gameObject, () => Destroy(gameObject));
        _executedTrigger = true;
    }

    private void Start()
    {
        if (_config != null)
        {
            Initialize(_config);
        }
    }
    public void Initialize(BasePickupableConfig config)
    {
        _config = config;
        _pickupable = _config.GetPickupable(gameObject);
    }
    public void Initialize(Pickupable pickupable)
    {
        _pickupable = pickupable;
    }
}
