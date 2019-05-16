using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SpawnType/Rotation Sequence")]
public class RotationSequenceSpawnTypeConfig : NormalSpawnTypeConfig
{
    [Header("Rotation Sequence Spawn Type")]
    [SerializeField] int _degreesPerStep = 20;
    public int DegreesPerStep => _degreesPerStep;

    public override SpawnType GetSpawnType(MonoBehaviour behaviour) => new RotationSequenceSpawnType(behaviour, this);

}
