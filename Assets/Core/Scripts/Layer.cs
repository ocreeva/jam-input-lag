using UnityEngine;

namespace Moyba
{
    [System.Serializable]
    public struct Layer
    {
        [SerializeField] internal int _index;

        public Layer(int index) => _index = index;

        public static explicit operator Layer(int index) => new Layer(index);
        public static implicit operator int(Layer layer) => layer.Index;

        public readonly int Index => _index;
        public readonly int Mask => 1 << _index;
    }
}
