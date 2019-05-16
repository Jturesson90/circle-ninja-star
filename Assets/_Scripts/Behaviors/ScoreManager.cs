using System;
using System.Collections;
using System.Collections.Generic;
using Drolegames.Events;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public bool CanCountScore { get; set; } = true;
    [SerializeField] private int _pointsPerEnemy = 10;
    [SerializeField] private IntVariable _extraComboPoints = null;
    [SerializeField] private IntVariable _score = null;
    public int GameScore
    {
        get
        {
            return _score.Value;
        }
        set
        {
            if (CanCountScore)
            {
                _score.Value = value;
            }
        }
    }

    void Start()
    {
        GameScore = 0;
    }

    public void Initialize()
    {
        GameScore = 0;
    }

    public void AddScore()
    {
        GameScore += _pointsPerEnemy + _extraComboPoints.Value;
    }
}
