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

    [CustomEditor(typeof(CCustomAlertButtons))]
    public class CustomAlertButtonsEditor : Editor
    {
        public bool bDrawDefaultInspector = false;
        private ReorderableList List;
        private const float fieldWidth = 70f;

        private void OnEnable()
        {
            List = new ReorderableList(serializedObject,serializedObject.FindProperty("ButtonLogic"),true, true, true, true);
            InitReorderableList();
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
                //draw the reordable lists:
                serializedObject.Update();
                List.DoLayoutList();
                serializedObject.ApplyModifiedProperties();
            }
        }

        void InitReorderableList()
        {
            List.elementHeight = EditorGUIUtility.singleLineHeight * 3f;

            List.drawHeaderCallback = (Rect rect) => {
                EditorGUI.LabelField(rect, "Custom functionality for buttons");
            };

            List.drawElementCallback = (Rect rect, int index, bool isActive, bool isFocused) =>
            {
                var element = List.serializedProperty.GetArrayElementAtIndex(index);
                rect.y += 2f;

                int tQPos = 0;

                //Button
                DrawLabelProp(element, rect, tQPos, "ButtonName", "Button");
                tQPos++;
                //method holder
                DrawLabelPropLeft(element, rect, tQPos, "MethodName", "Method");
                //method name
                DrawLabelPropRight(element, rect, tQPos, "MethodHolder", "> Holder");
            };
        }

        void DrawLabelProp(SerializedProperty _Element, Rect _Rect, int _QueuePosition, string _PropertyName, string _LabelName)
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

        void DrawLabelPropLeft(SerializedProperty _Element, Rect _Rect, int _QueuePosition, string _PropertyName, string _LabelName)
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