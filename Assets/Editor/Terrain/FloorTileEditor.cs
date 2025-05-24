using System.Collections.Generic;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace Moyba.Terrain
{
    [CustomEditor(typeof(FloorTile))]
    public class FloorTileEditor : UnityEditor.Editor
    {
        private static readonly Color _CreationHandleColor = Color.white;
        private const float _CreationHandleOffset = 1.25f;
        private const float _CreationHandleSize = 0.5f;

        private bool _isInitialized;
        private IDictionary<int, (Vector3 handle, CoordinateOffset tile)> _creationOffset = new Dictionary<int, (Vector3, CoordinateOffset)>();

        private void OnSceneGUI()
        {
            var floorTile = (FloorTile)this.target;
            if (EditorSceneManager.IsPreviewScene(floorTile.gameObject.scene)) return;

            var transform = floorTile.transform;

            this.Initialize();

            var eventType = Event.current.type;
            switch (eventType)
            {
                case EventType.Layout:
                case EventType.Repaint:
                    foreach (var controlID in _creationOffset.Keys) this.GenerateCreationHandle(controlID, transform, eventType);
                    break;

                case EventType.MouseDown:
                    this.TryCreateNewFloorTile(HandleUtility.nearestControl, transform);
                    break;
            }
        }

        private void GenerateCreationHandle(int controlID, Transform source, EventType eventType)
        {
            var targetCoordinate = new Coordinate(source.position) + _creationOffset[controlID].tile;
            var targetPosition = targetCoordinate.ToWorldPosition();

            var container = source.parent;
            for (var index = 0; index < container.childCount; index++)
            {
                var child = container.GetChild(index);
                if (child.transform.position.x == targetPosition.x && child.transform.position.z == targetPosition.z) return;
            }

            if (eventType == EventType.Repaint) Handles.color = _CreationHandleColor;

            var handleOffset = _creationOffset[controlID].handle;
            Handles.ArrowHandleCap(controlID, source.position + handleOffset, Quaternion.LookRotation(handleOffset), _CreationHandleSize, eventType);
        }

        private bool TryCreateNewFloorTile(int controlID, Transform source)
        {
            if (!_creationOffset.ContainsKey(controlID)) return false;

            var targetCoordinate = new Coordinate(source.position) + _creationOffset[controlID].tile;
            var targetPosition = targetCoordinate.ToWorldPosition();
            targetPosition.y = source.position.y;

            var container = source.parent;

            var prefab = PrefabUtility.GetCorrespondingObjectFromSource(this.target);
            var newFloorTile = (FloorTile)PrefabUtility.InstantiatePrefab(prefab, container);
            newFloorTile.transform.position = targetPosition;
            newFloorTile.gameObject.name = $"Floor Tile ({targetCoordinate})";

            return true;
        }

        private void Initialize()
        {
            if (_isInitialized) return;

            _creationOffset.Add(GUIUtility.GetControlID(FocusType.Passive), (Vector3.forward * _CreationHandleOffset, CoordinateOffset.Forward));
            _creationOffset.Add(GUIUtility.GetControlID(FocusType.Passive), (Vector3.back * _CreationHandleOffset, CoordinateOffset.Backward));
            _creationOffset.Add(GUIUtility.GetControlID(FocusType.Passive), (Vector3.right * _CreationHandleOffset, CoordinateOffset.Right));
            _creationOffset.Add(GUIUtility.GetControlID(FocusType.Passive), (Vector3.left * _CreationHandleOffset, CoordinateOffset.Left));

            _isInitialized = true;
        }
    }
}
