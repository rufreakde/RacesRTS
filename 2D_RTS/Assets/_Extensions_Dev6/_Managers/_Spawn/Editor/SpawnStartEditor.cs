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

[CustomEditor(typeof(SpawnStart))]
public class SpawnStartEditor : Editor {

        public bool bDrawDefaultInspector = false;
        private ReorderableList list;

        private void OnEnable()
        {
            list = new ReorderableList(serializedObject, serializedObject.FindProperty("StartPrefabs"), true, true, true, true);
            InitReordableList();
        }

        public override void OnInspectorGUI()
        {
            if(GUILayout.Button("Toogle Inspector"))
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
            serializedObject.Update();
            list.DoLayoutList();
            serializedObject.ApplyModifiedProperties();
        }

        void InitReordableList()
        {
            list.drawHeaderCallback = (Rect rect) => {
                EditorGUI.LabelField(rect, "Objects to spawn on SceneStart (Awake)");
            };

            list.elementHeight = EditorGUIUtility.singleLineHeight * 4f;

            list.drawElementCallback = (Rect rect, int index, bool isActive, bool isFocused) => 
            {
                var element = list.serializedProperty.GetArrayElementAtIndex(index);
                rect.y += 2f;

                EditorGUI.LabelField(new Rect(rect.x, rect.y, 60f, EditorGUIUtility.singleLineHeight),"Parent");
                EditorGUI.PropertyField(
                    new Rect(rect.x, rect.y, 60f, EditorGUIUtility.singleLineHeight),
                    element.FindPropertyRelative("Parent"), GUIContent.none);

                EditorGUI.PropertyField(
                    new Rect(rect.x + 60f, rect.y, rect.width - 60f, EditorGUIUtility.singleLineHeight),
                    element.FindPropertyRelative("Prefab"), GUIContent.none);

                EditorGUI.LabelField(new Rect(rect.x, rect.y + EditorGUIUtility.singleLineHeight, 60f, EditorGUIUtility.singleLineHeight), "Default");
                EditorGUI.PropertyField(
                    new Rect(rect.x + 17.5f, rect.y + EditorGUIUtility.singleLineHeight * 2f, 30f, EditorGUIUtility.singleLineHeight),
                    element.FindPropertyRelative("KeepPrefabTransform"), GUIContent.none);

                //draw the transform property only if the transforms should be mondified!
                if (element.FindPropertyRelative("KeepPrefabTransform").boolValue == false)
                {
                    EditorGUI.LabelField(new Rect(rect.x + 60f, rect.y + EditorGUIUtility.singleLineHeight, 60f, EditorGUIUtility.singleLineHeight), "Position");
                    EditorGUI.PropertyField(
                       new Rect(rect.x + 120, rect.y + EditorGUIUtility.singleLineHeight, rect.width - 120f, EditorGUIUtility.singleLineHeight),
                       element.FindPropertyRelative("Position"), GUIContent.none);

                    EditorGUI.LabelField(new Rect(rect.x + 60f, rect.y + EditorGUIUtility.singleLineHeight * 2f, 60f, EditorGUIUtility.singleLineHeight), "Rotation");
                    EditorGUI.PropertyField(
                       new Rect(rect.x + 120, rect.y + EditorGUIUtility.singleLineHeight * 2f, rect.width - 120f, EditorGUIUtility.singleLineHeight),
                       element.FindPropertyRelative("Rotation"), GUIContent.none);
                }
            };

            //higlight the prefab in hierachy
            list.onSelectCallback = (ReorderableList _list) => {
                var prefab = _list.serializedProperty.GetArrayElementAtIndex(_list.index).FindPropertyRelative("Prefab").objectReferenceValue as GameObject;
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
