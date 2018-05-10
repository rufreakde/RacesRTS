/*********************
*	Rudolf Chrispens
***********************/

#region USE
using UnityEngine;
using UnityEditor;
using UnityEngine.Audio;
#endregion

namespace Dev6
{
    //[SelectionBase]
    [CustomEditor(typeof(PlaySoundOn))]
    [CanEditMultipleObjects]
    public class PlaySoundOnEditor : Editor
    {
        private bool bDrawDefaultInspector = false;

        private SerializedProperty DIRECT_SOUND;
        private SerializedProperty _Awake;
        private SerializedProperty _Start;
        private SerializedProperty _OnEnable;
        private SerializedProperty _OnDisable;                                  
        private SerializedProperty _OnClick;
        private SerializedProperty _OnEnter;
        private SerializedProperty _OnExit;

        private SerializedProperty _OnTriggerEnter;
        private SerializedProperty _OnTriggerExit;
        private SerializedProperty _ScriptTrigger;

        private const float LabelWith = 70f;
        private const float FieldWith = 30f;

        void OnEnable()
        {
            DIRECT_SOUND = serializedObject.FindProperty("DIRECT_SOUND");
            _Awake = serializedObject.FindProperty("_Awake");
            _Start = serializedObject.FindProperty("_Start");
            _OnEnable = serializedObject.FindProperty("_OnEnable");
            _OnDisable = serializedObject.FindProperty("_OnDisable");


            _ScriptTrigger = serializedObject.FindProperty("_ScriptTrigger");
            _OnTriggerEnter = serializedObject.FindProperty("_TriggerEnter");
            _OnTriggerExit = serializedObject.FindProperty("_TriggerExit");

            _OnClick = serializedObject.FindProperty("_OnClick");
            _OnEnter = serializedObject.FindProperty("_OnEnter");
            _OnExit = serializedObject.FindProperty("_OnExit");
        }

        //[Split("PlaySoundOnMouseEditor")]
        public override void OnInspectorGUI()
        {
            if (GUILayout.Button("Toogle Inspector"))
            {
                bDrawDefaultInspector = !bDrawDefaultInspector;
            }

            if (!bDrawDefaultInspector)
            {
                //call at the start of OIG
                serializedObject.Update();

                //EditorGUILayout.PropertyField(DIRECT_SOUND, new GUIContent("Direct Sound"));
                if (GUILayout.Button("Play sound directly: " + DIRECT_SOUND.boolValue.ToString()))
                {
                    DIRECT_SOUND.boolValue = !DIRECT_SOUND.boolValue;
                }

                EditorGUIUtility.labelWidth = LabelWith;
                EditorGUIUtility.fieldWidth = FieldWith;

                EditorGUILayout.LabelField("#On all:");
                DrawSet("Awake", _Awake);
                DrawSet("Start", _Start);
                DrawSet("OnEnable", _OnEnable);
                DrawSet("OnDisable", _OnDisable);

                EditorGUILayout.Space();
                EditorGUILayout.LabelField("#Collision:");
                DrawSet("TriggerEnter", _OnTriggerEnter);
                DrawSet("TriggerExit", _OnTriggerExit);

                EditorGUILayout.Space();
                EditorGUILayout.LabelField("#Scriptcall:");
                if(_ScriptTrigger.FindPropertyRelative("Play").boolValue == true)
                    EditorGUILayout.HelpBox("[Trigger] 'No string needed!' ", MessageType.Info);
                DrawSet("ScriptCall", _ScriptTrigger);

                EditorGUILayout.Space();
                EditorGUILayout.LabelField("#UI only!");
                DrawSet("OnClick", _OnClick);
                DrawSet("OnEnter", _OnEnter);
                DrawSet("OnExit", _OnExit);

                //apply serialized property stats
                serializedObject.ApplyModifiedProperties();
            }
            else
                DrawDefaultInspector();
        }

        public void DrawSet(string _Label, SerializedProperty _Set)
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.PropertyField(_Set.FindPropertyRelative("Play"), new GUIContent(_Label));

            if(_Set.FindPropertyRelative("Play").boolValue)
            {
                EditorGUILayout.PropertyField(_Set.FindPropertyRelative("OnlyOnce"), new GUIContent("Only Once"));
                EditorGUILayout.PropertyField(_Set.FindPropertyRelative("Clip"), GUIContent.none );
                EditorGUILayout.PropertyField(_Set.FindPropertyRelative("Source"), GUIContent.none );
                if(DIRECT_SOUND.boolValue != true)
                    EditorGUILayout.PropertyField(_Set.FindPropertyRelative("Mixer"), GUIContent.none);
                EditorGUILayout.PropertyField(_Set.FindPropertyRelative("Loudness"), GUIContent.none);
            }

            EditorGUILayout.EndHorizontal();
        }
    }
}
