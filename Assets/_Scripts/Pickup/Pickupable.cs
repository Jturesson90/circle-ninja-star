using System;
using Drolegames.Events;
using UnityEngine;

public abstract class Pickupable
{
    public readonly static PickupConfigEvent OnPickup = new PickupConfigEvent();
    public abstract void Use(GameObject target, Action safeToDeleteCallback);
}
