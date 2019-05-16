#define DEBUG

using System;
using System.Collections;
using System.Collections.Generic;
using Drolegames.Events;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour, IInitializeable
{
    [Header("Events")]
    [SerializeField] private GameEvent _onGameMenu = null;
    [SerializeField] private GameEvent _onGameStarted = null;
    [SerializeField] private GameEvent _onGameOver = null;

    [Space()]
    [SerializeField] private EnemySpawner _spawner = null;
    [SerializeField] private Player _player = null;
    [SerializeField] private float _gameOverWaitDuration = 5f;

    [SerializeField] private int _targetFrameRate = 60;
    [SerializeField] private BoolVariable _isAlive = null;

    private static GameManager _instance;
    private bool _shouldStartGame;

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            Application.targetFrameRate = _targetFrameRate;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        Initialize();
        StartCoroutine(GameLoop());
    }

    public void Initialize()
    {
        if (_player.isActiveAndEnabled)
        {
            _player.Initialize();
        }
        _spawner.Initialize();
    }

    private IEnumerator GameLoop()
    {
        yield return StartCoroutine(GameMenu());
        yield return StartCoroutine(GameStarting());
        yield return StartCoroutine(GameOver());
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void StartGame()
    {
        _shouldStartGame = true;
    }

    private IEnumerator GameMenu()
    {
        Input.backButtonLeavesApp = true;
        _shouldStartGame = false;

        _onGameMenu?.Invoke();

        while (!_shouldStartGame) yield return null;
    }

    private IEnumerator GameStarting()
    {
        Input.backButtonLeavesApp = false;
        _spawner.StartSpawn();
        _onGameStarted?.Invoke();
        while (_isAlive.Value) yield return null;
    }

    private IEnumerator GameOver()
    {
        _spawner.StopSpawn();
        _onGameOver?.Invoke();

        yield return new WaitForSeconds(_gameOverWaitDuration);
    }
}
