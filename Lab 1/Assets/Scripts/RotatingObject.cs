using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingObject : MonoBehaviour
{
    [SerializeField] private Transform _transformObj;
    [SerializeField] private float _rotationSpeed;

    [SerializeField] private float _timeForDirection;

    private float _startDirectionTime = 0;
    private bool direction = true;

    private void Update()
    {
        if (Input.GetMouseButtonUp(0))
            ChangeDirection();

        if (direction)
            _transformObj.rotation *= Quaternion.Euler(Vector3.one * _rotationSpeed * Time.deltaTime);
        else
            _transformObj.rotation *= Quaternion.Euler(Vector3.one * _rotationSpeed * Time.deltaTime * -1);

        if (Time.time - _startDirectionTime >= _timeForDirection)
            ChangeDirection();
    }

    private void ChangeDirection()
    {
        direction = !direction;
        _startDirectionTime = Time.time;
    }
}
