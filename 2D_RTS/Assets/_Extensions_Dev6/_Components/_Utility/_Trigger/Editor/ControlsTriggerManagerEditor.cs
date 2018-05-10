/*******************
* Rudolf Chrispens *
*******************/

#region using
using UnityEngine;
using System.Collections;
using UnityEditor;
using UnityEditorInternal;
using System;
#endregion

namespace Dev6
{

    [CustomEditor(typeof(ControlsTriggerManager))]
    public class ControlsTriggerManagerEditor : Editor
    {

        public bool bDrawDefaultInspector = false;
        private ReorderableList list;

        private void OnEnable()
        {
            list = new ReorderableList(serializedObject, serializedObject.FindProperty("Triggers"), true, true, true, true);
            InitReordableList();
        }

        public override void OnInspectorGUI()
        {
            if (GUILayout.Button("Toogle Inspector"))
            {
                bDrawDefaultInspector = !bDrawDefaultInspector;
            }

            ShowInformation();

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

        private void ShowInformation()
        {
            //TO DO when InControl is ready change information shown in inspector
            EditorGUILayout.HelpBox("Feuert die Trigger bei Tasteneingaben \nDIESES SCRIPT MUSS 'noch' FÜR JEDE STEUERUNG UMGESCHRIEBEN WERDEN!", MessageType.Info);
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
                EditorGUI.LabelField(rect, "Triggers:");
            };

            list.elementHeight = EditorGUIUtility.singleLineHeight * 1.5f;

            list.drawElementCallback = (Rect rect, int index, bool isActive, bool isFocused) =>
            {
                var element = list.serializedProperty.GetArrayElementAtIndex(index);
                rect.y += 2f;
                DrawSet(element, rect); // draw the element
            };

            //higlight the prefab in hierachy
            list.onSelectCallback = (ReorderableList _list) => {
                var TargetObject = _list.serializedProperty.GetArrayElementAtIndex(_list.index).FindPropertyRelative("Target").objectReferenceValue as GameObject;
                if (TargetObject)
                    EditorGUIUtility.PingObject(TargetObject.gameObject);
            };

            list.onRemoveCallback = (ReorderableList _list) => {
                if (EditorUtility.DisplayDialog("Warning!",
                    "Are you sure you want to delete the selected trigger?", "Yes", "No"))
                {
                    ReorderableList.defaultBehaviours.DoRemoveButton(_list);
                }
            };
        }

        public void DrawSet(SerializedProperty _Element, Rect _Rect)
        {
            float split = _Rect.width / 4f;
            float widthMod = 5f;

            //Target
            EditorGUI.LabelField(
                    new Rect(   _Rect.x,
                                _Rect.y,
                                split,
                                EditorGUIUtility.singleLineHeight),
                    "TargetScript");
            EditorGUI.PropertyField(
                new Rect(   _Rect.x + split * 1f,
                            _Rect.y,
                            split - widthMod,
                            EditorGUIUtility.singleLineHeight),
                _Element.FindPropertyRelative("Target"),
                GUIContent.none);
            
            //Enum Type
            EditorGUI.PropertyField(
                new Rect(   _Rect.x + split * 2f,
                            _Rect.y,
                            split - widthMod,
                            EditorGUIUtility.singleLineHeight),
                _Element.FindPropertyRelative("Event"),
                GUIContent.none);

            //string Trigger
            EditorGUI.LabelField(
                    new Rect(   _Rect.x + split * 3f,
                                _Rect.y,
                                split - widthMod,
                                EditorGUIUtility.singleLineHeight),
                    "Specialtrigger");
            EditorGUI.PropertyField(
                new Rect(   _Rect.x + split * 3f,
                            _Rect.y,
                            split - widthMod,
                            EditorGUIUtility.singleLineHeight),
                _Element.FindPropertyRelative("Trigger"),
                GUIContent.none);
        }
    }
}