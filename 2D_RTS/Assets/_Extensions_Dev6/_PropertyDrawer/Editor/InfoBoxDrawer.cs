using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(InfoBoxAttribute))]
public class InfoBoxDrawer : DecoratorDrawer
{
    private float boxheight = 2f;
    InfoBoxAttribute separatorAttribute;

    public override void OnGUI(Rect _Position)
    {
        separatorAttribute = (InfoBoxAttribute)attribute;
        boxheight = separatorAttribute.Height;

        if (separatorAttribute.Description != "")
        {
            EditorGUI.HelpBox(_Position,separatorAttribute.Description, MessageType.Info);
        }
    }

    public override float GetHeight()
    {
        return base.GetHeight() * boxheight + 3f;
    }
}