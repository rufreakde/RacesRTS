using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(SplitAttribute))]
public class SplitDrawer : DecoratorDrawer
{
    //Unity GUI function
    public override void OnGUI(Rect _Position)
    {
        SplitAttribute separatorAttribute = (SplitAttribute)attribute;
        Color tColorStore = GUI.backgroundColor;
        GUI.backgroundColor = Color.green;

        if (separatorAttribute.Title == "")
        {
            _Position.height = 1;
            _Position.y += 9;
            GUI.Box(_Position, "");
        }
        else
        {
            Vector2 textSize = GUI.skin.label.CalcSize(new GUIContent(separatorAttribute.Title));
            float separatorWidth = (_Position.width - textSize.x) / 2.0f - 5.0f;
            _Position.y += 9;

            GUI.Box(new Rect(_Position.xMin, _Position.yMin, separatorWidth, 1), "");
            GUI.Label(new Rect(_Position.xMin + separatorWidth + 5.0f, _Position.yMin - 8f, textSize.x, 20), separatorAttribute.Title);
            GUI.Box(new Rect(_Position.xMin + separatorWidth + 10.0f + textSize.x, _Position.yMin, separatorWidth, 1), "");
        }

        GUI.backgroundColor = tColorStore;
    }

    public override float GetHeight()
    {
        return base.GetHeight() * 1.5f;
    }
}