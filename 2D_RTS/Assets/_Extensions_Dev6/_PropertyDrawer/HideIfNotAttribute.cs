using UnityEngine;

public class HideIfNotAttribute : PropertyAttribute
{
    
    //variable to define if the property will be shown or hidden when refProperty hits the defined mark
    public bool HideOnRefValue = true;

    //the name of the attribute field
    private string name = "";
    public string Name
    {
        get { return name; }
        set { name = value; }
    }


    #region ref
        //the type chosen ( constuctor dependdent )
        System.Type refType;
        public System.Type RefType
        {
            get { return refType; }
            set { refType = value; }
        }

        //the name of the refernce propertyfield that will be used to show or hide the property with the Attribute
        private string propertyName = "";
        public string PropertyName 
        {
            get { return propertyName; }
            set { propertyName = value; } 
        }

        //the name of a property holder ( for e.g. a class that holds the property of "PropertyName"
        private string propHolder = "";
        public string PropHolder
        {
            get { return propHolder; }
            set { propHolder = value; }
        }

        //the different RefValues
        public bool RefBool = true;
        public int RefInt = 0;
        public string RefString = "";
    #endregion

    public HideIfNotAttribute(string _PropertyName, bool _RefValue, bool _HideOnRefValue = false)
    {
        RefType = typeof(bool);
        PropertyName = _PropertyName;
        RefBool = _RefValue;
        HideOnRefValue = _HideOnRefValue;
    }

    public HideIfNotAttribute(string _PropertyName, int _RefValue, bool _HideOnRefValue = false)
    {
        RefType = typeof(int);
        PropertyName = _PropertyName;
        RefInt = _RefValue;
        HideOnRefValue = _HideOnRefValue;
    }

    public HideIfNotAttribute(string _PropertyName, string _RefValue, bool _HideOnRefValue = false)
    {
        RefType = typeof(string);
        PropertyName = _PropertyName;
        RefString = _RefValue;
        HideOnRefValue = _HideOnRefValue;
    }

    //upgradet:
    /// <summary>
    /// Use if capsulated!
    /// </summary>
    /// <param name="_PropertyName"></param>
    /// <param name="_RefValue"></param>
    /// <param name="_PropertyRefHolder"></param>
    /// <param name="_AttributePropertyName"></param>
    /// <param name="_HideOnRefValue"></param>
    public HideIfNotAttribute(string _PropertyName, bool _RefValue, string _PropertyRefHolder, string _AttributePropertyName, bool _HideOnRefValue = false)
    {
        RefType = typeof(bool);
        PropertyName = _PropertyName;
        RefBool = _RefValue;
        HideOnRefValue = _HideOnRefValue;
        PropHolder = _PropertyRefHolder;
        Name = _AttributePropertyName;
    }

    /// <summary>
    /// Use if capsulated!
    /// </summary>
    /// <param name="_PropertyName"></param>
    /// <param name="_RefValue"></param>
    /// <param name="_PropertyRefHolder"></param>
    /// <param name="_AttributePropertyName"></param>
    /// <param name="_HideOnRefValue"></param>
    public HideIfNotAttribute(string _PropertyName, int _RefValue, string _PropertyRefHolder, string _AttributePropertyName, bool _HideOnRefValue = false)
    {
        RefType = typeof(int);
        PropertyName = _PropertyName;
        RefInt = _RefValue;
        HideOnRefValue = _HideOnRefValue;
        PropHolder = _PropertyRefHolder;
        Name = _AttributePropertyName;
    }

    /// <summary>
    /// Use if capsulated!
    /// </summary>
    /// <param name="_PropertyName"></param>
    /// <param name="_RefValue"></param>
    /// <param name="_PropertyRefHolder"></param>
    /// <param name="_AttributePropertyName"></param>
    /// <param name="_HideOnRefValue"></param>
    public HideIfNotAttribute(string _PropertyName, string _RefValue, string _PropertyRefHolder, string _AttributePropertyName, bool _HideOnRefValue = false)
    {
        RefType = typeof(string);
        PropertyName = _PropertyName;
        RefString = _RefValue;
        HideOnRefValue = _HideOnRefValue;
        PropHolder = _PropertyRefHolder;
        Name = _AttributePropertyName;
    }
}
