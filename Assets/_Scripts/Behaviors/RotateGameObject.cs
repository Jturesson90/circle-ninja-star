using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateGameObject : MonoBehaviour
{
    [SerializeField] private float _rotateSpeed = 1f;

    public float RotateSpeed { get { return _rotateSpeed; } set { _rotateSpeed = value; } }

    private RotationTypes _realRotationType;
    private RotationTypes _rotationType;
    public RotationTypes RotateType
    {
        get { return _rotationType; }
        set
        {
            _rotationType = value;
            _realRotationType = value;
            if (value == RotationTypes.Random)
            {
                _realRotationType = Random.Range(0, 2) == 0 ? RotationTypes.Left : RotationTypes.Right;
            }
        }
    }

    private void Update()
    {
        switch (_realRotationType)
        {
            case RotationTypes.Left:
                transform.Rotate(Vector3.forward * Time.deltaTime * -RotateSpeed, Space.World);
                break;
            case RotationTypes.Right:
                transform.Rotate(Vector3.forward * Time.deltaTime * RotateSpeed, Space.World);
                break;
            default:
                break;
        }

    }
}
