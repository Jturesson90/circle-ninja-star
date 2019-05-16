using System;
using System.Collections;
using System.Collections.Generic;
using Drolegames.Events;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class GameScoreUI : MonoBehaviour
{
    private Text _scoreText;
    [SerializeField] private IntVariable _score = null;

    private void OnEnable()
    {
        if (_score != null)
            UpdateScore(_score.InitialValue);
        _score?.SubscribeOnValueChanged(UpdateScore);

    }

    private void OnDisable()
    {
        _score?.UnsubscribeOnValueChanged(UpdateScore);
    }

    private void Awake()
    {
        _scoreText = GetComponent<Text>();
    }

    private void UpdateScore(int score)
    {
        _scoreText.text = score.ToString();
    }
}