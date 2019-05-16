using System;
using System.Collections;
using System.Collections.Generic;
using Drolegames.Events;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private GameEvent OnPlayerHit = null;
    [SerializeField] private GameEvent OnPlayerDied = null;
    private PlayerMovement _movement;
    [SerializeField] private LayerMask _enemyLayer;

    [SerializeField] private BoolVariable _isAlive = null;
    public bool IsAlive => _isAlive.Value;

    private void Awake()
    {
        _movement = GetComponent<PlayerMovement>();
    }

    public void Die()
    {
        OnPlayerDied?.Invoke();
        SetMovementEnabled(false);
    }

    public void Initialize()
    {
        IInitializeable[] iis = GetComponents<IInitializeable>();
        for (int i = 0; i < iis.Length; i++)
        {
            var init = iis[i];
            init.Initialize();
        }

    }
    public void Damage()
    {
        if (IsAlive)
            OnPlayerHit?.Invoke();
    }

    public void SetMovementEnabled(bool active)
    {
        _movement.enabled = active;
    }
}
