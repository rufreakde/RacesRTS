/*********************
*	Rudolf Chrispens
***********************/

#region USE
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
#endregion

namespace Dev6
{

    [RequireComponent(typeof(Camera))]
    [AddComponentMenu("Dev6/CAMERA/Fade in and out")]
    public class CameraFade : MonoBehaviour
    {
        private Camera cam = null;
        public Camera Cam
        {
            get { return cam; }
            set { cam = value; }
        }

        private Transform camTransform = null;
        public Transform CamTransform
        {
            get { return camTransform; }
            set { camTransform = value; }
        }

        void Awake()
        {
            Cam = this.GetSafeComponent<Camera>();
            CamTransform = this.transform;
            t_FadeTexture = new Texture2D(1, 1);
            t_FadeTexture.SetPixel(0, 0, Color.black);
            t_FadeTexture.Apply();
        }

        void Start()
        {
            if (State == eFadeStatus.Full)
            {
                f_alpha = 1f;
            }
            else if (State == eFadeStatus.Clear)
            {
                f_alpha = 0f;
            }

            color_Alpha = GUI.color;
            color_Alpha.a = f_alpha;
            GUI.color = color_Alpha;
            GUI.depth = i_DrawDepth;
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /********************
         * FADING  *
         ********************/
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #region FadeIn and FadeOut
        private Texture2D t_FadeTexture;

        [Split("CameraFade")]
        public eFadeStatus State = eFadeStatus.Clear;
        [Range(0.1f, 0.90f)]
        public float FadeSpeed = 0.2f;

        private int i_DrawDepth = -99;
        private float f_alpha = 0.0f;
        private Color color_Alpha;

        void OnGUI()
        {
            if (State != eFadeStatus.Full && State != eFadeStatus.Clear)
            {
                f_alpha -= (int)State * FadeSpeed * Time.deltaTime;
                f_alpha = Mathf.Clamp01(f_alpha);

                color_Alpha = GUI.color;
                color_Alpha.a = f_alpha;
                GUI.color = color_Alpha;
                GUI.depth = i_DrawDepth;
                GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), t_FadeTexture);

                if (f_alpha <= 0.0001f)
                {
                    State = eFadeStatus.Clear;
                }
                else if (f_alpha >= 0.9999f)
                {
                    State = eFadeStatus.Full;
                }
            }
            else if (State == eFadeStatus.Full)
            {
                //draw gui
                GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), t_FadeTexture);
            }
            //else if (FadingMode == eFadeStatus.Full)
            //{
            //    f_alpha = 1f;
            //    //f_alpha = Mathf.Clamp01(f_alpha);

            //    color_Alpha = GUI.color;
            //    color_Alpha.a = f_alpha;
            //    GUI.color = color_Alpha;

            //    GUI.depth = i_DrawDepth;
            //    GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), t_FadeTexture);
            //}
            //else if (FadingMode == eFadeStatus.Clear)
            //{
            //    f_alpha = 0f;
            //    f_alpha = Mathf.Clamp01(f_alpha);

            //    color_Alpha = GUI.color;
            //    color_Alpha.a = f_alpha;
            //    GUI.color = color_Alpha;

            //    GUI.depth = i_DrawDepth;
            //    GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), t_FadeTexture);
            //}
        }
        #endregion


        public enum eFadeStatus
        {
            FadeToFull = -1,
            Full = 0,
            FadeToClear = 1,
            Clear = 2
        }
    }
}