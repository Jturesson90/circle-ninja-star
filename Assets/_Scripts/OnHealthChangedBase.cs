using System.Collections;
using System.Collections.Generic;
using Drolegames.Events;
using UnityEngine;

public static class OnHealthChangedBase
{
    public static float GetIntensity(float min, float max, float percent) => min + ((max - min) * percent);
}

