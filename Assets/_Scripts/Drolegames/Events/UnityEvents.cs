using System;
using UnityEngine;
using UnityEngine.Events;

namespace Drolegames.Events
{
    [Serializable] public class UnityActionEvent : UnityEvent { }
    [Serializable] public class UnityFloatEvent : UnityEvent<float> { }
    [Serializable] public class UnityIntEvent : UnityEvent<int> { }
    [Serializable] public class UnityBoolEvent : UnityEvent<bool> { }
    [Serializable] public class PickupConfigEvent : UnityEvent<BasePickupableConfig> { }

}