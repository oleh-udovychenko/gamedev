using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class Parallax : MonoBehaviour
    {
        [SerializeField] private List<BGObject> _moveBG;
        [SerializeField] private Transform _player;

        private Vector3 _lastPosition;

        private void Start()
        {
            _lastPosition = _player.position;
        }

        private void FixedUpdate()
        {
            foreach (var element in _moveBG)
            {
                element.transform.position += Vector3.right * (_lastPosition.x - _player.position.x) * element._speedCoefficient;
            }

            _lastPosition = _player.position;
        }

        [System.Serializable]
        public struct BGObject
        {
            public Transform transform;
            public float _speedCoefficient;
        }
    }
}
