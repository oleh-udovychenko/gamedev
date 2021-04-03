using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace Game
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private KnightControl _knightControl;
        [SerializeField] private Joystick _joystick;
        [SerializeField] private Rigidbody2D _rigidbody;

        [SerializeField] private float _speed;
        [SerializeField] private float _attackTime;

        private bool _isRun;
        private bool _isAttack;


        private void FixedUpdate()
        {
            if (_isAttack)
                return;

            if (_joystick.Direction != Vector2.zero)
            {
                Vector2 direction = Vector2.right * _joystick.Direction;
                Move(direction);

                if (!_isRun)
                {
                    _knightControl.running();
                    _isRun = true;
                }
            }
            else
            {
                if (_isRun)
                {
                    _knightControl.idle();
                    _isRun = false;
                }
            }
        }

        public void SwordAttack()
        {
            Debug.Log("A");

            if (_isAttack)
                return;
            Debug.Log("B");
            _knightControl.attack_2();
            _isAttack = true;
            DOVirtual.DelayedCall(_attackTime, () => 
            {
                _isAttack = false;
                _knightControl.idle();
            });
        }

        private void Move(Vector3 direction)
        {
            _rigidbody.MovePosition(transform.position + (Time.fixedDeltaTime * _speed * direction));

            transform.rotation = (direction.x > 0f) ? (Quaternion.Euler(Vector3.zero)) : (Quaternion.Euler(Vector3.up * 180f));
        }
    }
}