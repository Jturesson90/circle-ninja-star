using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Transform _rotatationOrigin;

    [SerializeField] private float m_movementSpeed = 300f;

    private Vector3 _targetDirection;

    private void Update()
    {
        _targetDirection = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        _targetDirection.Normalize();
    }

    private void FixedUpdate()
    {
        Vector3 vectorToTarget = _rotatationOrigin.position - _targetDirection;
        float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
        var quaternionAngle = Quaternion.AngleAxis(angle, Vector3.forward);
        _rotatationOrigin.rotation = Quaternion.RotateTowards(_rotatationOrigin.rotation, quaternionAngle, Time.deltaTime * m_movementSpeed);
    }
}
