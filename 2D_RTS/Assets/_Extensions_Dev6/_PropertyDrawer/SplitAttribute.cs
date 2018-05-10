/*********************
*	Rudolf Chrispens
***********************/

#region USE
using UnityEngine;
using System;
#endregion

//[SelectionBase]
[AttributeUsage(AttributeTargets.Field, Inherited = true, AllowMultiple = true)]
public class SplitAttribute : PropertyAttribute
{
    private string title;

    public string Title
    {
        get { return title; }
        set { title = value; }
    }

    public SplitAttribute()
    {
        this.Title = "";
    }

    public SplitAttribute(string _title)
    {
        this.Title = _title;
    }
}
