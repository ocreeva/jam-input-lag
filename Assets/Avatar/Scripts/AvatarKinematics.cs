using System;
using System.Collections;
using Moyba.Contracts;
using UnityEngine;

namespace Moyba.Avatar
{
    [RequireComponent(typeof(Animator))]
    public class AvatarKinematics : ATrait<AvatarManager>, IAvatarKinematics
    {
        private const string _AvatarSpeedLiteral = "Speed";
        private static readonly int _AvatarSpeed = Animator.StringToHash(_AvatarSpeedLiteral);

        private static readonly _StubAvatarKinematics _Stub = new _StubAvatarKinematics();

        internal static IAvatarKinematics Stub => _Stub;

        [Header("Configuration")]
        [SerializeField, Range(0f, 10f)] private float _acceleration = 1f;
        [SerializeField, Range(0f, 10f)] private float _deceleration = 1f;
        [SerializeField, Range(-10f, 0f)] private float _minSpeed = 1f;
        [SerializeField, Range(0f, 10f)] private float _maxSpeed = 3f;

        [NonSerialized] private Animator _animator;

        [NonSerialized] private float _speed;
        [NonSerialized] private Coroutine _speedCoroutine;

        private void Awake()
        {
            this._Assert(ReferenceEquals(_manager.Kinematics, _Stub), "is replacing a non-stub instance.");
            _manager.Kinematics = this;
            _Stub.TransferControlTo(this);

            _animator = this.GetComponent<Animator>();

            this.HandleSpeedChanged(0);
        }

        private IEnumerator Coroutine_ApplySpeed(int input)
        {
            var rate = input > 0 ? _acceleration * input : _deceleration * input;
            do
            {
                var delta = rate * Time.deltaTime;
                _speed = Mathf.Clamp(_speed + delta, _minSpeed, _maxSpeed);

                _animator.SetFloat(_AvatarSpeed, _speed / _maxSpeed);

                yield return null;
            }
            while (_speed < _maxSpeed && _speed > _minSpeed);

            _speedCoroutine = null;
        }

        private void HandleSpeedChanged(int input)
        {
            if (_speedCoroutine != null) this.StopCoroutine(_speedCoroutine);

            if (input == 0) return;

            _speedCoroutine = this.StartCoroutine(Coroutine_ApplySpeed(input));
        }

        private void OnDestroy()
        {
            this._Assert(ReferenceEquals(_manager.Kinematics, this), "is stubbing a different instance.");
            _manager.Kinematics = _Stub;
            _Stub.TransferControlFrom(this);
        }

        private void OnDisable()
        {
            Omnibus.Input.Avatar.OnSpeedChanged -= this.HandleSpeedChanged;
        }

        private void OnEnable()
        {
            Omnibus.Input.Avatar.OnSpeedChanged += this.HandleSpeedChanged;
        }

        private class _StubAvatarKinematics : ATraitStub<AvatarKinematics>, IAvatarKinematics { }
    }
}
