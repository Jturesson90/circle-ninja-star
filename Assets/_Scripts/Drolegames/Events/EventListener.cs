using System;
using UnityEngine;
using UnityEngine.Events;

namespace Drolegames.Events
{
    [Serializable]
    public class EventListener
    {
        [SerializeField] private GameEvent _event = null;
        [SerializeField] private UnityEvent _response = new UnityEvent();
        public void OnEnable()
        {
            _event?.AddListener(_response.Invoke);
        }
        public void OnDisable()
        {
            _event?.RemoveListener(_response.Invoke);
        }
    }
}