using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public abstract class SpawnType
{
    public abstract void Start(Action<SpawnDto> onSpawnedNewEnemy, Action onSpawnEndCallback);
    public abstract void Stop();
}
