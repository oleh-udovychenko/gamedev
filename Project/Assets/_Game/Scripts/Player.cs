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
        [SerializeField] private LayerMask _layerMask;
        [SerializeField] private UIController _ui;

        [SerializeField] private float _hitDistance = 1.5f;

        [SerializeField] private float _speed;
        [SerializeField] private float _attackTime;
        [SerializeField] private float _jumpTime;
        [SerializeField] private float _delayTimeForHit = 0.5f;

        private int _kills;

        private State _currentState;

        #region UnityMethods
        private void Start()
        {
            _currentState = State.Idle;
            _ui.SetKills(_kills);
        }

        private void FixedUpdate()
        {
            if (_currentState == State.Stun || _currentState == State.Attack)
                return;

            if (_joystick.Direction != Vector2.zero)
            {
                Vector2 direction = Vector2.right * _joystick.Direction;
                Move(direction);

                if (_currentState == State.Idle)
                {
                    _knightControl.running();
                    _currentState = State.Run;
                }
            }
            else
            {
                if (_currentState == State.Run)
                {
                    _knightControl.idle();
                    _currentState = State.Idle;
                }
            }
        }
        #endregion

        #region PrivateMethods
        private void Move(Vector3 direction)
        {
            _rigidbody.MovePosition(transform.position + (Time.fixedDeltaTime * _speed * direction));

            transform.rotation = (direction.x > 0f) ? (Quaternion.Euler(Vector3.zero)) : (Quaternion.Euler(Vector3.up * 180f));
        }

        private void Attack(AttakMode mode)
        {


            RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.forward, _hitDistance, _layerMask);
            if (hit)
            {
                var enemy = hit.collider.gameObject.GetComponent<Enemy>();

                switch (mode)
                {
                    case AttakMode.ShieldAttack:
                        enemy.Stun();
                        break;
                    case AttakMode.SwordAttack:
                        enemy.ReceiveDamage();
                        break;
                }

                if (enemy.IsDeath)
                {
                    _kills++;
                    _ui.SetKills(_kills);
                }
            }

            DOVirtual.DelayedCall(_attackTime, () =>
            {
                _knightControl.idle();
                _currentState = State.Idle;
            });
        }
        #endregion

        #region PublicMethods
        public void SwordAttack()
        {
            if (_currentState == State.Attack || _currentState == State.Stun)
                return;

            _currentState = State.Attack;

            _knightControl.attack_2();
            DOVirtual.DelayedCall(_delayTimeForHit, () => Attack(AttakMode.SwordAttack));
        }

        public void ShieldAttack()
        {
            if (_currentState == State.Attack || _currentState == State.Stun)
                return;

            _currentState = State.Attack;

            _knightControl.attack_1();
            DOVirtual.DelayedCall(_delayTimeForHit, () => Attack(AttakMode.ShieldAttack));
        }

        public void Jump()
        {
            if (_currentState == State.Jump || _currentState == State.Stun)
                return;

            _knightControl.jump();
            _currentState = State.Jump;

            DOVirtual.DelayedCall(_jumpTime, () =>
            {
                _currentState = State.Idle;
                _knightControl.idle();
            });
        }
        #endregion

        #region Enums
        public enum State
        {
            Idle,
            Run,
            Jump,
            Attack,
            Stun,
        }

        public enum AttakMode
        {
            ShieldAttack,
            SwordAttack,
        }
        #endregion
    }
}