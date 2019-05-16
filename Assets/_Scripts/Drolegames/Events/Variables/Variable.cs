using System;
using UnityEngine;
using UnityEngine.Events;

namespace Drolegames.Events
{
    public class Variable<T> : ScriptableObject, ISerializationCallbackReceiver
    {
        [Serializable] public class UnityEventT : UnityEvent<T> { }
        private UnityEventT _onValueChanged = new UnityEventT();
        [SerializeField] private T _initialValue = default;
        public T InitialValue => _initialValue;
        private T _value = default;
        public T Value
        {
            get => _value;
            set
            {
                if (!_value.Equals(value))
                {
                    _value = value;
                    _onValueChanged?.Invoke(_value);
                }
            }
        }
        public void OnAfterDeserialize()
        {
            Value = _initialValue;
        }
        public void OnBeforeSerialize()
        {
        }
        public void SubscribeOnValueChanged(UnityAction<T> action)
        {
            _onValueChanged.AddListener(action);
        }
        public void UnsubscribeOnValueChanged(UnityAction<T> action)
        {
            _onValueChanged.RemoveListener(action);
        }
    }
}