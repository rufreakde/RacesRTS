using UnityEditor;
using UnityEngine;
using System;
using System.Reflection;

[CustomPropertyDrawer(typeof(SimpleButtonAttribute))]
public class SimpleButtonDrawer : DecoratorDrawer
{
    //Unity GUI function
    public override void OnGUI(Rect _Position)
    {
        // First get the attribute
        SimpleButtonAttribute tAttribute = attribute as SimpleButtonAttribute;

        UnityEngine.Object theObject = Selection.activeGameObject.GetComponent(tAttribute.ClassType) as UnityEngine.Object;

        if (GUI.Button(_Position, tAttribute.ButtonName))
        {
            
            MethodInfo tMethod = theObject.GetType().GetMethod(tAttribute.FunctionName);
            if (tMethod != null)
            {
                //Invoke the method if != null Note: It works only of you dont need special parameters! (null)
                tMethod.Invoke(theObject, null);
            }
        }
    }

    public override float GetHeight()
    {
        return base.GetHeight() *1.5f;
    }
}