using Moyba.Contracts;
using UnityEngine;
using UnityEngine.Audio;

namespace Moyba.SFX
{
    public class SFXVolume : AValueTrait<SFXManager, float>, IValueTrait<float>
    {
        private static string _VolumeSetting = $"{nameof(SFXVolume)}_Value";

        private static readonly _StubSFXVolume _Stub = new _StubSFXVolume();

        [Header("Components")]
        [SerializeField] private AudioMixer _audioMixer;

        [Header("Configuration")]
        [SerializeField, Range(0f, 1f)] private float _default = 0.75f;
        [SerializeField, Range(-80, 20)] private float _minDb = -30f;
        [SerializeField, Range(-80, 20)] private float _maxDb = 10f;

        internal static IValueTrait<float> Stub => _Stub;

        private void Awake()
        {
            this._Assert(ReferenceEquals(_manager.Volume, _Stub), "is replacing a non-stub instance.");
            _manager.Volume = this;
            _Stub.TransferControlTo(this);
        }

        private void HandleValueChanged(float value)
        => _audioMixer.SetFloat(_VolumeSetting, value * (_maxDb - _minDb) + _minDb);
        private void HandleValueChanged_Persistence(float value)
        => PlayerPrefs.SetFloat(_VolumeSetting, value);

        private void OnDestroy()
        {
            this._Assert(ReferenceEquals(_manager.Volume, this), "is stubbing a different instance.");
            _manager.Volume = _Stub;
            _Stub.TransferControlFrom(this);
        }

        private void OnDisable()
        {
            this.OnValueChanged -= this.HandleValueChanged_Persistence;

            this.Value = default;

            this.OnValueChanged -= this.HandleValueChanged;
        }

        private void OnEnable()
        {
            this.OnValueChanged += this.HandleValueChanged;

            this.Value = PlayerPrefs.GetFloat(_VolumeSetting, _default);

            this.OnValueChanged += this.HandleValueChanged_Persistence;
        }

        private void Start()
        => this.HandleValueChanged(this.Value);

        private class _StubSFXVolume : AValueTraitStub<SFXVolume>, IValueTrait<float> { }
    }
}
