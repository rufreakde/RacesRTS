using UnityEngine;
using System.Collections;

public class SliderAttribute : PropertyAttribute {

    public bool Modifiable = false;
    public string PropertyMin = "";
    public string PropertyMax = "";


    float min = 0f;
    public float Min
    {
        get { return min; }
        set
        {
            min = value;
        }
    }

    float max = 100f;
    public float Max
    {
        get { return max; }
        set
        {
            max = value;
        }
    }

    public SliderAttribute(float _Min, float _Max)
    {
        Min = _Min;
        Max = _Max;
        Modifiable = false;
    }

    public SliderAttribute(string _PropertyMin, string _PropertyMax)
    {
        Min = 0;
        Max = 1;
        Modifiable = true;
        PropertyMin = _PropertyMin;
        PropertyMax = _PropertyMax;
    }
}
