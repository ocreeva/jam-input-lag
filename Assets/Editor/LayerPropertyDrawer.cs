using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace Moyba.Editor
{
    [CustomPropertyDrawer(typeof(Layer))]
    public class LayerPropertyDrawer : PropertyDrawer
    {
        public override VisualElement CreatePropertyGUI(SerializedProperty property)
        {
            var layerField = new LayerField(this.preferredLabel)
            {
                bindingPath = nameof(Layer._index)
            };

            layerField.Align();

            return layerField;
        }
    }
}
