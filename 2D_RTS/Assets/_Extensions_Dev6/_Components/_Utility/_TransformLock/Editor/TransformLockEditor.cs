// File "TransformLockEditor.cs": Copyright by Dev6 Game Studio
// Author: Korbinian Bergauer

using UnityEngine;
using UnityEditor;
using System.Collections;

/// <summary>
/// Custom inspector showing a warning about the locked transform.
/// </summary>
[CustomEditor(typeof(TransformLock))]
[CanEditMultipleObjects]
public class TransformLockEditor : Editor 
{
	[MenuItem("CONTEXT/Transform/Lock", false)]
	static public void Lock()
	{
		for(int i = 0; i < Selection.gameObjects.Length; i++)
		{
			if(Selection.gameObjects[i].GetComponent<TransformLock>() == null)
				Undo.AddComponent<TransformLock>(Selection.gameObjects[i]);
		}
	}

	[MenuItem("CONTEXT/Transform/Unlock", false)]
	static public void Unlock()
	{
		for(int i = 0; i < Selection.gameObjects.Length; i++)
		{
			if(Selection.gameObjects[i].GetComponent<TransformLock>() != null)
				Undo.DestroyObjectImmediate(Selection.gameObjects[i].GetSafeComponent<TransformLock>());
		}
	}

	[MenuItem("CONTEXT/Transform/Lock", true)]
	static public bool ValidLockSelection()
	{
		if(Selection.gameObjects.Length > 0)
		{
			for(int i = 0; i < Selection.gameObjects.Length; i++)
			{
				if(Selection.gameObjects[i].GetComponent<TransformLock>() == null)
					return true;
			}
		}

		return false;
	}

	[MenuItem("CONTEXT/Transform/Unlock", true)]
	static public bool ValidUnlockSelection()
	{
		if(Selection.gameObjects.Length > 0)
		{
			for(int i = 0; i < Selection.gameObjects.Length; i++)
			{
				if(Selection.gameObjects[i].GetComponent<TransformLock>() != null)
					return true;
			}
		}

		return false;
	}



	GUIContent message = null;

	void OnEnable()
	{
		// Prepare inspector message
		Texture icon = (Texture)EditorGUIUtility.Load("icons/console.warnicon.png");
		message = new GUIContent("Transform locked.", icon);

		// Hide tools
		Tools.hidden = true;
	}

	void OnDisable()
	{
		// Unhide tools
		Tools.hidden = false;
	}

	public override bool UseDefaultMargins()
	{
		return false;
	}

	public override void OnInspectorGUI()
	{
		GUIStyle box = GUI.skin.box;

		GUI.skin.box.normal.textColor = EditorStyles.label.normal.textColor;
		GUILayout.Box(message, GUILayout.ExpandWidth(true));
		EditorGUILayout.LabelField("To unlock the transform, remove this component via its context menu.", EditorStyles.wordWrappedLabel);
		//EditorGUILayout.Space();
		//EditorGUILayout.LabelField("Do not derive from this component as that is not intended! It is being executed in edit mode and may lead to unexpected behaviour.", EditorStyles.wordWrappedLabel);

		GUI.skin.box = box;
	}
}