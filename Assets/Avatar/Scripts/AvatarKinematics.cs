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

        private const string _AvatarStrafeLiteral = "Strafe";
        private static readonly int _AvatarStrafe = Animator.StringToHash(_AvatarStrafeLiteral);

        private static readonly Quaternion _Right = Quaternion.Euler(0, 90, 0);

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
        [SerializeField, Range(0f, 90f)] private float _strafeAngle = 45f;
        [SerializeField, Range(0f, 10f)] private float _turnRate = 1.2f;

        [NonSerialized] private Rigidbody _rigidbody;

        [NonSerialized] private float _speed;
        [NonSerialized] private int _speedInput;

        [NonSerialized] private int _strafeInput;

        [NonSerialized] private Quaternion _facing = Quaternion.identity;
        [NonSerialized] private int _turnInput;

        private void Awake()
        {
            this._Assert(ReferenceEquals(_manager.Kinematics, _Stub), "is replacing a non-stub instance.");
            _manager.Kinematics = this;
            _Stub.TransferControlTo(this);

            _rigidbody = this.GetComponent<Rigidbody>();

            _speed = _initialSpeed;
            _facing = this.transform.rotation;
        }

#if UNITY_EDITOR
        private void HandleDebugSignal()
        {
            this.transform.position = Vector3.zero;
        }
#endif

        private void HandleSpeedChanged(int speedInput)
        => _speedInput = speedInput;
        private void HandleStrafeChanged(int strafeInput)
        => _strafeInput = strafeInput;
        private void HandleTurnChanged(int turnInput)
        => _turnInput = turnInput;

        private void OnDestroy()
        {
            this._Assert(ReferenceEquals(_manager.Kinematics, this), "is stubbing a different instance.");
            _manager.Kinematics = _Stub;
            _Stub.TransferControlFrom(this);
        }

        private void OnDisable()
        {
            Omnibus.Input.Avatar.OnSpeedChanged -= this.HandleSpeedChanged;
            Omnibus.Input.Avatar.OnStrafeChanged -= this.HandleStrafeChanged;
            Omnibus.Input.Avatar.OnTurnChanged -= this.HandleTurnChanged;

#if UNITY_EDITOR
            Omnibus.Input.Debug.OnSignal -= this.HandleDebugSignal;
#endif
        }

        private void OnEnable()
        {
            Omnibus.Input.Avatar.OnSpeedChanged += this.HandleSpeedChanged;
            Omnibus.Input.Avatar.OnStrafeChanged += this.HandleStrafeChanged;
            Omnibus.Input.Avatar.OnTurnChanged += this.HandleTurnChanged;

#if UNITY_EDITOR
            Omnibus.Input.Debug.OnSignal += this.HandleDebugSignal;
#endif
        }

        private void FixedUpdate()
        {
            if (_speedInput != 0)
            {
                var rate = _speed > 0
                    ? (_speedInput > 0 ? _positiveAcceleration : _deceleration)
                    : (_speedInput > 0 ? _deceleration : _negativeAcceleration);
                _speed += _speedInput * rate * Time.deltaTime;
                _speed = Mathf.Clamp(_speed, _minSpeed, _maxSpeed);
            }

            if (_turnInput != 0)
            {
                _facing = Quaternion.LerpUnclamped(_facing, _facing * _Right, _turnInput * _turnRate * Time.deltaTime);
            }

            var velocity = _facing * Vector3.forward * _speed;
            if (_strafeInput != 0)
            {
                velocity = Quaternion.Euler(0, _strafeAngle * _strafeInput * Mathf.Sign(_speed), 0) * velocity;
            }

            // TODO: in air
            _rigidbody.linearVelocity = velocity;
        }

        private void Update()
        {
            _animator.SetFloat(_AvatarSpeed, _speed);
            _animator.SetFloat(_AvatarStrafe, _speed * _strafeInput);

            this.transform.rotation = _facing;
        }

        private class _StubAvatarKinematics : ATraitStub<AvatarKinematics>, IAvatarKinematics { }
    }
}
