using System;
using UnityEngine;

public class MoveToCenter : MonoBehaviour
{
    public bool IsMoving { get; private set; }

    private Action _doneCallback;

    private Vector2 Center = Vector2.zero;
    public void StartMoving(Action doneCallback)
    {
        IsMoving = true;
        _doneCallback = doneCallback;
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        if (!IsMoving) return;
        var speed = Enemy.ShrinkSpeed * Time.deltaTime;
        transform.position = Vector2.MoveTowards(transform.position, Center, speed);

        if (Vector3.Distance(transform.position, Center) < 0.5f)
        {
            IsMoving = false;
            _doneCallback();
        }
    }
}