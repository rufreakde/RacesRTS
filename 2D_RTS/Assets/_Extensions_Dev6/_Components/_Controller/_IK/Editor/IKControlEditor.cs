/*******************
* Rudolf Chrispens *
*******************/

#region using
using UnityEngine;
using System.Collections;
using UnityEditor;
using UnityEditorInternal;
#endregion

namespace Dev6
{

    [CustomEditor(typeof(IKControl))]
    public class IKControlEditor : Editor
    {

        public bool bDrawDefaultInspector = false;
        private ReorderableList list;

        private void OnEnable()
        {
            list = new ReorderableList(serializedObject, serializedObject.FindProperty("IKSet"), true, true, true, true);
            InitReordableList();
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
                //head ik
                EditorGUILayout.Space();
                EditorGUILayout.PropertyField(serializedObject.FindProperty("IKHead"));
                EditorGUILayout.PropertyField(serializedObject.FindProperty("LookTarget"));
                EditorGUILayout.Space();
                //draw custom inspector
                DrawCustomInspector();
            }
        }

        public void DrawCustomInspector()
        {
            serializedObject.Update();
            list.DoLayoutList();
            serializedObject.ApplyModifiedProperties();
        }

        void InitReordableList()
        {
            list.drawHeaderCallback = (Rect rect) => {
                EditorGUI.LabelField(rect, "IK Control Editor");
            };

            list.elementHeight = EditorGUIUtility.singleLineHeight * 4f;

            list.drawElementCallback = (Rect rect, int index, bool isActive, bool isFocused) =>
            {
                var element = list.serializedProperty.GetArrayElementAtIndex(index);
                rect.y += 2f;

                EditorGUI.PropertyField(
                    new Rect(rect.x - 16f, rect.y + EditorGUIUtility.singleLineHeight * 1.25f, 20f, EditorGUIUtility.singleLineHeight * 1.05f),
                    element.FindPropertyRelative("IKactive"), GUIContent.none);

                //draw the transform property only if the transforms should be mondified!
                if (element.FindPropertyRelative("IKactive").boolValue == false)
                {
                    EditorGUI.LabelField(new Rect(rect.x + 10f, rect.y, 140f, EditorGUIUtility.singleLineHeight), "Target");
                    EditorGUI.PropertyField(
                        new Rect(rect.x + 140f, rect.y, rect.width - 140f, EditorGUIUtility.singleLineHeight),
                        element.FindPropertyRelative("Handle"), GUIContent.none);

                    EditorGUI.LabelField(new Rect(rect.x + 10f, rect.y + EditorGUIUtility.singleLineHeight * 1.25f, 140f, EditorGUIUtility.singleLineHeight), "Type");
                    EditorGUI.PropertyField(
                        new Rect(rect.x + 140f, rect.y + EditorGUIUtility.singleLineHeight * 1.25f, rect.width - 140f, EditorGUIUtility.singleLineHeight),
                        element.FindPropertyRelative("IKBone"), GUIContent.none);

                    EditorGUI.LabelField(new Rect(rect.x + 10f, rect.y + EditorGUIUtility.singleLineHeight * 1.25f * 2f, 140f, EditorGUIUtility.singleLineHeight), "TransitionValue");
                    EditorGUI.PropertyField(
                        new Rect(rect.x + 140f, rect.y + EditorGUIUtility.singleLineHeight * 1.25f * 2f, rect.width - 140f, EditorGUIUtility.singleLineHeight),
                        element.FindPropertyRelative("TransitionValue"), GUIContent.none);
                }
            };

            //higlight the prefab in hierachy
            list.onSelectCallback = (ReorderableList _list) => {
                var prefab = _list.serializedProperty.GetArrayElementAtIndex(_list.index).FindPropertyRelative("Handle").objectReferenceValue as GameObject;
                if (prefab)
                    EditorGUIUtility.PingObject(prefab.gameObject);
            };

            list.onRemoveCallback = (ReorderableList _list) => {
                if (EditorUtility.DisplayDialog("Warning!",
                    "Are you sure you want to delete the selected object?", "Yes", "No"))
                {
                    ReorderableList.defaultBehaviours.DoRemoveButton(_list);
                }
            };
        }
    }
}
