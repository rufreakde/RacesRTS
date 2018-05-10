/*********************
*	Rudolf Chrispens
***********************/

#region USE
using UnityEngine;
using UnityEngine.Scripting;
using System;
#endregion

[AttributeUsage(AttributeTargets.Field, Inherited = true, AllowMultiple = false)]
public class ShowOnlyAttribute : PropertyAttribute
{
    //private System.Type chosenType;

    //public System.Type ChosenType
    //{
    //    get { return chosenType; }
    //}
    public ShowOnlyAttribute()
    {

    }
}
