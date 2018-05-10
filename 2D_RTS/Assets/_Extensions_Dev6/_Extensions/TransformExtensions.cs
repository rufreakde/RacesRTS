/*********************
*	Rudolf Chrispens
***********************/

#region USE
using UnityEngine;
using System.Collections;
#endregion

public enum AXIS
{
    X = 0,
    Y = 1,
    Z = 2,
    _COUNT,
};


public static class TransformExtensions 
{
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /********************
     * ON TRANSFORMS    *
     ********************/
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    #region TRANSFORM

    /// <summary>
    /// Set the layer of all child objects of target Transform.
    /// </summary>
    /// <param name="_Transform">The transform target Object.</param>
    /// <param name="layer">Integer for Layer.</param>
    public static void SetLayerRecursively(this Transform _Transform, int layer)
    {
        _Transform.gameObject.layer = layer;
        foreach (Transform t in _Transform)
            t.gameObject.SetLayerRecursively(layer);
    }

    /// <summary>
    /// Set the layer of all children, of target Transform.
    /// </summary>
    /// <param name="_Transform">Target Transform.</param>
    /// <param name="_Tag">New Tag.</param>
    public static void SetTagRecursively(this Transform _Transform, string _Tag)
    {
        _Transform.tag = _Tag;
        foreach (Transform t in _Transform)
            t.SetTagRecursively(_Tag);
    }


    /// <summary>
    /// Sets only the x value of the specified transforms position.
    /// </summary>
    /// <param name="_Transform">The transform to modify.</param>
    /// <param name="_X">The new x value.</param>
    public static void SetPositionX(this Transform _Transform, float _X)
    {
        _Transform.position = new Vector3(_X, _Transform.position.y, _Transform.position.z);
    }
    /// <summary>
    /// Sets only the y value of the specified transforms position.
    /// </summary>
    /// <param name="_Transform">The transform to modify.</param>
    /// <param name="_Y">The new y value.</param>
	public static void SetPositionY(this Transform _Transform, float _Y)
    {
        _Transform.position = new Vector3(_Transform.position.x, _Y, _Transform.position.z);
    }
    /// <summary>
    /// Sets only the z value of the specified transforms position.
    /// </summary>
    /// <param name="_Transform">The transform to modify.</param>
    /// <param name="_Z">The new z value.</param>
	public static void SetPositionZ(this Transform _Transform, float _Z)
    {
        _Transform.position = new Vector3(_Transform.position.x, _Transform.position.y, _Z);
    }
    /// <summary>
    /// Sets all values of transforms.position.
    /// </summary>
    /// <param name="_Transform">The transform to modify.</param>
    /// <param name="_X">The new x value.</param>
    /// <param name="_Y">The new y value.</param>
    /// <param name="_Z">The new z value.</param>
	public static void SetPosition(this Transform _Transform, float _X, float _Y, float _Z)
    {
        _Transform.position = new Vector3(_X, _Y, _Z);
    }

    /// <summary>
    /// Return the current value of X (transforms.position).
    /// </summary>
    /// <param name="_Transform">The transform target Object.</param>
	public static float GetPositionX(this Transform _Transform)
    {
        return _Transform.position.x;
    }
    /// <summary>
    /// Return the current value of Y (transforms.position).
    /// </summary>
    /// <param name="_Transform">The transform target Object.</param>
	public static float GetPositionY(this Transform _Transform)
    {
        return _Transform.position.y;
    }
    /// <summary>
    /// Return the current value of Z (transforms.position).
    /// </summary>
    /// <param name="_Transform">The transform target Object.</param>
	public static float GetPositionZ(this Transform _Transform)
    {
        return _Transform.position.z;
    }

    /// <summary>
    /// Resets the position, rotation and local scale of the specified transform.
    /// </summary>
    /// <param name="_Transform">The transform to reset</param>
    public static void ResetTransform(this Transform _Transform)
    {
        _Transform.position = Vector3.zero;
        _Transform.localRotation = Quaternion.identity;
        _Transform.localScale = Vector3.one;
    }

    /// <summary>
    /// Resets the position of the specified transform.
    /// </summary>
    /// <param name="_Transform">The transform to reset</param>
    public static void ResetPosition(this Transform _Transform)
    {
        _Transform.position = Vector3.zero;
    }

    /// <summary>
    /// Resets the local rotation of the specified transform.
    /// </summary>
    /// <param name="_Transform">The transform to reset</param>
    public static void ResetLocalRotation(this Transform _Transform)
    {
        _Transform.localRotation = Quaternion.identity;
    }


