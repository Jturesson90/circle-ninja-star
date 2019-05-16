using UnityEngine;

namespace Drolegames
{
    public class MinMaxAttribute : PropertyAttribute
    {
        public float minLimit = 0;
        public float maxLimit = 1;

        public MinMaxAttribute(float min, float max)
        {
            minLimit = min;
            maxLimit = max;
        }
        public MinMaxAttribute(int min, int max)
        {
            minLimit = min;
            maxLimit = max;
        }

    }
}
