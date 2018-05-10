/*******************
* Rudolf Chrispens *
*******************/

#region using
using UnityEngine;
using System.Collections;
using UnityEditor;
#endregion

namespace Dev6
{
    [CustomEditor(typeof(MANAGER))]
    public class SingletonContainerEditor : Editor
    {
        bool ShowDict = true;

        MANAGER Target = null;

        void OnEnable()
        {
            Target = (MANAGER)target;
        }

        public override void OnInspectorGUI()
        {
            base.DrawDefaultInspector();

            //draw dictionary:
            EditorGUIUtility.labelWidth = 6f;
            ShowDict = EditorGUILayout.Foldout(ShowDict, "Dictionary");

            if (ShowDict)
            {
                if (Target.Singletons.Count <= 0)
                {
                    EditorGUILayout.HelpBox("No Data! - Press SetUp or wait for Awake.", MessageType.Info);
                }
                else
                {
                    EditorGUILayout.BeginVertical();
                    for (int i = 0; i < Target.Singletons.Count; i++)
                    {
                        DrawDictLabel(i, Target.Singletons.Keys[i].ToString(), Target.Singletons.Values[i].ToString());
                    }
                    EditorGUILayout.EndVertical();
                }
            }
        }

        void DrawDictLabel(int _ID, string _Key, string _Value)
        {
            GUILayout.BeginHorizontal();
            EditorGUILayout.LabelField(_ID.ToString());
            EditorGUILayout.LabelField(_Key);
            EditorGUILayout.LabelField(_Value);
            GUILayout.EndHorizontal();
        }
    }
}