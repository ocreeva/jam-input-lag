using System;
using Moyba.Contracts;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Moyba.Input
{
    public class AvatarInput : ATrait<InputManager>, IAvatarInput
    {
        private static readonly _StubAvatarInput _Stub = new _StubAvatarInput();

        [NonSerialized] private Controls.AvatarActions _avatarActions;

        [NonSerialized] private int _speed;
        [NonSerialized] private int _strafe;
        [NonSerialized] private int _turn;

        internal static IAvatarInput Stub => _Stub;

        public int Speed
        {
            get => _speed;
            set => _Set(value, ref _speed, changed: OnSpeedChanged);
        }

        public int Strafe
        {
            get => _strafe;
            set => _Set(value, ref _strafe, changed: OnStrafeChanged);
        }

        public int Turn
        {
            get => _turn;
            set => _Set(value, ref _turn, changed: OnTurnChanged);
        }

        public event ValueEventHandler<int> OnSpeedChanged;
        public event ValueEventHandler<int> OnStrafeChanged;
        public event ValueEventHandler<int> OnTurnChanged;

        private void Awake()
        {
            this._Assert(ReferenceEquals(_manager.Avatar, _Stub), "is replacing a non-stub instance.");
            _manager.Avatar = this;
            _Stub.TransferControlTo(this);
        }

        private void HandleSpeedChanged(InputAction.CallbackContext context)
        => this.Speed = Mathf.RoundToInt(context.ReadValue<float>());

        private void HandleStrafeChanged(InputAction.CallbackContext context)
        => this.Strafe = Mathf.RoundToInt(context.ReadValue<float>());

        private void HandleTurnChanged(InputAction.CallbackContext context)
        => this.Turn = Mathf.RoundToInt(context.ReadValue<float>());

        private void OnDestroy()
        {
            this._Assert(ReferenceEquals(_manager.Avatar, this), "is stubbing a different instance.");
            _manager.Avatar = _Stub;
            _Stub.TransferControlFrom(this);
        }

        private void OnDisable()
        {
            _avatarActions.Speed.started -= this.HandleSpeedChanged;
            _avatarActions.Speed.canceled -= this.HandleSpeedChanged;
            this.Speed = 0;

            _avatarActions.Strafe.started -= this.HandleStrafeChanged;
            _avatarActions.Strafe.canceled -= this.HandleStrafeChanged;
            this.Strafe = 0;

            _avatarActions.Turn.started -= this.HandleTurnChanged;
            _avatarActions.Turn.canceled -= this.HandleTurnChanged;
            this.Turn = 0;

            _avatarActions.Disable();
        }

        private void OnEnable()
        {
            _avatarActions = _manager.Controls.Avatar;
            _avatarActions.Enable();

            _avatarActions.Speed.started += this.HandleSpeedChanged;
            _avatarActions.Speed.canceled += this.HandleSpeedChanged;
            this.Speed = Mathf.RoundToInt(_avatarActions.Speed.ReadValue<float>());

            _avatarActions.Strafe.started += this.HandleStrafeChanged;
            _avatarActions.Strafe.canceled += this.HandleStrafeChanged;
            this.Strafe = Mathf.RoundToInt(_avatarActions.Strafe.ReadValue<float>());

            _avatarActions.Turn.started += this.HandleTurnChanged;
            _avatarActions.Turn.canceled += this.HandleTurnChanged;
            this.Turn = Mathf.RoundToInt(_avatarActions.Turn.ReadValue<float>());
        }

        private class _StubAvatarInput : ATraitStub<AvatarInput>, IAvatarInput
        {
            public int Speed => 0;
            public int Strafe => 0;
            public int Turn => 0;

            public event ValueEventHandler<int> OnSpeedChanged;
            public event ValueEventHandler<int> OnStrafeChanged;
            public event ValueEventHandler<int> OnTurnChanged;

            protected override void TransferEvents(AvatarInput trait)
            {
                (this.OnSpeedChanged, trait.OnSpeedChanged) = (trait.OnSpeedChanged, this.OnSpeedChanged);
                (this.OnStrafeChanged, trait.OnStrafeChanged) = (trait.OnStrafeChanged, this.OnStrafeChanged);
                (this.OnTurnChanged, trait.OnTurnChanged) = (trait.OnTurnChanged, this.OnTurnChanged);
            }
        }
    }
}
