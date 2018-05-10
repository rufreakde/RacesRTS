/*********************
*	Rudolf Chrispens
***********************/

#region USE
using UnityEngine;
using System.Collections;
using System.Linq;
#endregion

public static class GameObjectExtensions 
{
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /********************
     * ON GAMEOBJECTS   *
     ********************/
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    #region GameObject

    /// <summary>
    /// Returns true if GameObject has a Rigidbody.
    /// </summary>
    /// <param name="_GameObject">Target</param>
    /// <returns>Returns Bool true if found.</returns>
    public static bool HasRigidbody(this GameObject _GameObject)
    {
        return (_GameObject.GetSafeComponent<Rigidbody>() != null);
    }
    /// <summary>
    /// Returns true if GameObject has a Rigidbody2D.
    /// </summary>
    /// <param name="_GameObject">Target</param>
    /// <returns>Returns Bool true if found.</returns>
    public static bool HasRigidbody2D(this GameObject _GameObject)
    {
        return (_GameObject.GetSafeComponent<Rigidbody2D>() != null);
    }
    /// <summary>
    /// Returns true if GameObject has a Animation.
    /// </summary>
    /// <param name="_GameObject"></param>
    /// <returns>Return Animation.</returns>
    public static bool HasAnimation(this GameObject _GameObject)
    {
        return (_GameObject.GetSafeComponent<Animation>() != null);
    }

    /// <summary>
    /// Set the layer of all children, of target GameObject.
    /// </summary>
    /// <param name="_GameObject">Target GameObject.</param>
    /// <param name="layer">Int Layer.</param>
    public static void SetLayerRecursively(this GameObject _GameObject, int layer)
    {
        _GameObject.layer = layer;
        foreach (Transform t in _GameObject.transform)
            t.gameObject.SetLayerRecursively(layer);
    }

    /// <summary>
    /// Set the layer of all children, of target Transform.
    /// </summary>
    /// <param name="_Transform">Target Transform.</param>
    /// <param name="_Tag">New Tag.</param>
    public static void SetTagRecursively(this GameObject _GameObject, string _Tag)
    {
        _GameObject.tag = _Tag;
        foreach (Transform t in _GameObject.transform)
            t.SetTagRecursively(_Tag);
    }

    /// <summary>
    /// Returns the first interface of type T found on object _GameObject.
    /// <para>Equally heavy on performance as GetComponents; avoid extensive use.</para>
    /// </summary>
    /// <typeparam name="T">The interface type to return.</typeparam>
    /// <param name="_GameObject">The object to get the interface of.</param>
    /// <returns>The first interface of type T on object _GameObject.</returns>
    public static T GetInterface<T>(this GameObject _GameObject) where T : class
    {
        if (!typeof(T).IsInterface)
        {
            Debug.LogError(typeof(T).ToString() + " is not an interface! GetInterface<T> does not work with other types.");
            return null;
        }
        return _GameObject.GetComponents<Component>().OfType<T>().FirstOrDefault();
    }

    /// <summary>
    /// Returns the all interfaces of type T found on object _GameObject.
    /// <para>Equally heavy on performance as GetComponents; avoid extensive use.</para>
    /// </summary>
    /// <typeparam name="T">The interface type to return.</typeparam>
    /// <param name="_GameObject">The object to get the interfaces of.</param>
    /// <returns>Collection of all interfaces of type T on object _GameObject.</returns>
    public static T[] GetInterfaces<T>(this GameObject _GameObject) where T : class
    {
        if (!typeof(T).IsInterface)
        {
            Debug.LogError(typeof(T).ToString() + " is not an interface! GetInterfaces<T> does not work with other types.");
            return null;
        }

        return _GameObject.GetComponents<Component>().OfType<T>().ToArray<T>();
    }

    #endregion
}
