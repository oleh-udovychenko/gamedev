using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace Game
{
    public class Enemy : MonoBehaviour
    {
        [SerializeField] private KnightControl _knightControl;

        [SerializeField] private float _resTime = 5f;
        [SerializeField] private float _hitTime = 0.5f;
        [SerializeField] private float _stunTime = 2f;
        [SerializeField] private float _deathTime = 1f;

        [SerializeField] private int _hp = 3;

        private Tween _stateTween;
        private int _startHP;

        public bool IsDeath { get; private set; }

        private void Start()
        {
            _startHP = _hp;
        }

        private void Death()
        {
            _knightControl.death();
            IsDeath = true;

            _stateTween.Kill(false);

            DOVirtual.DelayedCall(_resTime, Revive);
            DOVirtual.DelayedCall(_deathTime, () => gameObject.SetActive(false));
        }

        private void Revive()
        {
            IsDeath = false;
            gameObject.SetActive(true);
            _hp = _startHP;

            _stateTween.Kill(false);

            _knightControl.idle();
        }

        private void ResetState(float time)
        {
            _stateTween = DOVirtual.DelayedCall(time, () => _knightControl.idle());
        }

        public void ReceiveDamage()
        {
            _stateTween.Kill(false);

            _hp--;
            if (_hp > 0)
            {
                _knightControl.getHit();
                ResetState(_hitTime);
            }
            else
                Death();
        }

        public void Stun()
        {
            _knightControl.stun();
            ResetState(_stunTime);
        }
    }
}      
