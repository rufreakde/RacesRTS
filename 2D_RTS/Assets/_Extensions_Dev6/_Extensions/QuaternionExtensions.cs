/*********************
*	Rudolf Chrispens
***********************/

#region USE
using UnityEngine;
using System.Collections;
#endregion

public static class QuaternionExtensions 
{
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /********************
     * ON Quaternions    *
     ********************/
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    #region QUATERNIONS

    /// <summary>
    /// Store this value and apply it after to the needed Quaternion. Or use the return value to apply it.
    /// </summary>
    /// <param name="_Base"></param>
    /// <param name="_Angle"></param>
    /// <param name="_Axis"></param>
    /// <returns></returns>
    public static Quaternion RotateInstantlyAroundAxis(this Quaternion _Base, float _Angle, Vector3 _Axis)
    {
        Quaternion newQuaternion = Quaternion.AngleAxis(_Angle, _Axis);
        return newQuaternion * _Base;
    }
    #endregion
}
