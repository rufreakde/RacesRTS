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
    [AddComponentMenu("Dev6/CAMERA/Move Rotate LookAt")]
    public class CameraMoveRotateLookAt : MonoBehaviour
    {
        [Split("CameraMoveRotateLookAt")]
        public Transform TargetLookAt = null;

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

        void Update()
        {
            LookAtUpdate();
        }

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /********************
         * MOVE and ROTATE  *
         ********************/
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #region Movement
        bool isMoving = false;
        public bool IsMoving
        {
            get { return isMoving; }
            set { isMoving = value; }
        }
        bool isRotating = false;
        public bool IsRotating
        {
            get { return isRotating; }
            set { isRotating = value; if (value) isMoving = false; } //if true disable look at
        }
        bool isLookingAt = false;
        public bool IsLookingAt
        {
            get { return isLookingAt; }
            set { isLookingAt = value; if (value) IsRotating = false; }  //if true disable rotation
        }

        /// <summary>
        /// Move to specific position via lerp function.
        /// </summary>
        /// <param name="_TargetPos"></param>
        /// <param name="_Time"></param>
        /// <param name="_Speed"></param>
        public void LerpToPos(Vector3 _TargetPos, float _Time)
        {
            if (IsMoving)
            {
                IsMoving = false;
            }

            StartCoroutine(LerpToPosCoroutine(CamTransform, _TargetPos, _Time));
        }

        /// <summary>
        /// Move to specific position via lerp function. This is not fpr moving targets!
        /// </summary>
        /// <param name="_TargetPos"></param>
        /// <param name="_Time"></param>
        /// <param name="_Speed"></param>
        public void LerpToPos(Transform _TargetPos, float _Time)
        {
            if (IsMoving)
            {
                IsMoving = false;
            }

            StartCoroutine(LerpToPosCoroutine(CamTransform, _TargetPos.position, _Time));
        }

        IEnumerator LerpToPosCoroutine(Transform _Camera, Vector3 _TargetPos, float _Time)
        {
            yield return null; //starts 1 frame later so the abort will be in effect and not move functions will be played together over several frames!
            IsMoving = true;

            Vector3 _StartPos = _Camera.position;
            float tTime = 0f;

            while (tTime <= _Time && IsMoving)  //if IsMoving will be set to false co routine aborts!
            {
                tTime += Time.deltaTime;
                _Camera.position = Vector3.Slerp(_StartPos, _TargetPos, tTime);

                yield return null;
            }

            if (IsMoving)    //will be true if not abbortet!
                _Camera.position = _TargetPos; //if coroutine was not abborted set the position to target position

            IsMoving = false; // finished
        }

        /// <summary>
        /// Rotate to specific rotation values.
        /// </summary>
        /// <param name="_TargetRotation"></param>
        /// <param name="_Time"></param>
        /// <param name="_Speed"></param>
        public void LerpRotation(Vector3 _TargetRotation, float _Time)
        {
            if (IsRotating)
            {
                IsRotating = false;
            }
            StartCoroutine(LerpToRotCoroutine(CamTransform, Quaternion.Euler(_TargetRotation), _Time));
        }

        /// <summary>
        /// Rotate to specific rotation values.
        /// </summary>
        /// <param name="_TargetRotation"></param>
        /// <param name="_Time"></param>
        /// <param name="_Speed"></param>
        public void LerpRotation(Quaternion _TargetRotation, float _Time)
        {
            if (IsRotating)
            {
                IsRotating = false;
            }
            StartCoroutine(LerpToRotCoroutine(CamTransform, _TargetRotation, _Time));
        }

        /// <summary>
        /// Rotate to specific rotation values. This is not for rotating targets!
        /// </summary>
        /// <param name="_TargetRotation"></param>
        /// <param name="_Time"></param>
        /// <param name="_Speed"></param>
        public void LerpRotation(Transform _TargetRotation, float _Time)
        {
            if (IsRotating)
            {
                IsRotating = false;
            }
            StartCoroutine(LerpToRotCoroutine(CamTransform, _TargetRotation.rotation, _Time));
        }

        IEnumerator LerpToRotCoroutine(Transform _Camera, Quaternion _TargetRotation, float _Time)
        {
            yield return null;
            IsRotating = true;

            Quaternion _StartRot = _Camera.rotation;
            float tTime = 0f;

            while (tTime <= _Time && IsRotating)
            {
                tTime += Time.deltaTime;
                _Camera.rotation = Quaternion.Slerp(_StartRot, _TargetRotation, tTime);
                yield return null;
            }

            if (IsRotating)
                _Camera.rotation = _TargetRotation;

            IsRotating = false;
        }

        #endregion

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /********************
         * LOOKAT (does not work simultaniously with rotate)  *
         ********************/
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #region LookAt
        private void LookAtUpdate()
        {
            if (IsLookingAt)
            {
                if (TargetLookAt)
                    CamTransform.LookAt(TargetLookAt);
                else
                {
                    Debug.LogWarning("Coud not find a target to look at for Camera: " + Cam);
                    IsLookingAt = false;
                }
            }
        }
        #endregion
    }
}