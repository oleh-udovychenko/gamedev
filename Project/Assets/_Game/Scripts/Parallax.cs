using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class Parallax : MonoBehaviour
    {
        [SerializeField] private Transform _camera;

        [Space]
        [SerializeField] private List<BGObject> _moveBG;

        private Vector3 _lastPosition;

        private void Start()
        {
            _lastPosition = _camera.position;
        }

        private void FixedUpdate()
        {
            foreach (var element in _moveBG)
            {
                element.transform.position += Vector3.right * (_lastPosition.x - _camera.position.x) * element._speedCoefficient;
            }

            _lastPosition = _camera.position;
        }

        [System.Serializable]
        public struct BGObject
        {
            public Transform transform;
            public float _speedCoefficient;
        }
    }
}
