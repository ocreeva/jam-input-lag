using System;
using Moyba.Contracts;
using UnityEngine;

namespace Moyba.Avatar
{
    [RequireComponent(typeof(Rigidbody))]
    public class AvatarKinematics : ATrait<AvatarManager>, IAvatarKinematics
    {
        private const string _AvatarJumpLiteral = "Jump";
        private static readonly int _AvatarJump = Animator.StringToHash(_AvatarJumpLiteral);

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
        [SerializeField, Range(0f, 100f)] private float _jumpVelocity = 10f;

        [NonSerialized] private Rigidbody _rigidbody;

        [NonSerialized] private float _speed;
        [NonSerialized] private int _speedInput;
        [NonSerialized] private int _strafeInput;
        [NonSerialized] private int _turnInput;

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

        private void HandleJump()
        {
            if (!_manager.IsGrounded.Value) return;

            var velocity = _rigidbody.linearVelocity;
            _rigidbody.linearVelocity = new Vector3(velocity.x, Mathf.Max(velocity.y, _jumpVelocity), velocity.z);

            Omnibus.SFX.Play(SFX.SoundClip.Jump);
        }

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
            Omnibus.Input.Avatar.OnJump -= this.HandleJump;
            Omnibus.Input.Avatar.OnSpeedChanged -= this.HandleSpeedChanged;
            Omnibus.Input.Avatar.OnStrafeChanged -= this.HandleStrafeChanged;
            Omnibus.Input.Avatar.OnTurnChanged -= this.HandleTurnChanged;

#if UNITY_EDITOR
            Omnibus.Input.Debug.OnSignal -= this.HandleDebugSignal;
#endif
        }

        private void OnEnable()
        {
            Omnibus.Input.Avatar.OnJump += this.HandleJump;
            Omnibus.Input.Avatar.OnSpeedChanged += this.HandleSpeedChanged;
            Omnibus.Input.Avatar.OnStrafeChanged += this.HandleStrafeChanged;
            Omnibus.Input.Avatar.OnTurnChanged += this.HandleTurnChanged;

#if UNITY_EDITOR
            Omnibus.Input.Debug.OnSignal += this.HandleDebugSignal;
#endif
        }

        private void FixedUpdate()
        {
            this.FixedUpdate_ApplySpeedInput();
            this.FixedUpdate_ApplyTurnInput();
            this.FixedUpdate_RecalculateVelocity();
            this.FixedUpdate_ApplyStrafeInput();
        }

        private void FixedUpdate_ApplySpeedInput()
        {
            if (_speedInput == 0) return;
            if (!_manager.IsGrounded.Value) return;

            var rate = _speed > 0
                ? (_speedInput > 0 ? _positiveAcceleration : _deceleration)
                : (_speedInput > 0 ? _deceleration : _negativeAcceleration);
            _speed = Mathf.Clamp(_speed + _speedInput * rate * Time.deltaTime, _minSpeed, _maxSpeed);
        }

        private void FixedUpdate_ApplyStrafeInput()
        {
            if (_strafeInput == 0) return;
            if (!_manager.IsGrounded.Value) return;

            _rigidbody.linearVelocity =
                Quaternion.Euler(0, _strafeAngle * _strafeInput * Mathf.Sign(_speed), 0) *
                _rigidbody.linearVelocity;
        }

        private void FixedUpdate_ApplyTurnInput()
        {
            if (_turnInput == 0) return;
            if (!_manager.IsGrounded.Value) return;

            this.transform.rotation = Quaternion.LerpUnclamped(
                this.transform.rotation,
                this.transform.rotation * _Right,
                _turnInput * _turnRate * Time.deltaTime);
        }

        private void FixedUpdate_RecalculateVelocity()
        {
            if (!_manager.IsGrounded.Value) return;

            var velocity = _rigidbody.linearVelocity;
            var horizontalVelocity = this.transform.rotation * Vector3.forward * _speed;

            _rigidbody.linearVelocity = new Vector3(horizontalVelocity.x, velocity.y, horizontalVelocity.z);
        }

        private void Update()
        {
            _animator.SetFloat(_AvatarSpeed, _speed);
            _animator.SetFloat(_AvatarStrafe, _speed * _strafeInput);
        }

        private class _StubAvatarKinematics : ATraitStub<AvatarKinematics>, IAvatarKinematics { }
    }
}
