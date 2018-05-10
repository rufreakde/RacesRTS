/*********************
*	Rudolf Chrispens
***********************/

#region USE
using UnityEngine;
using System;
#endregion

//[SelectionBase]
[AttributeUsage(AttributeTargets.Field, Inherited = true, AllowMultiple = true)]
public class InfoBoxAttribute : PropertyAttribute
{
    private string description;
    private float height;

    public string Description
    {
        get { return description; }
        set { description = value; }
    }

    public float Height
    {
        get { return height; }
        set { height = value; }
    }

    public InfoBoxAttribute(string _Description, float _Height = 2f)
    {
        this.Description = _Description;
        this.Height = _Height;
    }
}
