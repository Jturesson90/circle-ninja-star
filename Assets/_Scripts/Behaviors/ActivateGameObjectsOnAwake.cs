using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateGameObjectsOnAwake : MonoBehaviour
{

    [SerializeField] private List<GameObject> _gameObjects = new List<GameObject>();

    private void Awake()
    {
        foreach (var o in _gameObjects)
        {
            o.SetActive(true);
        }
    }
}
