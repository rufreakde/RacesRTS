using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(ShowOnlyAttribute))]
[SerializeField]
public class ShowOnlyDrawer : PropertyDrawer
{
    public override void OnGUI(Rect _Position, SerializedProperty _Property, GUIContent _Label)
    {

        if(_Property.propertyType == SerializedPropertyType.Float)
        {
            EditorGUI.LabelField(_Position,_Property.displayName,_Property.floatValue.ToString("0.00000"));
        }
        else if (_Property.propertyType == SerializedPropertyType.Boolean)
        {
            EditorGUI.LabelField(_Position, _Property.displayName, _Property.boolValue.ToString());
        }
        else if (_Property.propertyType == SerializedPropertyType.Enum)
        {
            EditorGUI.LabelField(_Position, _Property.displayName, _Property.enumDisplayNames[_Property.enumValueIndex]);
        }
        else if (_Property.propertyType == SerializedPropertyType.Integer) 
        {
            EditorGUI.LabelField(_Position, _Property.displayName, _Property.intValue.ToString());
        }
        else if (_Property.propertyType == SerializedPropertyType.String)
        {
            EditorGUI.LabelField(_Position, _Property.displayName, _Property.stringValue);
        }
        else if (_Property.propertyType == SerializedPropertyType.Vector2)
        {
            EditorGUI.LabelField(_Position, _Property.displayName, _Property.vector2Value.ToString());
        }
        else if (_Property.propertyType == SerializedPropertyType.Vector3)
        {
            EditorGUI.LabelField(_Position, _Property.displayName, _Property.vector3Value.ToString());
        }
        else if (_Property.propertyType == SerializedPropertyType.Vector4)
        {
            EditorGUI.LabelField(_Position, _Property.displayName, _Property.vector4Value.ToString());
        }
        else if (_Property.propertyType == SerializedPropertyType.Rect)
        {
            EditorGUI.LabelField(_Position, _Property.displayName, _Property.rectValue.ToString());
        }
        else if (_Property.propertyType == SerializedPropertyType.Quaternion)
        {
            EditorGUI.LabelField(_Position, _Property.displayName, _Property.quaternionValue.ToString());
        }
        else
        {
            //safe
            Color Colortemp = GUI.color;

            //set for error
            GUI.color = Color.red;
            EditorGUI.LabelField(_Position, "ERROR: [OnlyShowValue] "+ _Property.name,"[TYPE]"+ _Property.propertyType.ToString());

            //reset to default just in case
            GUI.color = Colortemp;
        }
    }
}