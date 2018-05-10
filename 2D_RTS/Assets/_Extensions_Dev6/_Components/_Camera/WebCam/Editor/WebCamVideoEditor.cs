using UnityEngine;
using System.Collections;
using UnityEditor;

public class WebCamVideoEditor : Editor {


    public bool bDrawDefaultInspector = false;

    private void OnEnable()
    {

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
        //head ik
        EditorGUILayout.Space();
        EditorGUILayout.PropertyField(serializedObject.FindProperty("DEBUG"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("webcamTexture"));
        EditorGUILayout.Space();

        serializedObject.Update();
        serializedObject.ApplyModifiedProperties();
    }

    void DrawCamera()
    {
        //TODO draw camera preview in inspector
    }
}
