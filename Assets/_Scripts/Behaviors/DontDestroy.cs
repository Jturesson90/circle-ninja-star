using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Drolegames.Tools
{
    public class DontDestroy : MonoBehaviour
    {
        public static DontDestroy _instance = null;
        private void Awake()
        {
            if (_instance && _instance != this)
            {
                Destroy(gameObject);
                return;
            }
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
}