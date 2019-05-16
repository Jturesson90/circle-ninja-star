using System;
using System.Collections;
using System.Collections.Generic;
using EasyButtons;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour, IInitializeable
{
    [SerializeField] private Enemy _enemyPrefab = null;
    [SerializeField] private BaseSpawnTypeConfig _defaultSpawnTypeConfig = null;
    [SerializeField] private List<BaseSpawnTypeConfig> _standardSpawnTypeConfigs;

    private EnemyPool _pool;
    private SpawnType _defaultSpawnType = null;
    private SpawnType _currentSpawnType = null;
    private SpawnType _nextSpawnType = null;

    private bool _shouldSpawn = false;
    private bool ShouldSpawnGameObjectInsteadOfEnemy => _moveToCenterQueue.Count > 0;

    private Queue<MoveToCenter> _moveToCenterQueue = new Queue<MoveToCenter>();
    private void Awake()
    {
        _pool = new EnemyPool(_enemyPrefab);
    }

    public void Initialize()
    {

    }

    /* Start And Spawn, maybe the only thing this class should do?*/
    public void StartSpawn()
    {
        _shouldSpawn = true;
        _defaultSpawnType = _defaultSpawnTypeConfig.GetSpawnType(this);
        SetNextSpawnType(_defaultSpawnType);
        SpawnNextSpawnType();
    }

    public void StopSpawn()
    {
        _shouldSpawn = false;
        TellCurrentSpawnTypeToStop();
    }

    public void ForceNewSpawnType(BaseSpawnTypeConfig config)
    {
        ForceNewSpawnType(config.GetSpawnType(this));
    }

    public void ForceNewSpawnType(SpawnType spawnType)
    {
        if (!_shouldSpawn) return;
        SetNextSpawnType(spawnType);
        TellCurrentSpawnTypeToStop();
    }

    public void PushGameObjectToStream(GameObject newStreamObject)
    {
        var moveToCenter = newStreamObject.AddComponent<MoveToCenter>();
        newStreamObject.AddComponent<RotateGameObject>();
        _moveToCenterQueue.Enqueue(moveToCenter);
    }

    private void TellCurrentSpawnTypeToStop()
    {
        _currentSpawnType?.Stop();
    }

    private void SetNextSpawnType(SpawnType spawnType)
    {
        _nextSpawnType = spawnType;
    }

    private void SpawnNextSpawnType()
    {
        _nextSpawnType = _nextSpawnType ?? _defaultSpawnType;
        _currentSpawnType = _nextSpawnType;
        _nextSpawnType = null;
        _currentSpawnType.Start(OnNewEnemyWantToSpawn, OnSpawnTypeEnd);
    }

    private void OnSpawnTypeEnd()
    {
        if (_shouldSpawn && _nextSpawnType != null)
        {
            SpawnNextSpawnType();
        }
        else if (_shouldSpawn)
        {
            StartSpawn();
        }
    }

    private void OnNewEnemyWantToSpawn(SpawnDto spawn)
    {
        var enemy = _pool.GetFromPool();
        if (!enemy) return;
        enemy.Rotation = spawn.Rotation;
        enemy.Degrees = spawn.Degrees;
        enemy.RotationSpeed = spawn.RotationSpeed;
        enemy.RotationType = spawn.RotationType;
        enemy.transform.SetParent(transform);
        if (ShouldSpawnGameObjectInsteadOfEnemy)
        {
            var moveToCenter = _moveToCenterQueue.Dequeue();
            moveToCenter.transform.position = DirFromAngle(spawn.Rotation) * enemy.StartRadius;
            var rotator = moveToCenter.GetComponent<RotateGameObject>();
            rotator.RotateSpeed = spawn.RotationSpeed;
            moveToCenter.StartMoving(() => Destroy(moveToCenter.gameObject));
        }
        else
        {
            enemy.Initialize();
        }
    }
    public Vector3 DirFromAngle(float angleInDegree, bool angleIsGlobal = true)
    {
        if (!angleIsGlobal)
        {
            angleInDegree += transform.eulerAngles.y;
        }

        return new Vector3(Mathf.Sin(angleInDegree * Mathf.Deg2Rad), Mathf.Cos(angleInDegree * Mathf.Deg2Rad), 0);
    }
}

public class EnemyPool
{
    private readonly List<Enemy> _enemies = new List<Enemy>();

    private readonly Enemy _enemyPrefab;

    public EnemyPool(Enemy enemyPrefab)
    {
        _enemyPrefab = enemyPrefab;
    }

    private Enemy AddToPool(Enemy enemy)
    {
        _enemies.Add(enemy);
        enemy.gameObject.SetActive(false);
        return enemy;
    }

    public Enemy GetFromPool() =>
    _enemies.Find(a => !a.gameObject.activeInHierarchy) ?? AddToPool(UnityEngine.Object.Instantiate(_enemyPrefab));
}


