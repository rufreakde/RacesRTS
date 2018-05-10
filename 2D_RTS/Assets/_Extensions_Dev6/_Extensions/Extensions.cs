// File "Extensions.cs": copyright by Dev6 Game Studio
// Author: Rudolf Chrispens

using UnityEngine;
using System.Collections;
using System.Linq;

public static class Extensions
{
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /********************
     * ANIMATOR  *
     ********************/
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// Returns true if the animator has the specified param.
    /// </summary>
    /// <param name="_Anim"></param>
    /// <param name="_ParamName"></param>
    /// <returns></returns>
    public static bool ContainsParam(this Animator _Anim, string _ParamName)
    {
        foreach (AnimatorControllerParameter param in _Anim.parameters)
        {
            if (param.name == _ParamName) return true;
        }
        return false;
    }

    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /********************
     * STRING   *
     ********************/
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// Use this function if you want to check if a string contains another string without case sensitivitiy!
    /// </summary>
    /// <param name="_Source">This string.</param>
    /// <param name="_ToCheck">Is this string inside of the Source string?</param>
    /// <param name="_CaseInsensitive">Always true! Do not use this function with false use default C# Contains instead!</param>
    /// <returns></returns>
    public static bool Contains(this string _Source, string _ToCheck, bool _CaseInsensitive)
    {
        bool Return = false;

        if (_CaseInsensitive == true)
        {
            Return = _Source.ToLower().Contains(_ToCheck.ToLower());
            return Return;
        }
        else
        {
            Debug.LogWarning("Do not use this overloaded function without the insensitive feature, Performance reasons. Use  '_string.Contains(_string)'  instead!");
            Return =  _Source.Contains(_ToCheck);
            return Return;
        }
    }

    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /********************
     * AUDIO   *
     ********************/
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    
    /// <summary>
    /// Returns the negative db ( -60f - 0f) as a value between 0f and 1f.
    /// </summary>
    /// <param name="_Db"></param>
    /// <returns></returns>
    public static float DbToVolume(this float _Db)
    {
        float tempDB = (Mathf.Log(1f, 10f) * 20f);

        tempDB += _Db;
        float newvalue = Mathf.Pow(10f, (tempDB / 20f));

        //Debug.Log("DB: " + _Db + "    new volume: " + newvalue );
        return newvalue;

        //float Volume = Mathf.Pow(Mathf.Clamp01(-_Db), Mathf.Log(10, 4));
        ////float Volume = Mathf.Pow(-_Db, 1f / Mathf.Log(10, 4));
        //Debug.Log("from db: " + _Db + " to Volume: " + Volume);
        //return Volume;
    }

    /// <summary>
    /// Returns the db calculated from Volume ( 0 - 1 ).
    /// </summary>
    /// <param name="_Volume"></param>
    /// <returns></returns>
    public static float VolumeToDb(this float _Volume)
    {
        if (_Volume <= 1f && _Volume >= 0.00001f)
        {
            //calculate float to decibel
            return (Mathf.Log(_Volume, 10f) * 20f);

        }
        else if (_Volume <= 0f)
        {
            //calculcate decibel to float
            return Mathf.Pow(10f, (_Volume / 20f));
        }

        Debug.LogWarning("Error sound value parameter wrong " + _Volume);
        return _Volume;
        //float db = 20f * Mathf.Log10(_Volume);
        //Debug.Log("from Volume: " + _Volume + " to db: " + db + "   negate: " + -db);
        //return -db;
    }

    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /********************
     * LAYERMASKS   *
     ********************/
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    #region LayerMasks
    /// <summary>
    /// Returns true if layermask contains the specified layer.
    /// </summary>
    /// <param name="_Mask">Layermask</param>
    /// <param name="_Layer">Layer</param>
    /// <returns>True if contains.</returns>
    public static bool IsInLayerMask(this LayerMask _Mask, int _Layer)
    {
        return ((_Mask.value & (1 << _Layer)) > 0);
    }
    /// <summary>
    /// Returns true if layermask contains gameobject layer.
    /// </summary>
    /// <param name="_Mask"></param>
    /// <param name="_Object"></param>
    /// <returns>True if contains.</returns>
    public static bool IsInLayerMask(this LayerMask _Mask, GameObject _Object)
    {
        return ((_Mask.value & (1 << _Object.layer)) > 0);
    }
    #endregion
}
