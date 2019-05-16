using System;
using UnityEngine.Events;

namespace Drolegames.Events
{
    public interface IGameEvent
    {
        void Invoke();
        void AddListener(UnityAction gameEventListener);
        void RemoveListener(UnityAction gameEventListener);

    }
}