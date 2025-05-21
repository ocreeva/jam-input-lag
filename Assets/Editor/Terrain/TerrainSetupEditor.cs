using System;
using System.Collections.Generic;
using Moyba.Editor;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace Moyba.Terrain.Editor
{
    [CustomEditor(typeof(TerrainSetup))]
    public class TerrainSetupEditor : AnEditor
    {
        private const string _BoundaryContainerName = "Boundary";
        private const string _FloorContainerName = "Floor";

        private static readonly (CoordinateOffset coordinate, Vector3 world, Quaternion rotation)[] _BoundaryOffsets
        = new (CoordinateOffset, Vector3, Quaternion)[]
        {
            (CoordinateOffset.Forward, new Vector3(0, 0, 1), Quaternion.Euler(0, 0, 0)),
            (CoordinateOffset.Backward, new Vector3(0, 0, -1), Quaternion.Euler(0, 0, 0)),
            (CoordinateOffset.Left, new Vector3(-1, 0, 0), Quaternion.Euler(0, 90, 0)),
            (CoordinateOffset.Right, new Vector3(1, 0, 0), Quaternion.Euler(0, 90, 0)),
        };

        private static readonly (CoordinateOffset c1, CoordinateOffset c2, Vector3 world)[] _CornerOffsets
        = new (CoordinateOffset, CoordinateOffset, Vector3)[]
        {
            (CoordinateOffset.Forward, CoordinateOffset.Right, new Vector3(1, 0, 1)),
            (CoordinateOffset.Forward, CoordinateOffset.Left, new Vector3(-1, 0, 1)),
            (CoordinateOffset.Backward, CoordinateOffset.Left, new Vector3(-1, 0, -1)),
            (CoordinateOffset.Backward, CoordinateOffset.Right, new Vector3(1, 0, -1)),
        };

        public override VisualElement CreateInspectorGUI()
        {
            var inspectorGUI = base.CreateInspectorGUI();

            inspectorGUI.Add(this.CreateSerializedPropertyGUI(nameof(TerrainSetup._terrainLayer)));
            inspectorGUI.Add(this.CreateSerializedPropertyGUI(nameof(TerrainSetup._wallOffset)));
            inspectorGUI.Add(this.CreateSerializedPropertyGUI(nameof(TerrainSetup._boundary)));
            inspectorGUI.Add(this.CreateSerializedPropertyGUI(nameof(TerrainSetup._boundaryCorner)));

            var buttonPanel = new VisualElement();
            buttonPanel.style.display = DisplayStyle.Flex;
            buttonPanel.style.flexDirection = FlexDirection.Row;
            buttonPanel.style.alignSelf = Align.Center;
            buttonPanel.style.marginTop = 16;
            inspectorGUI.Add(buttonPanel);

            buttonPanel.Add(this.CreateButton(this.CreateWalls, "Create Boundary"));
            buttonPanel.Add(this.CreateButton(this.ResetBoundary, "Reset Boundary"));

            return inspectorGUI;
        }

        private void CreateWalls()
        {
            var terrainSetup = (TerrainSetup)this.target;
            if (!terrainSetup._boundary) return;
            if (!terrainSetup._boundaryCorner) return;

            var floorContainer = this.GetContainer(_FloorContainerName);
            var floorCoordinates = new Dictionary<Coordinate, float>();
            for (var index = 0; index < floorContainer.childCount; index++)
            {
                var floor = floorContainer.GetChild(index);
                var floorCoordinate = new Coordinate(floor.position);
                if (!floorCoordinates.ContainsKey(floorCoordinate)) floorCoordinates[floorCoordinate] = float.MaxValue;

                floorCoordinates[floorCoordinate] = Mathf.Min(floorCoordinates[floorCoordinate], floor.position.y);
            }

            var offsetMultiplier = 1 + terrainSetup._wallOffset;

            var container = this.GetContainer(_BoundaryContainerName);
            for (var index = 0; index < floorContainer.childCount; index++)
            {
                var floor = floorContainer.GetChild(index);
                var floorCoordinate = new Coordinate(floor.position);

                foreach (var offset in _BoundaryOffsets)
                {
                    if (floorCoordinates.TryGetValue(floorCoordinate + offset.coordinate, out var height)
                        && (height <= floor.position.y)) continue;

                    var boundary = (GameObject)PrefabUtility.InstantiatePrefab(terrainSetup._boundary, container);
                    boundary.transform.position = floor.position + offsetMultiplier * offset.world;
                    boundary.transform.rotation = offset.rotation;
                }

                foreach (var offset in _CornerOffsets)
                {
                    if (floorCoordinates.TryGetValue(floorCoordinate + offset.c1, out var h1)
                        && (h1 <= floor.position.y)) continue;
                    if (floorCoordinates.TryGetValue(floorCoordinate + offset.c2, out var h2)
                        && (h2 <= floor.position.y)) continue;
                    if (floorCoordinates.TryGetValue(floorCoordinate + offset.c1 + offset.c2, out var h12)
                        && (h12 <= floor.position.y)) continue;

                    var corner = (GameObject)PrefabUtility.InstantiatePrefab(terrainSetup._boundaryCorner, container);
                    corner.transform.position = floor.position + offsetMultiplier * offset.world;
                }
            }
        }

        private void ResetBoundary()
        {
            var container = this.GetContainer(_BoundaryContainerName);
            _RemoveAllChildren(container);
        }

        private Transform GetContainer(string name)
        {
            var terrainSetup = (TerrainSetup)this.target;
            var transform = terrainSetup.transform;

            for (var index = 0; index < transform.childCount; index++)
            {
                var child = transform.GetChild(index);
                if (String.Equals(child.name, name)) return child;
            }

            var gameObject = new GameObject(name) { layer = terrainSetup._terrainLayer };
            gameObject.transform.parent = transform;
            return gameObject.transform;
        }

        private static void _RemoveAllChildren(Transform transform)
        {
            while (transform.childCount > 0) UnityEngine.Object.DestroyImmediate(transform.GetChild(0).gameObject);
        }
    }
}
