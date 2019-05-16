using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class PickupSpawner : MonoBehaviour
{
    [SerializeField] private float _length = 1f;
    [SerializeField] private Pickup _pickupPrefab = null;
    [SerializeField] private List<BasePickupableConfig> _pickupConfigs = new List<BasePickupableConfig>();


    private Player _player;
    private EnemySpawner _enemySpawner;

    private void Awake()
    {
        _enemySpawner = GameObject.FindObjectOfType<EnemySpawner>();
        _player = GameObject.FindObjectOfType<Player>();
    }

    public void StartSpawn()
    {
        InvokeRepeating("SpawnPickupToStream", 2f, 10f);
    }
    public void StopSpawn()
    {
        CancelInvoke("SpawnPickupToStream");
    }

    private void Start()
    {
        // InvokeRepeating("SpawnPickupToStream", 2f, 3f);
    }

    private void SpawnPickupToStream()
    {
        var go = Instantiate(_pickupPrefab, new Vector3(100, 100, 100), Quaternion.identity, transform);
        go.Initialize(GetRandomPickableConfig());
        _enemySpawner.PushGameObjectToStream(go.gameObject);
    }
    public void SpawnPickup()
    {
        Vector3 position = GetOppositePosition(_player.transform.position);
        var go = Instantiate(_pickupPrefab);

        var oldPos = go.transform.position;
        oldPos.x = position.x;
        oldPos.y = position.y;
        go.transform.position = oldPos;
        go.Initialize(GetRandomPickableConfig());
    }

    private Vector3 GetPosition() => Random.insideUnitCircle.normalized * _length;
    private Vector3 GetOppositePosition(Vector3 target) => target - (target * 2).normalized * _length;

    private BasePickupableConfig GetRandomPickableConfig() => _pickupConfigs[Random.Range(0, _pickupConfigs.Count)];

}
