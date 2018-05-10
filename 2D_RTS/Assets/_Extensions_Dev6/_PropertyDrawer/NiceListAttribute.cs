/*********************
*	Rudolf Chrispens
***********************/

#region USE
using UnityEngine;
using UnityEngine.Scripting;
using System;
#endregion

[AttributeUsage(AttributeTargets.Field, Inherited = true, AllowMultiple = false)]
public class NiceListAttribute : PropertyAttribute
{
    public NiceListAttribute() { }
}
