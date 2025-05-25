using System.Collections;
using Moyba.Contracts;
using UnityEngine;
using UnityEngine.Audio;

namespace Moyba.SFX
{
    public class SFXVolume : AValueTrait<SFXManager, float>, ISFXVolume
    {
        private static string _VolumeSetting = $"{nameof(SFXVolume)}_Value";

        private static readonly _StubSFXVolume _Stub = new _StubSFXVolume();

        [Header("Components")]
        [SerializeField] private AudioMixer _audioMixer;

        [Header("Configuration")]
        [SerializeField, Range(0f, 1f)] private float _default = 0.75f;
        [SerializeField, Range(-80, 20)] private float _minDb = -30f;
        [SerializeField, Range(-80, 20)] private float _maxDb = 10f;
        [SerializeField, Range(0.1f, 10f)] private float _fadeTime = 2f;

        internal static ISFXVolume Stub => _Stub;

        public void Fade()
        => this.StartCoroutine(Coroutine_Fade());

        private void Awake()
        {
            this._Assert(ReferenceEquals(_manager.Volume, _Stub), "is replacing a non-stub instance.");
            _manager.Volume = this;
            _Stub.TransferControlTo(this);
        }

        private IEnumerator Coroutine_Fade()
        {
            _audioMixer.GetFloat(_VolumeSetting, out var currentDb);
            var rate = (_maxDb - _minDb) / _fadeTime;

            while (currentDb > _minDb)
            {
                yield return null;

                currentDb = Mathf.Clamp(currentDb - Time.deltaTime * rate, _minDb, _maxDb);
                _audioMixer.SetFloat(_VolumeSetting, currentDb);
            }
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
            this.OnChanged -= this.HandleValueChanged_Persistence;

            this.Value = default;

            this.OnChanged -= this.HandleValueChanged;
        }

        private void OnEnable()
        {
            this.OnChanged += this.HandleValueChanged;

            this.Value = PlayerPrefs.GetFloat(_VolumeSetting, _default);

            this.OnChanged += this.HandleValueChanged_Persistence;
        }

        private void Start()
        => this.HandleValueChanged(this.Value);

        private class _StubSFXVolume : AValueTraitStub<SFXVolume>, ISFXVolume
        {
            public void Fade() { }
        }
    }
}
