using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SpawnType/Idle")]
public class IdleSpawnTypeConfig : BaseSpawnTypeConfig
{
    public override SpawnType GetSpawnType(MonoBehaviour behaviour) => new IdleSpawnType(behaviour, this);
}
