/*******************
* Rudolf Chrispens *
*******************/

#region using
using UnityEngine;
using System.Collections;
using UnityEditor;
#endregion

namespace Dev6 {

    [CustomEditor(typeof(StringDataBase))]
    public class StringDataBaseEditor : Editor
    {
        bool ShowDict = true;

        StringDataBase Target = null;

        void OnEnable()
        {
            Target = (StringDataBase)target;
        }

        public override void OnInspectorGUI()
        {
            //draw dictionary:
            EditorGUIUtility.labelWidth = 6f;
            ShowDict = EditorGUILayout.Foldout(ShowDict, "Dictionary");

            if (ShowDict)
            {
                if (Target.Data.Count <= 0)
                {
                    EditorGUILayout.HelpBox("No Data! - Please ensure network connection to the Server!", MessageType.Info);
                }
                else
                {
                    EditorGUILayout.BeginVertical();
                    for (int i = 0; i < Target.Data.Count; i++)
                    {
                        DrawDictLabel(i, Target.Data.Keys[i].ToString(), Target.Data.Values[i].ToString());
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
