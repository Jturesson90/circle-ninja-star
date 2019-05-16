using System;
using System.Collections;
using System.Collections.Generic;
using Drolegames.Events;
using UnityEngine;

public class ComboManager : MonoBehaviour
{
    [SerializeField] private IntVariable _combo = null;
    [SerializeField] private IntVariable _extraComboPoints = null;
    [SerializeField] private int _combos = 2;

    public int Combo
    {
        get => _combo.Value;
        private set
        {
            _combo.Value = value;
            _extraComboPoints.Value = Mathf.Max((_combo.Value - 1), 0) * _combos;
        }
    }

    private void Start()
    {
        Combo = 0;
    }

    public void OnEnemyPassed()
    {
        Combo++;
    }

    public void OnPlayerHit()
    {
        Combo = 0;
    }
}