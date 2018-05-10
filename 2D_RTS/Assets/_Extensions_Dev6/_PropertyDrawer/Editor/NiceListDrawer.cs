using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

/*
*   NOTE:
*   Here is a lot of hacking going on!
*   The _Position and _Property in OnGUI is wrong UNITY BUG! it gives the values of elements not of the array itself!
*   Also there is still the default array drawn! wtf?? This is totaly buggy but it works xD
*/

[CustomPropertyDrawer(typeof(NiceListAttribute))]
public class NiceListDrawer : PropertyDrawer
{
    private ReorderableList LIST;
    private float BaseDeltaY = 3f;

    public override void OnGUI(Rect _Position, SerializedProperty _Property, GUIContent _Label)
    {
        _Property.serializedObject.Update();

        SerializedProperty Prop = _Property.serializedObject.FindProperty(fieldInfo.Name);
        if (LIST == null)
        {
            LIST = new ReorderableList(_Property.serializedObject, Prop, true, true, true, true);
        }

        LIST.drawHeaderCallback += rect => GUI.Label(rect, Prop.name);
        LIST.drawElementCallback += (rect, index, active, focused) =>
        {
            rect.height = 16;
            rect.y += 2;
            EditorGUI.PropertyField(rect, LIST.serializedProperty.GetArrayElementAtIndex(index), GUIContent.none);
        };
        //HACKS
            //i think that Regex is windows specific but this is a editor script so dont worry :D
            string DataIDstring = System.Text.RegularExpressions.Regex.Replace(_Property.propertyPath, "[^0-9]", ""); // parse the ID of array index
            int DataIDint; // init array index als int
            int.TryParse(DataIDstring, out DataIDint); //get the int

            float YCorrection = (LIST.elementHeight - BaseDeltaY) * DataIDint;
            Rect DeltaYRect = new Rect(_Position.x, _Position.y - YCorrection, _Position.width, _Position.height);

        //CHeck the rects!
        //Debug.Log("New Rect: " + DeltaYRect);

        Debug.LogError("Do not use [NiceList] Tag! Its bugged because of unity! " + Prop.name);

        LIST.DoList(DeltaYRect);
        _Property.serializedObject.ApplyModifiedProperties();
    }
}