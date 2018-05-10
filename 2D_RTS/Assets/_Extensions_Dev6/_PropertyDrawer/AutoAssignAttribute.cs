/*********************
*	Rudolf Chrispens
***********************/

#region USE
using UnityEngine;
using UnityEngine.Scripting;
using System;
#endregion

[AttributeUsage(AttributeTargets.Field, Inherited = true, AllowMultiple = false)]
public class AutoAssignAttribute : PropertyAttribute
{
    private bool auto = true;

    public bool Auto
    {
        get { return auto; }
        set { auto = value; }
    }
    public AutoAssignAttribute() { }
}
