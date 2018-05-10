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
    [AddComponentMenu("Dev6/CAMERA/Screenshake")]
    public class ScreenShake : MonoBehaviour
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
        }

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /********************
            * SCREENSHAKE  *
            ********************/
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #region ScreenShake
        bool ScreenIsShaking = false;
        public bool IsShaking
        {
            get { return ScreenIsShaking; }
        }

        /// <summary>
        /// Shakes shake into random directions. Stops all other Coroutines on this behaviourscript!
        /// </summary>
        /// <param name="_Time"></param>
        /// <param name="_Intensity"></param>
        public void Shake(float _Time, float _Intensity)
        {
            if (ScreenIsShaking)
                StopAllCoroutines();

            StartCoroutine(ScreenShakeCoroutine(CamTransform, _Time, _Intensity));
        }

        /// <summary>
        /// Shakes shake into random directions. Stops all other Coroutines on this behaviourscript!
        /// </summary>
        /// <param name="_Time"></param>
        /// <param name="_Intensity"></param>
        public void Shake(float _Time, float _Intensity, AXIS _Axis)
        {
            if (ScreenIsShaking)
                StopAllCoroutines();

            StartCoroutine(ScreenShakeCoroutine(CamTransform, _Time, _Intensity, _Axis));
        }

        //Coroutine direction of Vector
        IEnumerator ScreenShakeCoroutine(Transform _CameraTrans, float _Duration, float _ShakeIntensity)
        {
            //store start values
            Vector3 StartTransformPos = _CameraTrans.position;
            Quaternion StartTransformRot = _CameraTrans.rotation;
            float tTime = _Duration;

            yield return null; //safe that no other shake coroutine is running atm

            while (tTime > 0.0f) //shake
            {
                if (Cam.orthographic)//shake 2d
                {
                    Vector2 tShakePos = Random.insideUnitCircle * _ShakeIntensity;
                    _CameraTrans.position = new Vector3(tShakePos.x + StartTransformPos.x, tShakePos.y + StartTransformPos.y, StartTransformPos.z);
                }
                else //shake 3d
                {
                    Vector3 tShakePos = Random.insideUnitSphere * _ShakeIntensity;
                    _CameraTrans.position = StartTransformPos + tShakePos;
                }

                tTime -= Time.deltaTime;
                yield return new WaitForEndOfFrame();
            }

            _CameraTrans.position = StartTransformPos;
            _CameraTrans.rotation = StartTransformRot;
        }

        //Coroutine direction of Vector
        IEnumerator ScreenShakeCoroutine(Transform _CameraTrans, float _Duration, float _ShakeIntensity, AXIS _Axis)
        {
            //store start values
            Vector3 StartTransformPos = _CameraTrans.position;
            Quaternion StartTransformRot = _CameraTrans.rotation;
            float tTime = _Duration;
            float tAxisValue = 0f;

            yield return null; //safe that no other shake coroutine is running atm

            while (tTime > 0.0f) //shake
            {
                tAxisValue = Random.Range(-1f, 1f);

                Debug.Log("Screenshake new " + _Axis);

                switch (_Axis)
                {
                    default: _CameraTrans.position = new Vector3(StartTransformPos.x, StartTransformPos.y, StartTransformPos.z); break;
                    case AXIS.X: _CameraTrans.position = new Vector3(StartTransformPos.x + tAxisValue, StartTransformPos.y, StartTransformPos.z); break;
                    case AXIS.Y: _CameraTrans.position = new Vector3(StartTransformPos.x, StartTransformPos.y + tAxisValue, StartTransformPos.z); break;
                    case AXIS.Z: _CameraTrans.position = new Vector3(StartTransformPos.x, StartTransformPos.y, StartTransformPos.z + tAxisValue); break;
                    case AXIS._COUNT: Debug.LogError("Cant use COUNT of AXIS enum to screenshake! " + this.ToString()); break;
                }

                tTime -= Time.deltaTime;
                yield return new WaitForEndOfFrame();
            }

            _CameraTrans.position = StartTransformPos;
            _CameraTrans.rotation = StartTransformRot;
        }

        #endregion
    }
}
