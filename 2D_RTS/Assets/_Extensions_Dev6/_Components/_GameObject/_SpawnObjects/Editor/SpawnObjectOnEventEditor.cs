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

    [CustomEditor(typeof(SpawnObjectOnEvent))]
    public class SpawnObjectOnEventEditor : Editor
    {

        public bool bDrawDefaultInspector = false;
        private ReorderableList list;
        private float[] LinesPerElement = new float[0];

        private void OnEnable()
        {
            list = new ReorderableList(serializedObject, serializedObject.FindProperty("Container"), true, true, true, true);
            LinesPerElement = new float[list.count];
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
            list.DoLayoutList();
            serializedObject.ApplyModifiedProperties();
        }

        void DrawNotList()
        {
            EditorGUILayout.PropertyField(serializedObject.FindProperty("_OnDisable"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("_OnDestroy"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("_OnAwake"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("_OnStart"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("_OnTimed"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("RandomAOA"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("RandomCount"));
        }

        void InitReordableList()
        {
            list.drawHeaderCallback = (Rect rect) => {
                EditorGUI.LabelField(rect, "Spawned objects");
            };

            list.onChangedCallback = (ReorderableList _list) =>
            {
                LinesPerElement = new float[list.count];
            };

            list.elementHeightCallback = (int index) =>
            {
                var element = list.serializedProperty.GetArrayElementAtIndex(index);
                LinesPerElement[index] = DrawAndCountProperties(element, 0f, new Rect(), false, false, true);
                list.elementHeight = EditorGUIUtility.singleLineHeight * LinesPerElement[index];
                return list.elementHeight;
            };

            //Only for counting of active properties
            list.drawElementCallback = (Rect rect, int index, bool isActive, bool isFocused) =>
            {
                var element = list.serializedProperty.GetArrayElementAtIndex(index);
                LinesPerElement[index] = DrawAndCountProperties(element, 0f, rect,isActive,isFocused);
                list.elementHeight = EditorGUIUtility.singleLineHeight * LinesPerElement[index];
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

        float DrawAndCountProperties(SerializedProperty element, float Lines, Rect rect, bool isActive, bool isFocused, bool CountOnlyDontDraw = false)
        {
            Lines = drawElement(rect, element, "Prefab", "Prefab", Lines, CountOnlyDontDraw);
            Lines = drawElement(rect, element, "ObjectName", "Rename", Lines, CountOnlyDontDraw);
            //Lines = drawElement(rect, element, "Tag", "Tag", Lines, CountOnlyDontDraw);
            //Lines = drawElement(rect, element, "Layer", "Layer", Lines, CountOnlyDontDraw);

            Lines = drawElement(rect, element, "RotationRandom", "RotationRandom", Lines, CountOnlyDontDraw);
            Lines = drawElement(rect, element, "RotationDegree", "RotationDegree", Lines, "RotationRandom", false, CountOnlyDontDraw);
            Lines = drawElement(rect, element, "RotationMin", "RotationMin", Lines, "RotationRandom", true, CountOnlyDontDraw);
            Lines = drawElement(rect, element, "RotationMax", "RotationMax", Lines, "RotationRandom", true, CountOnlyDontDraw);

            Lines = drawElement(rect, element, "RandomSpawn", "RandomSpawn", Lines, CountOnlyDontDraw);
            Lines = drawElement(rect, element, "RelativeSpawnPos", "RelativeSpawnPos", Lines, "RandomSpawn", false, CountOnlyDontDraw);
            Lines = drawElement(rect, element, "RandomXmin", "RandomXmin", Lines,"RandomSpawn", true, CountOnlyDontDraw);
            Lines = drawElement(rect, element, "RandomXmax", "RandomXmax", Lines,"RandomSpawn", true, CountOnlyDontDraw);
            Lines = drawElement(rect, element, "RandomYmin", "RandomYmin", Lines,"RandomSpawn", true, CountOnlyDontDraw);
            Lines = drawElement(rect, element, "RandomYmax", "RandomYmax", Lines,"RandomSpawn", true, CountOnlyDontDraw);
            Lines = drawElement(rect, element, "RandomZmin", "RandomZmin", Lines,"RandomSpawn", true, CountOnlyDontDraw);
            Lines = drawElement(rect, element, "RandomZmax", "RandomZmax", Lines,"RandomSpawn", true, CountOnlyDontDraw);

            Lines = drawElement(rect, element, "RandomScale", "RandomScale", Lines, CountOnlyDontDraw);
            Lines = drawElement(rect, element, "ScaleMin", "ScaleMin", Lines, "RandomScale", true, CountOnlyDontDraw);
            Lines = drawElement(rect, element, "ScaleMax", "ScaleMax", Lines, "RandomScale", true, CountOnlyDontDraw);

            //Lines = drawElement(rect, element, "HasRigidbody", "HasRigidbody", Lines,CountOnlyDontDraw);
            //Lines = drawElement(rect, element, "Gravity", "Gravity", Lines, "HasRigidbody", true, CountOnlyDontDraw);
            //Lines = drawElement(rect, element, "IsKinematic", "IsKinematic", Lines, "HasRigidbody", true, CountOnlyDontDraw);
            //Lines = drawElement(rect, element, "RandomForceValue", "RandomForceValue", Lines, "HasRigidbody", true, CountOnlyDontDraw);
            //Lines = drawElement(rect, element, "NotRandomForce", "NotRandomForce", Lines, "RandomForceValue", false, CountOnlyDontDraw);
            //Lines = drawElement(rect, element, "ForceValueRndMin", "ForceValueRndMin", Lines, "RandomForceValue", true, CountOnlyDontDraw);
            //Lines = drawElement(rect, element, "ForceValueRndMax", "ForceValueRndMax", Lines, "RandomForceValue", true, CountOnlyDontDraw);
            //Lines = drawElement(rect, element, "RndForceDirection", "RndForceDirection", Lines, "HasRigidbody", true, CountOnlyDontDraw);
            //Lines = drawElement(rect, element, "PhysicsForceDirection", "PhysicsForceDirection", Lines, "RandomForceValue", false, CountOnlyDontDraw);

            return Lines + 0.5f; // small spacer
        }

        float drawElement(Rect rect, SerializedProperty _Element, string _DrawProperty, string _Label, float _Lines, bool DontDraw = false)
        {
            if (!DontDraw)
            {
                //EditorGUI.LabelField
                //    (
                //    new Rect(rect.x + 10f,
                //    rect.y + (EditorGUIUtility.singleLineHeight * _Lines + EditorGUIUtility.standardVerticalSpacing),
                //    rect.width * 0.2f,
                //    EditorGUIUtility.singleLineHeight),
                //    _Label
                //    );

                EditorGUI.PropertyField
                    (
                    new Rect(rect.x,
                    rect.y + (EditorGUIUtility.singleLineHeight * _Lines + EditorGUIUtility.standardVerticalSpacing),
                    rect.width,
                    EditorGUIUtility.singleLineHeight),
                    _Element.FindPropertyRelative(_DrawProperty),
                    new GUIContent(_Label)
                    );
            }
            return _Lines + 1f; //always drawn because there is no condition
        }

        float drawElement(Rect rect, SerializedProperty _Element, string _DrawProperty, string _Label, float _Lines, string _ConditionProperty, bool _ConditionState, bool DontDraw = false)
        {
            if (!DontDraw)
            {
                if (_Element.FindPropertyRelative(_ConditionProperty).boolValue == _ConditionState)
                {
                    EditorGUI.PropertyField
                    (
                    new Rect(rect.x,
                    rect.y + (EditorGUIUtility.singleLineHeight * _Lines + EditorGUIUtility.standardVerticalSpacing),
                    rect.width,
                    EditorGUIUtility.singleLineHeight),
                    _Element.FindPropertyRelative(_DrawProperty),
                    new GUIContent(_Label)
                    );
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