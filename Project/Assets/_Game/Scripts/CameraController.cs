using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private Transform _player;
        [SerializeField] private float _moveSpeed;

        private float _xOffset;

        private void Start()
        {
            _xOffset = _player.transform.position.x - transform.position.x;
        }

        private void FixedUpdate()
        {
            var newPosition = transform.position;
            newPosition.x = _player.position.x + _xOffset;
            transform.position = Vector3.Lerp(transform.position, newPosition, _moveSpeed * Time.fixedDeltaTime);
        }
    }
}
