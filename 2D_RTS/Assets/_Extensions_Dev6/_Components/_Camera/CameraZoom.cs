/*********************
*	Rudolf Chrispens
***********************/

#region USE
using UnityEngine;
using System.Collections;
#endregion

namespace Dev6
{

    [RequireComponent(typeof(Camera))]
    [AddComponentMenu("Dev6/CAMERA/Zooming")]
    public class CameraZoom : MonoBehaviour
    {
        private Camera cam = null;
        public Camera Cam
        {
            get { return cam; }
            set { cam = value; }
        }

        void Awake()
        {
            Cam = this.GetSafeComponent<Camera>();
        }

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /********************
         * ZOOM  *
         ********************/
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #region Zooming
        bool isZooming = false;
        public bool IsZooming
        {
            get { return isZooming; }
            set { isZooming = value; }
        }
        /// <summary>
        /// Linear Zoom with PingPong care with the Zoom Range.
        /// </summary>
        /// <param name="_Camera">_ camera.</param>
        /// <param name="_Zoom">_ zoom 1 - 179 perspektive | 1 - infinity orthographic.</param>
        /// <param name="_Time">_ time.</param>
        /// <param name="_Speed">_ speed.</param>
        /// <param name="_PingPong">If set to <c>true</c> _ ping pong.</param>
        public void LerpZoom(float _Zoom, float _Time)
        {
            if (IsZooming)
            {
                IsZooming = false;
            }
            StartCoroutine(LerpZoomCoroutine(Cam, _Zoom, _Time));
        }

        IEnumerator LerpZoomCoroutine(Camera _Camera, float _Zoom, float _Time)
        {
            yield return null;
            IsZooming = true;

            float _StartZoom = 0f;
            float tTime = 0f;

            if (_Camera.orthographic)
                _StartZoom = _Camera.orthographicSize;
            else
                _StartZoom = _Camera.fieldOfView;


            while (tTime <= _Time && IsZooming)
            {
                tTime += Time.deltaTime;

                if (_Camera.orthographic)
                    _Camera.orthographicSize = Mathf.Lerp(_StartZoom, _Zoom, tTime);
                else
                    _Camera.fieldOfView = Mathf.Lerp(_StartZoom, _Zoom, tTime);

                yield return null;
            }

            if (IsZooming)
            {
                if (_Camera.orthographic)
                    _Camera.orthographicSize = _Zoom;
                else
                    _Camera.fieldOfView = _Zoom;
            }

            IsZooming = false;
        }
        #endregion
    }
}
