using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace Game
{
    public class Player : MonoBehaviour
    {
        public static Player Instance { get; private set; }

        [SerializeField] private Animator _animator;
        [SerializeField] private List<Transform> _currentPath;

        [SerializeField] private float _moveSpeed;
        [SerializeField] private float _rotateSpeed;

        [SerializeField] private int _countPathChunks;

        private int _chunksOnPositions;
        private bool _isMove;


        private void Awake()
        {
            if (Instance == null)
                Instance = this;
        }

        private void Update()
        {
            if (_isMove)
            {
                if (_currentPath.Count == 0)
                {
                    _isMove = false;
                    _animator.SetBool("Run Forward", false);

                    return;
                }

                var directionToPoint = (_currentPath[0].position - transform.position).normalized;
                var rotation = Quaternion.LookRotation(directionToPoint).eulerAngles;
                rotation.x = 0;
                rotation.z = 0;

                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(rotation), _rotateSpeed * Time.deltaTime);
                transform.position += transform.forward * _moveSpeed * Time.deltaTime;

                if (Vector3.Distance(transform.position, _currentPath[0].position) < 0.1f)
                    _currentPath.RemoveAt(0);
            }
        }

        public void ChunkOnPosition()
        {
            _chunksOnPositions++;

            if (_chunksOnPositions == _countPathChunks)
            {
                _isMove = true;
                _animator.SetBool("Run Forward", true);
            }
        }
    }
}
