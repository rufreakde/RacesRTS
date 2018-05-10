using UnityEngine;
using System.Collections;
using UnityEditor;
//using System.Reflection;

[CustomPropertyDrawer(typeof(SliderAttribute))]
public class SliderDrawer : PropertyDrawer
{
    private const float FieldWidth = 48f;
    private const float SliderPadding = 4.0f;

    public float HeightMultiplier = 1f;

    public override void OnGUI(Rect pos, SerializedProperty property, GUIContent label)
    {
        SliderAttribute rAttribute = (SliderAttribute)attribute;

        if(property.propertyType != SerializedPropertyType.Float)
        {
            EditorGUI.HelpBox(pos, "(" + property.type + " " + property.name + ") Has to be of type float!", MessageType.Warning);
            HeightMultiplier = 2.5f;
            return;
        }

        EditorGUI.BeginProperty(pos, label, property);
        {
            // Label
            pos = EditorGUI.PrefixLabel(pos, label);

            // Child objects shouldn't be indented
            int indent = EditorGUI.indentLevel;
            EditorGUI.indentLevel = 0;

            //Draw the different sliders
            if (!rAttribute.Modifiable)
                NormalSlider(rAttribute, pos, property, label);
            else
                ModifiableSlider(rAttribute, pos, property, label);

            // Reset indenting
            EditorGUI.indentLevel = indent;
        }
        EditorGUI.EndProperty();
    }


    void NormalSlider(SliderAttribute rAttribute, Rect pos, SerializedProperty property, GUIContent label)
    {

        // Create rects
        Rect minRect = new Rect(pos.x, pos.y, FieldWidth, pos.height);
        Rect sliderRect = new Rect(minRect.xMax + SliderPadding * 0.5f, pos.y, Mathf.Max(0.0f, pos.width - FieldWidth * 2.0f - SliderPadding), pos.height);
        Rect maxRect = new Rect(sliderRect.xMax, pos.y, FieldWidth, pos.height);

        // Minimum
        EditorGUI.LabelField(minRect, rAttribute.Min.ToString("0.0"));

        // Value slider
        property.floatValue = EditorGUI.Slider(sliderRect, property.floatValue, rAttribute.Min, rAttribute.Max);

        // Maximum
        EditorGUI.LabelField(maxRect, rAttribute.Max.ToString("0.0"));
    }

    void ModifiableSlider(SliderAttribute rAttribute, Rect pos, SerializedProperty property, GUIContent label)
    {

        // Create rects
        Rect minRect = new Rect(pos.x, pos.y, FieldWidth, pos.height);
        Rect sliderRect = new Rect(minRect.xMax + SliderPadding * 0.5f, pos.y, Mathf.Max(0.0f, pos.width - FieldWidth * 2.0f - SliderPadding), pos.height);
        Rect maxRect = new Rect(sliderRect.xMax, pos.y, FieldWidth, pos.height);

        //get the properties
        SerializedProperty spMin = property.serializedObject.FindProperty(rAttribute.PropertyMin);
        SerializedProperty spMax = property.serializedObject.FindProperty(rAttribute.PropertyMax);
        SerializedProperty spValue = property;

        if(spMin == null || spMin.propertyType != SerializedPropertyType.Float)
        {
            if(spMin != null)
                EditorGUI.HelpBox(pos, "(" + spMin.type + " " + spMin.name + ") Is null or not of type float!", MessageType.Error);
            else
                EditorGUI.HelpBox(pos, "PropertyMin == NULL (not found?)", MessageType.Error);

            HeightMultiplier = 2.5f;
            return;
        }

        if (spMax == null || spMax.propertyType != SerializedPropertyType.Float)
        {
            if (spMax != null)
                EditorGUI.HelpBox(pos, "(" + spMax.type + " " + spMax.name + ") Is null or not of type float!", MessageType.Error);
            else
                EditorGUI.HelpBox(pos, "PropertyMax == NULL (not found?)", MessageType.Error);

            HeightMultiplier = 2.5f;
            return;
        }

        // Minimum
        spMin.floatValue = EditorGUI.FloatField(minRect, spMin.floatValue);
        spMin.floatValue = Mathf.Min(spMin.floatValue, spMax.floatValue);

        // Maximum
        spMax.floatValue = EditorGUI.FloatField(maxRect, spMax.floatValue);
        spMax.floatValue = Mathf.Max(spMax.floatValue, spMin.floatValue);

        // Value slider            
        spValue.floatValue = EditorGUI.Slider(sliderRect, spValue.floatValue, spMin.floatValue, spMax.floatValue);

    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        return base.GetPropertyHeight(property, label) * HeightMultiplier;
    }
}
