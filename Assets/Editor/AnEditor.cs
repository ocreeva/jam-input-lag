using System;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace Moyba.Editor
{
    public abstract class AnEditor : UnityEditor.Editor
    {
        public override VisualElement CreateInspectorGUI()
        {
            var inspectorGUI = new VisualElement();

            var scriptField = this.CreateSerializedPropertyGUI("m_Script");
            scriptField.SetEnabled(false);
            inspectorGUI.Add(scriptField);

            return inspectorGUI;
        }

        protected Button CreateButton(Action clickEvent, string text)
        {
            var button = new Button(clickEvent) { text = text };
            button.style.paddingLeft = 16;
            button.style.paddingRight = 16;
            button.style.paddingTop = 8;
            button.style.paddingBottom = 8;
            button.style.marginLeft = 4;
            button.style.marginRight = 4;
            return button;
        }

        protected PropertyField CreateSerializedPropertyGUI(string fieldName)
        => new PropertyField(this.serializedObject.FindProperty(fieldName));
    }
}
