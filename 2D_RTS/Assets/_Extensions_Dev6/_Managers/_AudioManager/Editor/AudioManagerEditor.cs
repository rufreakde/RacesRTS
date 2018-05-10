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

    [CustomEditor(typeof(AudioManager))]
    public class AudioManagerEditor : Editor
    {
        public bool bDrawDefaultInspector = false;
        private ReorderableList list;
        private AudioManager TargetScript;

        private void OnEnable()
        {
            list = new ReorderableList(serializedObject, serializedObject.FindProperty("MixerGroups"), true, true, true, true);
            TargetScript = (AudioManager)target;
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
            EditorGUILayout.HelpBox("Achtung die Sounds müssen unterhalb dieser standard MixerGroups angeschlossen sein,\ndamit snapshots benutzt werden können! D.h. für Sounds sub-AudioMixerGroups erstellen!!", MessageType.Info);
            if (GUILayout.Button("Method: iInitialize()"))
            {
                TargetScript.iInitialize();
            }
            EditorGUILayout.Space();
            EditorGUILayout.PropertyField(serializedObject.FindProperty("mainMixer"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("mainMixerAssetName"));

            //Acutal Snapshot with button
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.PropertyField(serializedObject.FindProperty("actualSnapshot"));
            if (GUILayout.Button("GoTo Snapshot"))
            {
                //if button is pressed we make transition to the actual snapshot
                TargetScript.GoToSnapshot(TargetScript.ActualSnapshot.name, 1f);
            }
            EditorGUILayout.EndHorizontal();
        }

        void InitReordableList()
        {
            list.drawHeaderCallback = (Rect rect) => {
                EditorGUI.LabelField(rect, "Volume Settings");
            };

            list.elementHeightCallback = (int index) =>
            {
                return list.elementHeight;
            };

            //Only for counting of active properties
            list.drawElementCallback = (Rect rect, int index, bool isActive, bool isFocused) =>
            {
                var element = list.serializedProperty.GetArrayElementAtIndex(index);

                float stored = EditorGUIUtility.labelWidth;
                EditorGUIUtility.labelWidth = 100f;

                EditorGUI.PropertyField
                    (
                    new Rect(rect.x,
                    rect.y,
                    rect.width,
                    EditorGUIUtility.singleLineHeight),
                    element.FindPropertyRelative("volume_dB"),
                    new GUIContent(element.FindPropertyRelative("name").stringValue)
                    );

                //always set the mixer value to the editor set volume value
                if(TargetScript.MainMixer != null)
                    TargetScript.SetMixerGroupVolume(element.FindPropertyRelative("name").stringValue, element.FindPropertyRelative("volume_dB").floatValue);

                EditorGUIUtility.labelWidth = stored;
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