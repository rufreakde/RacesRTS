using UnityEngine;

[System.AttributeUsageAttribute(System.AttributeTargets.Field, AllowMultiple = true, Inherited = true)]
public class SimpleButtonAttribute : PropertyAttribute
{
    private System.Type classType;
    public System.Type ClassType
    {
        get { return classType; }
        set { classType = value; }
    }

    private string buttonName = "";
    public string ButtonName
    {
        get { return buttonName; }
        set { buttonName = value; }
    }

    private string functionName = "";
    public string FunctionName
    {
        get { return functionName; }
        set { functionName = value; }
    }

    public SimpleButtonAttribute(string _FunctionToCall, System.Type _Type ,bool _Above = false)
    {
        ButtonName = "Method: " + _FunctionToCall + "()";
        FunctionName = _FunctionToCall;
        ClassType = _Type;
	}
}
