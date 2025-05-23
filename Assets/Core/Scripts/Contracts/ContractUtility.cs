#if UNITY_EDITOR
using System.Collections.Generic;
using System.IO;
using UnityEditor;
#endif

namespace Moyba.Contracts
{
    internal static class _ContractUtility
    {
#if UNITY_EDITOR
        private static readonly string[] _CommonSuffixes = { "Manager" };

        public static TAsset LoadOmnibusAsset<TAsset>()
            where TAsset : class
        {
            var name = _ContractUtility.GetFeatureName<TAsset>();
            var asset = _ContractUtility.LoadAssetAtPath<TAsset>("Assets", name, $"Omnibus.{name}.asset");
            if (asset != null) return asset;

            // try pluralizing the name, for collection entities
            name = $"{name}s";
            return _ContractUtility.LoadAssetAtPath<TAsset>("Assets", name, $"Omnibus.{name}.asset");
        }
#endif

        public static void Set<TValue>(
            TValue value,
            ref TValue field,
            ValueEventHandler<TValue> onChanged)
        {
            if (EqualityComparer<TValue>.Default.Equals(value, field)) return;

            field = value;

            onChanged?.Invoke(field);
        }

#if UNITY_EDITOR
        private static string GetFeatureName<TAsset>()
        {
            var type = typeof(TAsset);
            var name = type.Name;

            if (type.IsInterface && name.StartsWith('I')) name = name[1..];

            return _ContractUtility.GetFeatureName(name);
        }

        private static string GetFeatureName(string name)
        {
            foreach (var suffix in _CommonSuffixes)
            {
                if (name.EndsWith(suffix)) name = name[..^suffix.Length];
            }

            return name;
        }

        private static TAsset LoadAssetAtPath<TAsset>(params string[] pathSegments)
            where TAsset : class
        {
            var path = Path.Combine(pathSegments);
            return AssetDatabase.LoadMainAssetAtPath(path) as TAsset;
        }
#endif
    }
}