    /// <summary>
    /// Resets the local scale of the specified transform.
    /// </summary>
    /// <param name="_Transform">The transform to reset</param>
    public static void ResetLocalScale(this Transform _Transform)
    {
        _Transform.localScale = Vector3.one;
    }

    /// <summary>
    /// Set local rotation of transform directly.
    /// </summary>
    /// <param name="_Transform">The transform target Object.</param>
    /// <param name="_QuaternionRotation">New Rotation.</param>
	public static void SetRotationInstantly(this Transform _Transform, Quaternion _QuaternionRotation)
    {
        _Transform.localRotation = _QuaternionRotation;
    }

    /// <summary>
    /// Set local rotation of transform directly.
    /// </summary>
    /// <param name="_Transform">The transform target Object.</param>
    /// <param name="_EulerAngles">New Rotation.</param>
	public static void SetRotationInstantly(this Transform _Transform, Vector3 _EulerAngles)
    {
        _Transform.localEulerAngles = _EulerAngles;
    }

    /// <summary>
    /// Add child to this object. (Parent the transform to this transform)
    /// </summary>
    /// <param name="_Transform">This Object.</param>
    /// <param name="_Child">Child Object.</param>
    /// <param name="_KeepWorldPos">Do you want to keep the world position?</param>
    public static void AddChild(this Transform _Transform, Transform _Child, bool _KeepWorldPos = false)
    {
        _Child.SetParent(_Transform, _KeepWorldPos);
    }

    /// <summary>
    ///  Add child to this object. (Parent the transform to this transform)
    /// </summary>
    /// <param name="_Transform">This Object.</param>
    /// <param name="_Child">Child Object.</param>
    /// <param name="_KeepWorldPos">Do you want to keep the world position?</param>
    public static void AddChild(this Transform _Transform, GameObject _Child, bool _KeepWorldPos = false)
    {
        _Child.transform.SetParent(_Transform, _KeepWorldPos);
    }

    /// <summary>
    /// This function returns an array of components. Its a modiefied version of the build in function and can exclude 'this' object itself.
    /// </summary>
    /// <typeparam name="T">Return type.</typeparam>
    /// <param name="_Transform">This Object.</param>
    /// <param name="_IncludeSelf">Returns the Array with itself. Dont use this extension if you want that!</param>
    /// <param name="_IncludeInactive">Includes inactive Objects in the hierarchy.</param>
    /// <returns></returns>
    public static T[] GetComponentsInChildrenExtension<T>(this Transform _Transform, bool _IncludeSelf = false, bool _IncludeInactive = true)
    {
        if (_IncludeSelf)
            return _Transform.GetComponentsInChildren<T>(_IncludeInactive);
        else
        {
            System.Collections.Generic.List<T> tCompArray = new System.Collections.Generic.List<T>();
            tCompArray.AddRange(_Transform.GetComponentsInChildren<T>(_IncludeInactive));

            for (int i=tCompArray.Count -1; i>= 0; i--)
            {
                if (tCompArray[i].Equals(_Transform.GetComponent<T>()))
                {
                    tCompArray.RemoveAt(i);
                }
            }
            return tCompArray.ToArray();
        }
    }

