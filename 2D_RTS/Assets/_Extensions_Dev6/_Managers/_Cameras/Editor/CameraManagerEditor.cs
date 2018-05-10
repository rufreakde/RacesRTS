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

    [CustomEditor(typeof(CameraManager))]
    public class CameraManagerEditor : Editor
    {
        public bool bDrawDefaultInspector = false;
        private ReorderableList list;
        private CameraManager TargetScript;

        private void OnEnable()
        {
            list = new ReorderableList(serializedObject, serializedObject.FindProperty("AllManagedCams"), true, true, true, true);
            TargetScript = (CameraManager)target;
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
                //draw custom inspector
                DrawCustomInspector();
            }
        }

        public void DrawCustomInspector()
        {
            serializedObject.Update();
            DrawNotList();
            EditorGUILayout.Space();
            list.DoLayoutList();
            serializedObject.ApplyModifiedProperties();
        }

        void DrawNotList()
        {
            EditorGUILayout.PropertyField(serializedObject.FindProperty("DEBUG"));

            EditorGUILayout.Space();

            float tWidth = EditorGUIUtility.labelWidth;
            EditorGUIUtility.labelWidth = 220f;
            SerializedProperty tBool = serializedObject.FindProperty("ShowQuickCameraCreaton");
            EditorGUILayout.PropertyField(tBool);
            EditorGUIUtility.labelWidth = tWidth;

            if (tBool.boolValue)
            {
                EditorGUILayout.PropertyField(serializedObject.FindProperty("DefaultName"));
                EditorGUILayout.PropertyField(serializedObject.FindProperty("AttachListener"));
                EditorGUILayout.PropertyField(serializedObject.FindProperty("ProjectType"));
                if(GUILayout.Button("Create"))
                {
                    GameObject tGO = new GameObject(TargetScript.DefaultName);
                    Camera _Cam = tGO.AddComponent<Camera>();
                    if (TargetScript.ProjectType == CameraManager.ProjType.Orthographic)
                        _Cam.orthographic = true;
                    else
                        _Cam.orthographic = false;

                    tGO.AddComponent<FlareLayer>(); //TO DO WHAT IS THIS?
                    tGO.AddComponent<GUILayer>(); //TO DO WHAT IS THIS?
                    if (TargetScript.AttachListener)
                        tGO.AddComponent<AudioListener>();
                    tGO.AddComponent<CameraManaged>();

                }
            }
            EditorGUILayout.Space();
            EditorGUILayout.Space();
        }

        void InitReordableList()
        {
            list.drawHeaderCallback = (Rect rect) => {
                EditorGUI.LabelField(rect, "Stored Camera Information");
            };

            list.elementHeightCallback = (int index) =>
            {
                return list.elementHeight;
            };

            //Only for counting of active properties
            list.drawElementCallback = (Rect rect, int index, bool isActive, bool isFocused) =>
            {
                var element = list.serializedProperty.GetArrayElementAtIndex(index);
                EditorGUI.PropertyField
                    (
                    new Rect(rect.x, rect.y, rect.width, EditorGUIUtility.singleLineHeight),
                    element.FindPropertyRelative("Cam"),
                    new GUIContent(element.FindPropertyRelative("Name").stringValue)
                    );
            };

            //higlight the prefab in hierachy
            list.onSelectCallback = (ReorderableList _list) => {
                var prefab = _list.serializedProperty.GetArrayElementAtIndex(_list.index).FindPropertyRelative("Prefab").objectReferenceValue as GameObject;
                if (prefab)
                    EditorGUIUtility.PingObject(prefab.gameObject);
            };

            list.onRemoveCallback = (ReorderableList _list) => {
                if (EditorUtility.DisplayDialog("Warning!",
                    "Are you sure you know what you do? This should not be worked on.", "Yes", "No"))
                {
                    ReorderableList.defaultBehaviours.DoRemoveButton(_list);
                }
            };
        }

        float drawElement(Rect rect, SerializedProperty _Element, string _DrawProperty, string _Label, float _Lines, string _ConditionProperty, bool _ConditionState, bool DontDraw = false)
        {
            if (!DontDraw)
            {
                if (_Element.FindPropertyRelative(_ConditionProperty).boolValue == _ConditionState)
                {
                    
                    return _Lines + 1f; //if its drawn
                }
                else
                    return _Lines;
            }
            else
            {
                if (_Element.FindPropertyRelative(_ConditionProperty).boolValue == _ConditionState)
                    return _Lines + 1f;
                else
                    return _Lines; //if its not drawn
            }
        }
    }
}