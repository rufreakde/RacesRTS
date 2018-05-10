/*********************
*	Rudolf Chrispens
***********************/

#region USE
using UnityEngine;
using System.Collections;
#endregion

public static class GetSafeCompExtensions 
{
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /********************
     * GET SAFE COMPONENT  *
     ********************/
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    #region Get Component
    /// <summary>
    /// Get Component from the Transform. This is a safe method to throw an error if the component was not found!
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="_Root"></param>
    /// <returns></returns>
    public static T GetSafeComponent<T>(this Transform _Root) where T : Component
    {
        T component = _Root.GetComponent<T>();

        if (component == null)
        {
            Debug.LogError("Expected to find component of type "
               + typeof(T) + " but found none on '" + _Root.ToString() + "'  FRAME: '" + Time.frameCount + "'  ||  ", _Root);
        }

        return component;
    }

    /// <summary>
    /// Get Component from the GameObject. This is a safe method to throw an error if the component was not found!
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="_Root"></param>
    /// <returns></returns>
    public static T GetSafeComponent<T>(this GameObject _Root) where T : Component
    {
        T component = _Root.GetComponent<T>();

        if (component == null)
        {
            Debug.LogError("Expected to find component of type "
               + typeof(T) + " but found none on '" + _Root.ToString() + "'  FRAME: '" + Time.frameCount + "'  ||  ", _Root);
        }

        return component;
    }

    /// <summary>
    /// Get Component from the Monobehaviour. This is a safe method to throw an error if the component was not found!
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="_Root"></param>
    /// <returns></returns>
    public static T GetSafeComponent<T>(this MonoBehaviour _Root) where T : Component
    {
        T component = _Root.GetComponent<T>();

        if (component == null)
        {
            Debug.LogError("Expected to find component of type "
               + typeof(T) + " but found none on '" + _Root.ToString() + "'  FRAME: '" + Time.frameCount + "'  ||  ", _Root);
        }

        return component;
    }

    /// <summary>
    /// Get Component from the Collider. This is a safe method to throw an error if the component was not found!
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="_Root"></param>
    /// <returns></returns>
    public static T GetSafeComponent<T>(this Collider _Root) where T : Component
    {
        T component = _Root.GetComponent<T>();

        if (component == null)
        {
            Debug.LogError("Expected to find component of type "
               + typeof(T) + " but found none on '" + _Root.ToString() + "'  FRAME: '" + Time.frameCount + "'  ||  ", _Root);
        }

        return component;
    }
    #endregion
}