    /*
     // Set position of Transform to a random Position of a circle. ( 2D not a sphere! )
     /// <summary>
     /// Set the position of this transform to a random position in a circle.
     /// </summary>
     /// <param name="_Transform">The transform target Object.</param>
     /// <param name="_Center">Position where to calculate from.</param>
     /// <param name="_MaxRadius">Radius of the circle.</param>
     /// <param name="_enumAxis">Axis where the circle spins around.</param>
     public static void SetToRandomPositionInCircle(this Transform _Transform, Vector3 _Center, float _MaxRadius, AXIS _enumAxis)
     {
         Vector3 Position = Vector3.zero;	
         float Radius = (_MaxRadius - Random.Range(0f, _MaxRadius));
         float Angle = Random.value * 360;
         if (_enumAxis == AXIS.X)
         {
             Position.x = _Center.x;// +Radius * Mathf.Sin(Angle * Mathf.Deg2Rad);
             Position.y = _Center.y + Radius * Mathf.Cos(Angle * Mathf.Deg2Rad);
             Position.z = _Center.z + Radius * Mathf.Sin(Angle * Mathf.Deg2Rad);
         }
         else if (_enumAxis == AXIS.Y)
         {
             Position.x = _Center.x + Radius * Mathf.Sin(Angle * Mathf.Deg2Rad);
             Position.y = _Center.y; //+ Radius * Mathf.Cos(Angle * Mathf.Deg2Rad);
             Position.z = _Center.z + Radius * Mathf.Cos(Angle * Mathf.Deg2Rad);
         }
         else if (_enumAxis == AXIS.Z)
         {
             Position.x = _Center.x + Radius * Mathf.Sin(Angle * Mathf.Deg2Rad);
             Position.y = _Center.y + Radius * Mathf.Cos(Angle * Mathf.Deg2Rad);
             Position.z = _Center.z;// +Radius * Mathf.Cos(Angle * Mathf.Deg2Rad);
         }
         else
         {
             Position = _Center;
             Debug.LogWarning("No random position of CircleSpawn. Spawning at center!");
         }
         _Transform.position = Position;
     }

     /// <summary>
     /// Set the position of this transform to a random position in a Donut.
     /// </summary>
     /// <param name="_Transform">The transform target Object.</param>
     /// <param name="_Center">Position where to calculate from.</param>
     /// <param name="_MinRadius">Radius of the inner circle.</param>
     /// <param name="_MaxRadius">Radius of the outer circle.</param>
     /// <param name="_enumAxis">Axis where the circle spins around.</param>
     public static void SetToRandomPositionInCircle(this Transform _Transform, Vector3 _Center, float _MinRadius, float _MaxRadius, AXIS _enumAxis)
     {
         float Angle = Random.value * 360;
         float Radius = (_MaxRadius - Random.Range(0f, _MaxRadius - _MinRadius));
         Vector3 Position = Vector3.zero;
         if (_enumAxis == AXIS.X)
         {
             Position.x = _Center.x;// +Radius * Mathf.Sin(Angle * Mathf.Deg2Rad);
             Position.y = _Center.y + Radius * Mathf.Cos(Angle * Mathf.Deg2Rad);
             Position.z = _Center.z + Radius * Mathf.Sin(Angle * Mathf.Deg2Rad);
         }
         else if (_enumAxis == AXIS.Y)
         {
             Position.x = _Center.x + Radius * Mathf.Sin(Angle * Mathf.Deg2Rad);
             Position.y = _Center.y; //+ Radius * Mathf.Cos(Angle * Mathf.Deg2Rad);
             Position.z = _Center.z + Radius * Mathf.Cos(Angle * Mathf.Deg2Rad);
         }
         else if (_enumAxis == AXIS.Z)
         {
             Position.x = _Center.x + Radius * Mathf.Sin(Angle * Mathf.Deg2Rad);
             Position.y = _Center.y + Radius * Mathf.Cos(Angle * Mathf.Deg2Rad);
             Position.z = _Center.z;// +Radius * Mathf.Cos(Angle * Mathf.Deg2Rad);
         }
         else
         {
             Position = _Center;
             Debug.LogWarning("No random position of CircleSpawn. Spawning at center!");
         }
         _Transform.position = Position;
     }
     */

    /// <summary>
    /// Find and return the transform of a child with matching string.
    /// </summary>
    /// <param name="_Transform">The transform target Object.</param>
    /// <param name="_Name">String to compare with GameObject Name.</param>
    /// <returns></returns>
    public static Transform FindChildRecursive(this Transform _Transform, string _Name)
    {
        Transform Found = null;

        //Debug.Log("Searching for : " + _Name + " in " + _Transform);

        foreach (Transform trans in _Transform)
        {
            if (trans.name == _Name)
            {
                //Debug.Log("  FOUND CHILD #" + _Name +"# IN : '" + trans.name + "'");
                return trans;
            }
            else
            {
                //Debug.Log("  No CHILD IN : '" +trans.name + "'");
                Found = FindChildRecursive(trans, _Name);
            }

            if (Found != null)
                return Found;
        }

        //Debug.LogWarning("!!No CHILD FOUND ABORT FUNCTION CALL!!");
        return Found;
    }
    /// <summary>
    /// Find and return the transform of a child with matching string for TAG.
    /// </summary>
    /// <param name="_Transform">The transform target Object.</param>
    /// <param name="_Tag">String to compare with GameObject Tag.</param>
    /// <returns></returns>
    public static Transform FindChildByTagRecursive(this Transform _Transform, string _Tag)
    {
        Transform Found = null;

        foreach (Transform trans in _Transform)
        {
            if (trans.tag == _Tag)
            {
                return trans;
            }
            else
            {
                Found = FindChildByTagRecursive(trans, _Tag);
            }

            if (Found != null)
                return Found;
        }

        //Debug.LogWarning("!!No CHILD FOUND ABORT FUNCTION CALL!!");
        return Found;
    }

    #endregion
}
