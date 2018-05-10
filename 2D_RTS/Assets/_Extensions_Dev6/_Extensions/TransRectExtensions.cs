using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public enum eRectAnchorPreset
{
    TopLeft,
    TopMid,
    TopRight,
    MidLeft,
    Center,
    MidRight,
    BotLeft,
    BotMid,
    BotRight,
    COUNT
}

public static class TransRectExtensions {

    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /********************
     * ON CANVAS   *
     ********************/
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    #region Canvas
    /// <summary>
    /// Calulates Position for RectTransform.position from a transform.position. Does not Work with WorldSpace Canvas!
    /// </summary>
    /// <param name="_Canvas"> The Canvas parent of the RectTransform.</param>
    /// <param name="_Position">Position of in world space of the "Transform" you want the "RectTransform" to be.</param>
    /// <param name="_Cam">The Camera which is used. Note this is useful for split screen and both RenderModes of the Canvas.</param>
    /// <returns></returns>
    public static Vector3 CalculatePositionFromTransformToRectTransform(this Canvas _Canvas, Vector3 _Position, Camera _Cam)
    {
        Vector3 Return = Vector3.zero;
        if (_Canvas.renderMode == RenderMode.ScreenSpaceOverlay)
        {
            Return = _Cam.WorldToScreenPoint(_Position);
        }
        else if (_Canvas.renderMode == RenderMode.ScreenSpaceCamera)
        {
            Vector2 tempVector = Vector2.zero;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(_Canvas.transform as RectTransform, _Cam.WorldToScreenPoint(_Position), _Cam, out tempVector);
            Return = _Canvas.transform.TransformPoint(tempVector);
        }

        return Return;
    }

    /// <summary>
    /// Calulates Position for RectTransform.position Mouse Position. Does not Work with WorldSpace Canvas!
    /// </summary>
    /// <param name="_Canvas">The Canvas parent of the RectTransform.</param>
    /// <param name="_Cam">The Camera which is used. Note this is useful for split screen and both RenderModes of the Canvas.</param>
    /// <returns></returns>
    public static Vector3 CalculatePositionFromMouseToRectTransform(this Canvas _Canvas, Camera _Cam)
    {
        Vector3 Return = Vector3.zero;

        if (_Canvas.renderMode == RenderMode.ScreenSpaceOverlay)
        {
            Return = Input.mousePosition;
        }
        else if (_Canvas.renderMode == RenderMode.ScreenSpaceCamera)
        {
            Vector2 tempVector = Vector2.zero;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(_Canvas.transform as RectTransform, Input.mousePosition, _Cam, out tempVector);
            Return = _Canvas.transform.TransformPoint(tempVector);
        }

        return Return;
    }

    /// <summary>
    /// Calculates Position for "Transform".position from a "RectTransform".position. Does not Work with WorldSpace Canvas!
    /// </summary>
    /// <param name="_Canvas">The Canvas parent of the RectTransform.</param>
    /// <param name="_Position">Position of the "RectTransform" UI element you want the "Transform" object to be placed to.</param>
    /// <param name="_Cam">The Camera which is used. Note this is useful for split screen and both RenderModes of the Canvas.</param>
    /// <returns></returns>
    public static Vector3 CalculatePositionFromRectTransformToTransform(this Canvas _Canvas, Vector3 _Position, Camera _Cam)
    {
        Vector3 Return = Vector3.zero;
        if (_Canvas.renderMode == RenderMode.ScreenSpaceOverlay)
        {
            Return = _Cam.ScreenToWorldPoint(_Position);
        }
        else if (_Canvas.renderMode == RenderMode.ScreenSpaceCamera)
        {
            RectTransformUtility.ScreenPointToWorldPointInRectangle(_Canvas.transform as RectTransform, _Cam.WorldToScreenPoint(_Position), _Cam, out Return);
        }
        return Return;
    }
    #endregion

    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /********************
     * ON RECTTRANSFORM   *
     ********************/
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    #region RectTransform
    public static void SetAnchorPreset(this RectTransform _Rect, eRectAnchorPreset _Preset)
    {
        switch(_Preset)
        {
            case eRectAnchorPreset.TopLeft:     { _Rect.SetAnchorPresetManualy(new Vector2(0f, 1f), new Vector2(0f, 1f), new Vector2(0f, 1f)); break; }
            case eRectAnchorPreset.TopMid:      { _Rect.SetAnchorPresetManualy(new Vector2(0.5f, 1f), new Vector2(0.5f, 1f), new Vector2(0.5f, 1f)); break; }
            case eRectAnchorPreset.TopRight:    { _Rect.SetAnchorPresetManualy(new Vector2(1f, 1f), new Vector2(1f, 1f), new Vector2(1f, 1f)); break; }

            case eRectAnchorPreset.MidLeft:     { _Rect.SetAnchorPresetManualy(new Vector2(0f, .5f), new Vector2(0f, .5f), new Vector2(0f, .5f)); break; }
            case eRectAnchorPreset.Center:      { _Rect.SetAnchorPresetManualy(new Vector2(0.5f, 0.5f), new Vector2(0.5f, 0.5f), new Vector2(0.5f, 0.5f)); break; }
            case eRectAnchorPreset.MidRight:    { _Rect.SetAnchorPresetManualy(new Vector2(1f, .5f), new Vector2(1f, .5f), new Vector2(1f, .5f)); break; }
           
            case eRectAnchorPreset.BotLeft:     { _Rect.SetAnchorPresetManualy(new Vector2(0f, 0f), new Vector2(0f, 0f), new Vector2(0f, 0f)); break; }
            case eRectAnchorPreset.BotMid:      { _Rect.SetAnchorPresetManualy(new Vector2(0.5f, 0f), new Vector2(0.5f, 0f), new Vector2(0.5f, 1f)); break; }
            case eRectAnchorPreset.BotRight:    { _Rect.SetAnchorPresetManualy(new Vector2(1f, 0f), new Vector2(1f, 0f), new Vector2(1f, 0f)); break; }

            case eRectAnchorPreset.COUNT: { Debug.LogError("You cant use COUNT like that! This is intended for iterations"); break; }
        }
    }

    public static RectTransform SetAnchorPresetManualy(this RectTransform _Rect, Vector2 _AnchorMin, Vector2 _AnchorMax, Vector2 _Pivot )
    {
        _Rect.anchorMin = _AnchorMin;
        _Rect.anchorMax = _AnchorMax;
        _Rect.pivot = _Pivot;

        return _Rect;
    }
    #endregion
}
