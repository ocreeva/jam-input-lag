using System;
using Moyba.Contracts;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Moyba.Input
{
    public class DebugInput : ATrait<InputManager>, IDebugInput
    {
        private static readonly _StubDebugInput _Stub = new _StubDebugInput();

        [NonSerialized] private Controls.DebugActions _debugActions;

        internal static IDebugInput Stub => _Stub;

        public event SimpleEventHandler OnSignal;

        private void Awake()
        {
            this._Assert(ReferenceEquals(_manager.Debug, _Stub), "is replacing a non-stub instance.");
            _manager.Debug = this;
            _Stub.TransferControlTo(this);

            _debugActions = _manager.Controls.Debug;
        }

        private void HandleSignalPerformed(InputAction.CallbackContext _)
        => this.OnSignal?.Invoke();

        private void OnDestroy()
        {
            this._Assert(ReferenceEquals(_manager.Debug, this), "is stubbing a different instance.");
            _manager.Debug = _Stub;
            _Stub.TransferControlFrom(this);
        }

        private void OnDisable()
        {
            _debugActions.Signal.performed -= this.HandleSignalPerformed;

            _debugActions.Disable();
        }

        private void OnEnable()
        {
            _debugActions.Enable();

            _debugActions.Signal.performed += this.HandleSignalPerformed;
        }

        private class _StubDebugInput : ATraitStub<DebugInput>, IDebugInput
        {
            public event SimpleEventHandler OnSignal;

            protected override void TransferEvents(DebugInput trait)
            {
                (this.OnSignal, trait.OnSignal) = (trait.OnSignal, this.OnSignal);
            }
        }
    }
}
