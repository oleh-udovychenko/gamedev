using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleObject : MonoBehaviour
{
    [SerializeField] private Transform _transformObj;
    [SerializeField] private float _scaleSpeed;

    [SerializeField] private float _maxScale;
    [SerializeField] private float _minScale;

    private bool _increase = true;

    private void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            _increase = !_increase;
        }

        if (_increase)
        {
            _transformObj.localScale += Vector3.one * _scaleSpeed * Time.deltaTime;

            if (_transformObj.localScale.x >= _maxScale)
                _increase = false;
        }
        else
        {
            _transformObj.localScale -= Vector3.one * _scaleSpeed * Time.deltaTime;

            if (_transformObj.localScale.x <= _minScale)
                _increase = true;
        }
    }
}
