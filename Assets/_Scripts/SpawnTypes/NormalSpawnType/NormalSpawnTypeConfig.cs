using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Drolegames;

[CreateAssetMenu(menuName = "SpawnType/Normal")]
public class NormalSpawnTypeConfig : BaseSpawnTypeConfig
{
    [Header("Normal Spawn Type Configs")]
    [SerializeField] private RotationTypes _rotationType = RotationTypes.Random;
    [SerializeField] private float _beatsPerMinute = 60f;
    [MinMax(0, 359), SerializeField] private Vector2Int _rotationMinMax = new Vector2Int(0, 359);
    [MinMax(0, 359), SerializeField] private Vector2Int _degreeMinMax = new Vector2Int(0, 359);
    [SerializeField] private float _rotationSpeed = 3f;

    public float BeatsPerMinute => _beatsPerMinute;
    public float RotationMin => _rotationMinMax.x;
    public float RotationMax => _rotationMinMax.y;
    public float RotationSpeed => _rotationSpeed;
    public int DegreeMin => _degreeMinMax.x;
    public int DegreeMax => _degreeMinMax.y;
    public RotationTypes RotationType => _rotationType;

    public override SpawnType GetSpawnType(MonoBehaviour behaviour) => new NormalSpawnType(behaviour, this);
}
