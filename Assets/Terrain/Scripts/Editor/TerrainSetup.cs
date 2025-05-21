using UnityEngine;

namespace Moyba.Terrain.Editor
{
    public class TerrainSetup : MonoBehaviour
    {
        [Header("Configuration")]
        [SerializeField] internal Layer _terrainLayer;
        [SerializeField, Range(0f, 1f)] internal float _wallOffset;

        [Header("Prefabs")]
        [SerializeField] internal GameObject _boundary;
        [SerializeField] internal GameObject _boundaryCorner;

#if UNITY_EDITOR
        private void Awake()
        => this._Warn("should not be in an active scene.");
#endif
    }
}
