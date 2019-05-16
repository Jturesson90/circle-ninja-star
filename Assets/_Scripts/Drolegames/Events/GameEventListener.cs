
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


namespace Drolegames.Events
{
    public class GameEventListener : MonoBehaviour
    {
        [SerializeField] private EventListener _eventListener = new EventListener();

        void OnEnable()
        {
            _eventListener.OnEnable();
        }
        void OnDisable()
        {
            _eventListener.OnDisable();
        }
    }

}
