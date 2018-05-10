using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(AutoAssignAttribute))]
[SerializeField]
public class AutoAssignDrawer : PropertyDrawer
{
    float ButtonWidth = 100f;
    float LabelWidth = 0f;
    AutoAssignAttribute Attribute;

    public override void OnGUI(Rect _Position, SerializedProperty _Property, GUIContent _Label)
    {
        //allign the property field with the other fields
        LabelWidth = EditorGUIUtility.labelWidth - ButtonWidth;
        EditorGUIUtility.labelWidth = LabelWidth;
        //EditorGUIUtility.fieldWidth = 0.1f;

        Attribute = (AutoAssignAttribute)attribute;

        if (Attribute.Auto && _Property.objectReferenceValue == null)
        {
            //Custom Auto Button
            Rect Pos1 = new Rect(_Position.x, _Position.y, ButtonWidth, _Position.height);
            Rect Pos2 = new Rect(_Position.x + ButtonWidth, _Position.y, _Position.width - ButtonWidth, _Position.height);
            //default assignable property
            if(GUI.Button(Pos1, "auto"))
            {
                Attribute.Auto = false;
            }
            EditorGUI.LabelField(Pos2, _Property.displayName, _Property.type);
        }
        else
        {
            Rect Pos1 = new Rect (_Position.x, _Position.y, ButtonWidth, _Position.height);
            Rect Pos2 = new Rect (_Position.x + ButtonWidth, _Position.y, _Position.width - ButtonWidth, _Position.height);
            //default assignable property
            if (GUI.Button(Pos1, "manuel"))
            {
                Attribute.Auto = true;
                _Property.objectReferenceValue = null;
            }
            EditorGUI.PropertyField(Pos2, _Property);
        }
    }
}