
using UnityEngine;
using System.Collections.Generic;
using System;
using UnityEngine.Events;

namespace Drolegames.Events
{
    [CreateAssetMenu]
    public class GameEvent : ScriptableObject, IGameEvent
    {
        private List<UnityAction> _listeners = new List<UnityAction>();
        public void Invoke()
        {
            for (int i = _listeners.Count - 1; i >= 0; i--)
            {
                _listeners[i]?.Invoke();
            }
        }
        public void AddListener(UnityAction gameEventListener)
        {
            _listeners.Add(gameEventListener);
        }
        public void RemoveListener(UnityAction gameEventListener)
        {
            _listeners.Remove(gameEventListener);
        }
    }
}