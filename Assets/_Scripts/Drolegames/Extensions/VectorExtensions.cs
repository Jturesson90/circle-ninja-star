using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Drolegames.Extensions
{
    public static class VectorExtensions
    {
        public static Vector2 RotateAround2D(this Vector3 origin, float length, float degree)
        {
            var radDegree = Mathf.Deg2Rad * degree;
            var x = Mathf.Sin(radDegree);
            var y = Mathf.Cos(radDegree);
            var newVector = new Vector3(x, y, 0) * length;
            return origin + newVector;
        }
    }
}