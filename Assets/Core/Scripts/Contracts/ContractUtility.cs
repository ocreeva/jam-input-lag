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

        public static T LoadOmnibusAsset<T>()
            where T : class
        {
            var name = _ContractUtility.GetFeatureName<T>();
            var asset = _ContractUtility.LoadAssetAtPath<T>("Assets", name, $"Omnibus.{name}.asset");
            if (asset != null) return asset;

            // try pluralizing the name, for collection entities
            name = $"{name}s";
            return _ContractUtility.LoadAssetAtPath<T>("Assets", name, $"Omnibus.{name}.asset");
        }
#endif

        public static void Set<T>(
            T value,
            ref T field,
            ValueEventHandler<T> changing,
            ValueEventHandler<T> changed,
            bool includeIdempotent)
        {
            if (!includeIdempotent && EqualityComparer<T>.Default.Equals(value, field)) return;

            changing?.Invoke(field);

            field = value;

            changed?.Invoke(field);
        }

        public static void Set(
            bool value,
            ref bool field,
            SimpleEventHandler onFalse,
            SimpleEventHandler onTrue,
            bool includeIdempotent)
        {
            if (!includeIdempotent && value == field) return;

            field = value;

            (value ? onTrue : onFalse)?.Invoke();
        }

#if UNITY_EDITOR
        private static string GetFeatureName<T>()
        {
            var type = typeof(T);
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

        private static T LoadAssetAtPath<T>(params string[] pathSegments)
            where T : class
        {
            var path = Path.Combine(pathSegments);
            return AssetDatabase.LoadMainAssetAtPath(path) as T;
        }
#endif
    }
}
