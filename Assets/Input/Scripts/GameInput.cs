using System;
using Moyba.Contracts;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Moyba.Input
{
    public class GameInput : ATrait<InputManager>, IGameInput
    {
        private static readonly _StubGameInput _Stub = new _StubGameInput();

        [NonSerialized] private Controls.GameActions _gameActions;

        public event SimpleEventHandler OnPause;

        internal static IGameInput Stub => _Stub;

        private void Awake()
        {
            this._Assert(ReferenceEquals(_manager.Game, _Stub), "is replacing a non-stub instance.");
            _manager.Game = this;
            _Stub.TransferControlTo(this);

            _gameActions = _manager.Controls.Game;
        }

        private void HandlePause(InputAction.CallbackContext _)
        => this.OnPause?.Invoke();

        private void OnDestroy()
        {
            this._Assert(ReferenceEquals(_manager.Game, this), "is stubbing a different instance.");
            _manager.Game = _Stub;
            _Stub.TransferControlFrom(this);
        }

        private void OnDisable()
        {
            _gameActions.Pause.performed -= this.HandlePause;

            _gameActions.Disable();
        }

        private void OnEnable()
        {
            _gameActions.Enable();

            _gameActions.Pause.performed += this.HandlePause;
        }

        private class _StubGameInput : ATraitStub<GameInput>, IGameInput
        {
            public event SimpleEventHandler OnPause;

            protected override void TransferEvents(GameInput trait)
            {
                (this.OnPause, trait.OnPause) = (trait.OnPause, this.OnPause);
            }
        }
    }
}
