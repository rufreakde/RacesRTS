// File "TransformLock.cs": Copyright by Dev6 Game Studio
// Author: Korbinian Bergauer

using UnityEngine;
using System.Collections;

/// <summary>
/// Class used to lock an object's transform.
/// It is not intended to be derived from because it is executed in edit mode.
/// </summary>
[DisallowMultipleComponent]
[ExecuteInEditMode]
[AddComponentMenu("Dev6/UTILITY/Transform Lock")]
sealed public class TransformLock : MonoBehaviour 
{

    /// <summary>
    /// Sets the object's transform component to not editable.
    /// </summary>
    void Awake()
	{
		#if UNITY_EDITOR
			transform.hideFlags |= HideFlags.NotEditable;
		#else
			Destroy(this);
			return;
		#endif
	}

	/// <summary>
	/// Resets the object's transform component to be editable again.
	/// </summary>
	void OnDestroy()
	{
		#if UNITY_EDITOR
			transform.hideFlags &= ~HideFlags.NotEditable;
        #endif
    }
}