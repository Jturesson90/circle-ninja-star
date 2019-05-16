using System;
using System.Collections;
using System.Collections.Generic;
using Drolegames.Events;
using UnityEngine;

[RequireComponent(typeof(CircleBorderMesh), typeof(RotateGameObject))]
public class Enemy : MonoBehaviour
{
    [SerializeField] GameEvent _onEnemyPassed = null;
    public static float ShrinkSpeed = 3f;

    public RotationTypes RotationType { get { return _rotator.RotateType; } set { _rotator.RotateType = value; } }
    public float RotationSpeed { get { return _rotator.RotateSpeed; } set { _rotator.RotateSpeed = value; } }
    public float Rotation { get; set; }
    public int Degrees { get; set; } = 250;
    public float Radius => _circle.Radius;
    public float StartRadius => _circle.StartRadius;

    public bool HitPlayer { get; private set; }

    private CircleBorderMesh _circle;
    private RotateGameObject _rotator;

    private void Awake()
    {
        _circle = GetComponent<CircleBorderMesh>();
        _rotator = GetComponent<RotateGameObject>();
    }

    public void DisableEnemy()
    {
        gameObject.SetActive(false);
    }

    public void Initialize()
    {
        HitPlayer = false;
        _circle.Generate(Degrees);
        transform.eulerAngles = Vector3.zero;
        transform.Rotate(Vector3.forward, Rotation);
        gameObject.SetActive(true);
    }

    private void Update()
    {
        ShrinkRadius();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        var damageable = collision.GetComponent<RecoveringHealth>();
        if (damageable)
        {
            HitPlayer = true;
            damageable.Damage();
        }
    }
    private void ShrinkRadius()
    {
        _circle.UpdateRadius(_circle.Radius - ShrinkSpeed * Time.deltaTime);
        if (_circle.Radius < 1f)
        {
            DisableEnemy();
            if (!HitPlayer)
                _onEnemyPassed?.Invoke();
        }
    }
}


