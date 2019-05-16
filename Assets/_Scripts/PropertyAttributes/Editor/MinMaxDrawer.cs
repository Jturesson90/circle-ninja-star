using System;
using UnityEditor;
using UnityEngine;

namespace Drolegames
{
    [CustomPropertyDrawer(typeof(MinMaxAttribute))]
    public class MinMaxDrawer : PropertyDrawer
    {
        private float _horizontalSpacing = 5;
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if (!IsValidProperty(property))
            {
                EditorGUI.LabelField(position, label.text, "Use MinMax with Vector2.");
                return;
            }
            float firstPosition = EditorGUIUtility.labelWidth + 14f;
            float fieldWidth = EditorGUIUtility.fieldWidth;

            var minMax = (MinMaxAttribute)attribute;

            float sliderWidth = (position.xMax - fieldWidth - _horizontalSpacing) - (firstPosition + fieldWidth + _horizontalSpacing);

            Rect minLabelRect;
            Rect minMaxSlider;
            Rect maxLabelRect;

            if (EditorGUIUtility.wideMode)
            {
                minLabelRect = new Rect(firstPosition, position.y, fieldWidth, EditorGUIUtility.singleLineHeight);
                minMaxSlider = new Rect(firstPosition + fieldWidth + _horizontalSpacing, position.y, sliderWidth, EditorGUIUtility.singleLineHeight);
                maxLabelRect = new Rect(position.xMax - fieldWidth, position.y, fieldWidth, EditorGUIUtility.singleLineHeight);
            }
            else
            {
                minMaxSlider = position;
                minMaxSlider.height = EditorGUIUtility.singleLineHeight;
                minLabelRect = new Rect(firstPosition, position.y + EditorGUIUtility.singleLineHeight, fieldWidth, EditorGUIUtility.singleLineHeight);
                maxLabelRect = new Rect(position.xMax - fieldWidth, position.y + EditorGUIUtility.singleLineHeight, fieldWidth, EditorGUIUtility.singleLineHeight);
            }

            EditorGUI.BeginProperty(position, label, property);

            if (property.propertyType == SerializedPropertyType.Vector2Int)
            {
                RenderVector2Int(position, property, label, minMax, minLabelRect, minMaxSlider, maxLabelRect);
            }
            else
            {
                RenderVector2(position, property, label, minMax, minLabelRect, minMaxSlider, maxLabelRect);
            }
            EditorGUI.EndProperty();
        }

        private void RenderVector2Int(Rect position, SerializedProperty property, GUIContent label, MinMaxAttribute minMax, Rect minLabelRect, Rect minMaxSlider, Rect maxLabelRect)
        {
            float minValue = property.vector2IntValue.x;
            float maxValue = property.vector2IntValue.y;
            int minLimit = (int)minMax.minLimit;
            int maxLimit = (int)minMax.maxLimit;

            if (EditorGUIUtility.wideMode)
            {
                EditorGUI.PrefixLabel(position, label);
                minValue = (float)Mathf.Clamp(EditorGUI.IntField(minLabelRect, (int)minValue), minLimit, maxValue);
                EditorGUI.MinMaxSlider(minMaxSlider, ref minValue, ref maxValue, minLimit, maxLimit);
                maxValue = (float)Mathf.Clamp(EditorGUI.IntField(maxLabelRect, (int)maxValue), minValue, maxLimit);
            }
            else
            {
                EditorGUI.MinMaxSlider(minMaxSlider, label, ref minValue, ref maxValue, minLimit, maxLimit);
                minValue = Mathf.Clamp(EditorGUI.IntField(minLabelRect, (int)minValue), minLimit, maxValue);
                maxValue = Mathf.Clamp(EditorGUI.IntField(maxLabelRect, (int)maxValue), minValue, maxLimit);
            }
            property.vector2IntValue = new Vector2Int((int)minValue, (int)maxValue);
        }

        private static void RenderVector2(Rect position, SerializedProperty property, GUIContent label, MinMaxAttribute minMax, Rect minLabelRect, Rect minMaxSlider, Rect maxLabelRect)
        {
            float minValue = property.vector2Value.x;
            float maxValue = property.vector2Value.y;
            float minLimit = minMax.minLimit;
            float maxLimit = minMax.maxLimit;

            if (EditorGUIUtility.wideMode)
            {
                EditorGUI.PrefixLabel(position, label);
                minValue = (float)Math.Round(
                    (double)Mathf.Clamp(
                        EditorGUI.FloatField(minLabelRect, minValue), minLimit, maxValue), 3);
                EditorGUI.MinMaxSlider(minMaxSlider, ref minValue, ref maxValue, minLimit, maxLimit);

                maxValue = (float)Math.Round(
                    (double)Mathf.Clamp(
                        EditorGUI.FloatField(maxLabelRect, maxValue), minValue, maxLimit), 3);
            }
            else
            {
                EditorGUI.MinMaxSlider(minMaxSlider, label, ref minValue, ref maxValue, minLimit, maxLimit);
                minValue = (float)Math.Round(
                (double)Mathf.Clamp(
                    EditorGUI.FloatField(minLabelRect, minValue), minLimit, maxValue), 3);
                maxValue = (float)Math.Round(
                 (double)Mathf.Clamp(
                     EditorGUI.FloatField(maxLabelRect, maxValue), minValue, maxLimit), 3);
            }
            property.vector2Value = new Vector2(minValue, maxValue);
        }

        private bool IsValidProperty(SerializedProperty property)
        {
            if (property.propertyType == SerializedPropertyType.Vector2 || property.propertyType == SerializedPropertyType.Vector2Int)
            {
                return true;
            }
            return false;
        }
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            if (!IsValidProperty(property))
            {
                return EditorGUIUtility.singleLineHeight;
            }

            return (EditorGUIUtility.wideMode ? 0 : EditorGUIUtility.singleLineHeight) // header
            + EditorGUIUtility.singleLineHeight;// first line
            // remaining lines
        }
    }
}