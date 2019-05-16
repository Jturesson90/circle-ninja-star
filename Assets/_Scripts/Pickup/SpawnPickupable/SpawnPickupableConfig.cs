using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Pickup/Spawn")]
public class SpawnPickupableConfig : BasePickupableConfig
{
    [Header("Spawn Settings")]
    [SerializeField] private BaseSpawnTypeConfig _spawnTypeConfig = null;
    public BaseSpawnTypeConfig SpawnTypeConfig => _spawnTypeConfig;

    public override Pickupable GetPickupable(GameObject objectToAttachTo) => new SpawnPickupable(objectToAttachTo, this);

}
