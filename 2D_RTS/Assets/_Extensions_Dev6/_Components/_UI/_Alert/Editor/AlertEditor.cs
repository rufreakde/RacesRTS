/*******************
* Rudolf Chrispens *
*******************/

#region using
using UnityEngine;
using System.Collections;
using UnityEditor;
using UnityEditorInternal;
#endregion

namespace Dev6 {

    [CustomEditor(typeof(CAlert))]
    public class AlertEditor : Editor
    {
        public bool bDrawDefaultInspector = false;
        private ReorderableList ButtonList;
        private const float fieldWidth = 70f;

        private void OnEnable()
        {
            ButtonList = new ReorderableList(serializedObject, serializedObject.FindProperty("StringSettings").FindPropertyRelative("Buttons"), true, true, true, true);
            InitButtonList();
        }

        public override void OnInspectorGUI()
        {
            if (GUILayout.Button("Toogle Inspector"))
            {
                bDrawDefaultInspector = !bDrawDefaultInspector;
            }

            if (bDrawDefaultInspector)
            {
                //draw default IDE inspector
                DrawDefaultInspector();
            }
            else
            {
                //draw custom inspector
                DrawCustomInspector();
            }
        }

        public void DrawCustomInspector()
        {
            SerializedProperty TextID = serializedObject.FindProperty("StringSettings").FindPropertyRelative("MainID");
            SerializedProperty TextKey = serializedObject.FindProperty("StringSettings").FindPropertyRelative("MainDBKey");
            SerializedProperty Padding = serializedObject.FindProperty("StringSettings").FindPropertyRelative("ButtonPadding");
            EditorGUILayout.Space();

            //draw ID
            //EditorGUILayout.BeginHorizontal();
            //EditorGUILayout.LabelField("Text ID");
            EditorGUILayout.PropertyField(TextID, new GUIContent("Text ID"));
            //EditorGUILayout.EndHorizontal();

            //draw Key
            //EditorGUILayout.BeginHorizontal();
            //EditorGUILayout.LabelField("Text Key");
            EditorGUILayout.PropertyField(TextKey, new GUIContent("Text Key"));
            //EditorGUILayout.EndHorizontal();

            //button padding:
            EditorGUILayout.Space();
            EditorGUILayout.PropertyField(Padding, true);

            //draw the reordable lists:
            ButtonList.DoLayoutList();
            serializedObject.ApplyModifiedProperties();
        }

        void InitButtonList()
        {
            ButtonList.drawHeaderCallback = (Rect rect) => {
                EditorGUI.LabelField(rect, "Buttons");
            };

            ButtonList.elementHeight = EditorGUIUtility.singleLineHeight * 4f;

            ButtonList.drawElementCallback = (Rect rect, int index, bool isActive, bool isFocused) =>
            {
                var element = ButtonList.serializedProperty.GetArrayElementAtIndex(index);
                rect.y += 2f;

                int tQPos = 0;
                //Alert button ID
                DrawLabelPropLeft(element, rect, tQPos, "DbID","ID");
                //dictionary string
                DrawLabelPropRight(element, rect, tQPos, "DbKey", "Key");
                //Font
                tQPos++;   
                DrawLabelPropLeft(element, rect, tQPos, "FontSize", "Font Size");
                DrawLabelPropRight(element, rect, tQPos, "Font", "Font Style");
                //Color and background
                tQPos++;
                DrawLabelPropLeft(element, rect, tQPos, "TextColor", "Text Color");
                DrawLabelPropRight(element, rect, tQPos, "Background", "Btn Sprite");
            };

            ////higlight the prefab in hierachy
            //ButtonList.onSelectCallback = (ReorderableList _list) => {
            //    var prefab = _list.serializedProperty.GetArrayElementAtIndex(_list.index).FindPropertyRelative("Prefab").objectReferenceValue as GameObject;
            //    if (prefab)
            //        EditorGUIUtility.PingObject(prefab.gameObject);
            //};

            //ButtonList.onRemoveCallback = (ReorderableList _list) => {
            //    if (EditorUtility.DisplayDialog("Warning!",
            //        "Are you sure you want to delete the selected object?", "Yes", "No"))
            //    {
            //        ReorderableList.defaultBehaviours.DoRemoveButton(_list);
            //    }
            //};
        }

        void DrawLabelPropLeft(SerializedProperty _Element ,Rect _Rect ,int _QueuePosition, string _PropertyName, string _LabelName)
        {
            //draw label
            EditorGUI.LabelField(
                    new Rect(_Rect.x, _Rect.y + EditorGUIUtility.singleLineHeight * _QueuePosition, fieldWidth, EditorGUIUtility.singleLineHeight),
                    _LabelName
                    );
            //draw property
            EditorGUI.PropertyField(
                    new Rect(_Rect.x + fieldWidth, _Rect.y + EditorGUIUtility.singleLineHeight * _QueuePosition, _Rect.width * 0.5f - fieldWidth, EditorGUIUtility.singleLineHeight),
                    _Element.FindPropertyRelative(_PropertyName), GUIContent.none
                    );
        }

        void DrawLabelPropRight(SerializedProperty _Element, Rect _Rect, int _QueuePosition, string _PropertyName, string _LabelName)
        {
            //draw label
            EditorGUI.LabelField(
                    new Rect(_Rect.x + _Rect.width * 0.5f, _Rect.y + EditorGUIUtility.singleLineHeight * _QueuePosition, fieldWidth, EditorGUIUtility.singleLineHeight),
                    _LabelName
                    );
            //draw property
            EditorGUI.PropertyField(
                    new Rect(_Rect.x + _Rect.width * 0.5f + fieldWidth, _Rect.y + EditorGUIUtility.singleLineHeight * _QueuePosition, _Rect.width * 0.5f - fieldWidth, EditorGUIUtility.singleLineHeight),
                    _Element.FindPropertyRelative(_PropertyName), GUIContent.none
                    );
        }
    }
}