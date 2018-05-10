using UnityEngine;

/// <summary>
/// Angle attribute.
/// </summary>
public class AngleAttribute : PropertyAttribute
{
    public float min = -1, max = -1;
    public string unit = string.Empty;
    public Color backgroundColor = Color.gray, activeColor = Color.red;
    public bool showValue = true;
    public float knobSize = 32;
    public AngleAttribute()
    {
    }

    public AngleAttribute(float min, float max)
    {
        this.min = min;
        this.max = max;
    }

    public AngleAttribute(float min, float max, string unit)
        : this(min, max)
    {
        this.unit = unit;
    }

    public AngleAttribute(float min, float max, string unit, Color backgroundColor)
        : this(min, max, unit)
    {
        this.backgroundColor = backgroundColor;
    }

    public AngleAttribute(float min, float max, string unit, Color backgroundColor, Color activeColor)
        : this(min, max, unit, backgroundColor)
    {
        this.activeColor = activeColor;
    }

    public AngleAttribute(float min, float max, string unit, Color backgroundColor, Color activeColor, bool showValue)
        : this(min, max, unit, backgroundColor, activeColor)
    {
        this.showValue = showValue;
    }

    public AngleAttribute(float min, float max, string unit, Color backgroundColor, Color activeColor, bool showValue, float knobSize)
        : this(min, max, unit, backgroundColor, activeColor, showValue)
    {
        this.knobSize = knobSize;
    }
}