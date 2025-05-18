using System;
using Moyba.Contracts;
using UnityEngine;

namespace Moyba.Avatar
{
    public class AvatarIsGrounded : ABoolValueTrait<AvatarManager>, IBoolValue
    {
        private const string _IsGroundedLiteral = "Is Grounded";
        private static readonly int _IsGrounded = Animator.StringToHash(_IsGroundedLiteral);

        private static readonly _StubAvatarIsGrounded _Stub = new _StubAvatarIsGrounded();

        internal static IBoolValue Stub => _Stub;

        [Header("Components")]
        [SerializeField] private Animator _animator;

        [NonSerialized] private int _terrainCount;

        private void Awake()
        {
            this._Assert(ReferenceEquals(_manager.IsGrounded, _Stub), "is replacing a non-stub instance.");
            _manager.IsGrounded = this;
            _Stub.TransferControlTo(this);
        }

        private void HandleValueChanged(bool isGrounded)
        => _animator.SetBool(_IsGrounded, isGrounded);

        private void OnDestroy()
        {
            this._Assert(ReferenceEquals(_manager.IsGrounded, this), "is stubbing a different instance.");
            _manager.IsGrounded = _Stub;
            _Stub.TransferControlFrom(this);
        }

        private void OnDisable()
        {
            this.OnChanged -= this.HandleValueChanged;
        }

        private void OnEnable()
        {
            this.OnChanged += this.HandleValueChanged;
        }

        private void OnTriggerEnter(Collider other)
        {
            _terrainCount++;
            this.Value = true;
        }

        private void OnTriggerExit(Collider other)
        {
            _terrainCount--;
            this.Value = _terrainCount != 0;
        }

        private class _StubAvatarIsGrounded : ABoolValueTraitStub<AvatarIsGrounded>, IBoolValue { }
    }
}
