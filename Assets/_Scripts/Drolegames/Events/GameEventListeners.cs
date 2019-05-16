using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Drolegames.Events
{
    public class GameEventListeners : MonoBehaviour
    {
        [SerializeField] private List<EventListener> _eventListeners = new List<EventListener>();

        private void OnEnable()
        {
            foreach (var item in _eventListeners)
            {
                item.OnEnable();
            }
        }
        private void OnDisable()
        {
            foreach (var item in _eventListeners)
            {
                item.OnDisable();
            }
        }
    }
}