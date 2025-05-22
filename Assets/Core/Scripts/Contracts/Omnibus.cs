using Moyba.Avatar;
using Moyba.Camera;
using Moyba.Collectibles;
using Moyba.Game;
using Moyba.Input;
using Moyba.SFX;
using Moyba.Terrain;
using Moyba.UI;
using UnityEngine;

namespace Moyba.Contracts
{
    [DefaultExecutionOrder(-99)]
    public class Omnibus : MonoBehaviour
    {
        private static Omnibus _Instance;

        [SerializeField, Require(typeof(IAvatarManager))] private Object _avatar;
        [SerializeField, Require(typeof(ICameraManager))] private Object _camera;
        [SerializeField, Require(typeof(ICollectiblesManager))] private Object _collectibles;
        [SerializeField, Require(typeof(IGameManager))] private Object _game;
        [SerializeField, Require(typeof(IInputManager))] private Object _input;
        [SerializeField, Require(typeof(ISFXManager))] private Object _sfx;
        [SerializeField, Require(typeof(ITerrainManager))] private Object _terrain;
        [SerializeField, Require(typeof(IUIManager))] private Object _ui;

        public static IAvatarManager Avatar { get; private set; }
        public static ICameraManager Camera { get; private set; }
        public static ICollectiblesManager Collectibles { get; private set; }
        public static IGameManager Game { get; private set; }
        public static IInputManager Input { get; private set; }
        public static ISFXManager SFX { get; private set; }
        public static ITerrainManager Terrain { get; private set; }
        public static IUIManager UI { get; private set; }

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
                Omnibus.Camera = (ICameraManager)_camera;
                Omnibus.Collectibles = (ICollectiblesManager)_collectibles;
                Omnibus.Game = (IGameManager)_game;
                Omnibus.Input = (IInputManager)_input;
                Omnibus.SFX = (ISFXManager)_sfx;
                Omnibus.Terrain = (ITerrainManager)_terrain;
                Omnibus.UI = (IUIManager)_ui;
            }
        }

#if UNITY_EDITOR
        private void Reset()
        {
            _avatar = _ContractUtility.LoadOmnibusAsset<IAvatarManager>() as Object;
            _camera = _ContractUtility.LoadOmnibusAsset<ICameraManager>() as Object;
            _collectibles = _ContractUtility.LoadOmnibusAsset<ICollectiblesManager>() as Object;
            _game = _ContractUtility.LoadOmnibusAsset<IGameManager>() as Object;
            _input = _ContractUtility.LoadOmnibusAsset<IInputManager>() as Object;
            _sfx = _ContractUtility.LoadOmnibusAsset<ISFXManager>() as Object;
            _terrain = _ContractUtility.LoadOmnibusAsset<ITerrainManager>() as Object;
            _ui = _ContractUtility.LoadOmnibusAsset<IUIManager>() as Object;
        }
#endif
    }
}
