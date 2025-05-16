using System;
using Moyba.Contracts;
using UnityEngine;

namespace Moyba.Avatar
{
    [RequireComponent(typeof(Rigidbody))]
    public class AvatarKinematics : ATrait<AvatarManager>, IAvatarKinematics
    {
        private const string _AvatarSpeedLiteral = "Speed";
        private static readonly int _AvatarSpeed = Animator.StringToHash(_AvatarSpeedLiteral);

        private static readonly _StubAvatarKinematics _Stub = new _StubAvatarKinematics();

        internal static IAvatarKinematics Stub => _Stub;

        [Header("Components")]
        [SerializeField] private Animator _animator;

        [Header("Configuration")]
        [SerializeField, Range(0f, 10f)] private float _positiveAcceleration = 2.5f;
        [SerializeField, Range(0f, 10f)] private float _negativeAcceleration = 0.5f;
        [SerializeField, Range(0f, 10f)] private float _deceleration = 5f;
        [SerializeField, Range(-10f, 10f)] private float _initialSpeed = 0f;
        [SerializeField, Range(-10f, 0f)] private float _minSpeed = -0.5f;
        [SerializeField, Range(0f, 10f)] private float _maxSpeed = 5f;

        [NonSerialized] private Rigidbody _rigidbody;

        [NonSerialized] private float _speed;
        [NonSerialized] private int _speedDelta;

        private void Awake()
        {
            this._Assert(ReferenceEquals(_manager.Kinematics, _Stub), "is replacing a non-stub instance.");
            _manager.Kinematics = this;
            _Stub.TransferControlTo(this);

            _rigidbody = this.GetComponent<Rigidbody>();

            _speed = _initialSpeed;
        }

#if UNITY_EDITOR
        private void HandleDebugSignal()
        {
            this.transform.position = Vector3.zero;
        }
#endif

        private void HandleSpeedChanged(int speedDelta)
        => _speedDelta = speedDelta;

        private void OnDestroy()
        {
            this._Assert(ReferenceEquals(_manager.Kinematics, this), "is stubbing a different instance.");
            _manager.Kinematics = _Stub;
            _Stub.TransferControlFrom(this);
        }

        private void OnDisable()
        {
            Omnibus.Input.Avatar.OnSpeedChanged -= this.HandleSpeedChanged;

#if UNITY_EDITOR
            Omnibus.Input.Debug.OnSignal -= this.HandleDebugSignal;
#endif
        }

        private void OnEnable()
        {
            Omnibus.Input.Avatar.OnSpeedChanged += this.HandleSpeedChanged;

#if UNITY_EDITOR
            Omnibus.Input.Debug.OnSignal += this.HandleDebugSignal;
#endif
        }

        private void FixedUpdate()
        {
            if (_speedDelta != 0)
            {
                var rate = _speed > 0
                    ? (_speedDelta > 0 ? _positiveAcceleration : _deceleration)
                    : (_speedDelta > 0 ? _deceleration : _negativeAcceleration);
                _speed += _speedDelta * rate * Time.deltaTime;
                _speed = Mathf.Clamp(_speed, _minSpeed, _maxSpeed);
            }

            // TODO: turn
            // TODO: in air
            _rigidbody.linearVelocity = new Vector3(0, 0, _speed);
        }

        private void Update()
        {
            _animator.SetFloat(_AvatarSpeed, _speed);
        }

        private class _StubAvatarKinematics : ATraitStub<AvatarKinematics>, IAvatarKinematics { }
    }
}
