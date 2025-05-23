using System;
using Moyba.Contracts;
using UnityEngine;

namespace Moyba.Game.Signal
{
    [RequireComponent(typeof(MeshRenderer))]
    public class SignalTransmitter_Model : ATrait<GameManager, SignalTransmitter>
    {
        [Header("Configuration")]
        [SerializeField] private Material _transmittingMaterial;

        [NonSerialized] private MeshRenderer _meshRenderer;

        private void Awake()
        {
            _meshRenderer = this.GetComponent<MeshRenderer>();
        }

        private void HandleTransmitting()
        {
            _meshRenderer.material = _transmittingMaterial;
            _entity.IsTransmitting.OnTrue -= this.HandleTransmitting;
        }

        private void Start()
        {
            _entity.IsTransmitting.OnTrue += this.HandleTransmitting;
        }
    }
}
