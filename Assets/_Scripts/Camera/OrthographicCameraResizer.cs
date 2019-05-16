using System.Collections;
using System.Collections.Generic;
using EasyButtons;
using UnityEngine;

namespace Drolegames.Tools
{
    [RequireComponent(typeof(Camera))]
    public class OrthographicCameraResizer : MonoBehaviour
    {

        [Tooltip("The default orthographic size in unity units")]
        [SerializeField] private float _defaultSize = 1f;

        private Camera _camera;

        private void Awake()
        {
            UpdateFromDefault();
        }

        [Button(ButtonSpacing.Before)]
        private void UpdateFromDefault()
        {
            _camera = GetComponent<Camera>();
            if (Screen.width < Screen.height)
            {
                UpdateFromWidthUnits(_defaultSize);
            }
            else
            {
                UpdateFromHeightUnits(_defaultSize);
            }
        }

        public void UpdateFromWidthUnits(float unityUnits)
        {
            _camera.orthographicSize = unityUnits * Screen.height / Screen.width * 0.5f;
        }
        public void UpdateFromHeightUnits(float unityUnits)
        {
            _camera.orthographicSize = unityUnits * 0.5f;
        }
    }
}