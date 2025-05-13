using Moyba.Avatar;
using Moyba.Input;
using Moyba.SFX;
using UnityEngine;

namespace Moyba.Contracts
{
    [DefaultExecutionOrder(-99)]
    public class Omnibus : MonoBehaviour
    {
        private static Omnibus _Instance;

        [SerializeField, Require(typeof(IAvatarManager))] private Object _avatar;
        [SerializeField, Require(typeof(IInputManager))] private Object _input;
        [SerializeField, Require(typeof(ISFXManager))] private Object _sound;

        public static IAvatarManager Avatar { get; private set; }
        public static IInputManager Input { get; private set; }
        public static ISFXManager Sound { get; private set; }

        private void Awake()
        {
            if (Omnibus._Instance)
            {
                if (Omnibus._Instance != this) Object.Destroy(this.gameObject);
            }
            else
            {
                _Instance = this;
                Object.DontDestroyOnLoad(this.gameObject);

                Omnibus.Avatar = (IAvatarManager)_avatar;
                Omnibus.Input = (IInputManager)_input;
                Omnibus.Sound = (ISFXManager)_sound;
            }
        }

#if UNITY_EDITOR
        private void Reset()
        {
            _avatar = _ContractUtility.LoadOmnibusAsset<IAvatarManager>() as Object;
            _input = _ContractUtility.LoadOmnibusAsset<IInputManager>() as Object;
            _sound = _ContractUtility.LoadOmnibusAsset<ISFXManager>() as Object;
        }
#endif
    }
}
