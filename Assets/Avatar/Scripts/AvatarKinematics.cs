using System;
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
        [SerializeField] private int _minSpeedStep;
        [SerializeField] private int _maxSpeedStep;

        [NonSerialized] private Animator _animator;

        [NonSerialized] private int _speed;

        private void Awake()
        {
            this._Assert(ReferenceEquals(_manager.Kinematics, _Stub), "is replacing a non-stub instance.");

            _manager.Kinematics = this;

            _Stub.TransferControlTo(this);

            _animator = this.GetComponent<Animator>();

            this.HandleSpeedChanged(0);
        }

        private void HandleSpeedChanged(int delta)
        {
            _speed = Mathf.Clamp(_speed + delta, _minSpeedStep, _maxSpeedStep);

            _animator.SetFloat(_AvatarSpeed, 1f * _speed / _maxSpeedStep);
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
