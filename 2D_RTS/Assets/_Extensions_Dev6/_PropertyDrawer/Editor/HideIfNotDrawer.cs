using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(HideIfNotAttribute))]
public class HideIfNotDrawer : PropertyDrawer
{
    //show or hide for the drawer to make visible or invisible
    bool Show = true;

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        HideIfNotAttribute tAttribute = attribute as HideIfNotAttribute;
        #region FALLBACK FOR ARRAYS
        SerializedProperty RefProperty = null;

        RefProperty = property.serializedObject.FindProperty(tAttribute.PropertyName);

        if (RefProperty == null)
        {
            RefProperty = GetCapsulatedProperty(property, tAttribute);
            //Debug.Log(RefProperty.name + "  " + RefProperty + "   " +  RefProperty.type + "   " + RefProperty.boolValue);
        }


        #endregion

        if (RefProperty != null)
        {
            SerializedPropertyType ReferenceType = RefProperty.propertyType;

            System.Type ChosenType = tAttribute.RefType;

            if (ChosenType == typeof(bool) && ReferenceType == SerializedPropertyType.Boolean)
                Show = CheckBool(RefProperty, tAttribute, label);

            else if (ChosenType == typeof(int) && ReferenceType == SerializedPropertyType.Integer)
                Show = CheckInt(RefProperty, tAttribute, label);

            else if (ChosenType == typeof(string) && ReferenceType == SerializedPropertyType.String)
                Show = CheckString(RefProperty, tAttribute, label);
            else
                Debug.LogError("Error: Attribut was not defined right!  ->  (" + ReferenceType + " " + tAttribute.PropertyName + ") != " + ChosenType);
        }

        if(Show)
        {
			//if(property.floatValue == null && property.boolValue == null && property.stringValue == null && property.intValue == null)
			//	return base.GetPropertyHeight(property, label) * 2f;

            return base.GetPropertyHeight(property, label);
        }
        else
            return 0f;
    }

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        var tAttribute = attribute as HideIfNotAttribute;
        
        #region FALLBACK FOR ARRAYS
            SerializedProperty RefProperty = null;
            RefProperty = property.serializedObject.FindProperty(tAttribute.PropertyName);
            

            if (RefProperty == null)
            {
                RefProperty = GetCapsulatedProperty(property, tAttribute);
                //Debug.Log(RefProperty.name + "  " + RefProperty + "   " +  RefProperty.type + "   " + RefProperty.boolValue);
            }

            if (RefProperty == null)
            {
                EditorGUI.HelpBox(position, string.Format("Property {0} may be capsulated!", tAttribute.PropertyName), MessageType.Error);
                return;
            }
        #endregion

        System.Type tTypeCompare = typeof(bool);

        if(RefProperty.propertyType == SerializedPropertyType.Boolean)
        {
            tTypeCompare = typeof(bool);
        }
        else if (RefProperty.propertyType == SerializedPropertyType.Integer)
        {
            tTypeCompare = typeof(int);
        }
        else if (RefProperty.propertyType == SerializedPropertyType.String)
        {
            tTypeCompare = typeof(string);
        }

        if (RefProperty != null)
        {
            if (tTypeCompare == tAttribute.RefType)
            {
                if(Show)
                    EditorGUI.PropertyField(position, property, label, true);
            }
            else
            {
                EditorGUI.HelpBox(position, string.Format("Property {0} is not of Type: (" + tAttribute.RefType.ToString() + ")", tAttribute.PropertyName), MessageType.Error);
            }
        }
        else
        {
            EditorGUI.HelpBox(position, string.Format("Couldn't find property {0}", tAttribute.PropertyName), MessageType.Error);
        }
    }


    public SerializedProperty GetCapsulatedProperty(SerializedProperty _PropertyOfAttribute, HideIfNotAttribute _Attribute)
    {
        SerializedProperty RefPropertyHolder = null;
        RefPropertyHolder = _PropertyOfAttribute.serializedObject.FindProperty(_Attribute.PropHolder);

        if(RefPropertyHolder == null)
        {
            return null;
        }

        //now we have to find the specific _PropertyOfAttribute Array! And then return the RefProperty of that Array ID
        for(int i=0; i< RefPropertyHolder.arraySize; i++)
        {
            if (RefPropertyHolder.GetArrayElementAtIndex(i).FindPropertyRelative(_Attribute.Name).propertyPath == _PropertyOfAttribute.propertyPath)
            {
                //Debug.Log("Equal Path: " + RefPropertyHolder.GetArrayElementAtIndex(i).FindPropertyRelative(_Attribute.Name).propertyPath + "  ==  " + _PropertyOfAttribute.propertyPath);
                return RefPropertyHolder.GetArrayElementAtIndex(i).FindPropertyRelative(_Attribute.PropertyName);
            }
        }

        return null; //didnt find the right property
    }

    public bool CheckBool(SerializedProperty _RefProp, HideIfNotAttribute _Attribute, GUIContent _label)
    {
        if (_Attribute.HideOnRefValue)
        {
            if (_RefProp != null && _RefProp.boolValue == _Attribute.RefBool)
            {
                return false;
            }
            else
                return true;
        }
        else
        {
            if (_RefProp != null && _RefProp.boolValue != _Attribute.RefBool)
            {
                return false;
            }
            else
                return true;
        }
    }

    public bool CheckInt(SerializedProperty _RefProp, HideIfNotAttribute _Attribute, GUIContent _label)
    {
        if (_Attribute.HideOnRefValue)
        {
            if (_RefProp != null && _RefProp.intValue == _Attribute.RefInt)
            {
                return false;
            }
            else
                return true;
        }
        else
        {
            if (_RefProp != null && _RefProp.intValue != _Attribute.RefInt)
            {
                return false;
            }
            else
                return true;
        }
    }

    public bool CheckString(SerializedProperty _RefProp, HideIfNotAttribute _Attribute, GUIContent _label)
    {
        if (_Attribute.HideOnRefValue)
        {
            if (_RefProp != null && _RefProp.stringValue == _Attribute.RefString)
            {
                return false;
            }
            else
                return true;
        }
        else
        {
            if (_RefProp != null && _RefProp.stringValue != _Attribute.RefString)
            {
                return false;
            }
            else
                return true;
        }
    }

}