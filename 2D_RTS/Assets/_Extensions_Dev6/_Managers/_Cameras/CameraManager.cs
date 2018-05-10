using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

namespace Dev6
{
    [AddComponentMenu("Dev6/MANAGER/Camera Manager")]
    public class CameraManager : MonoBehaviour, IamSingleton
    {
        [InfoBox("Cameras werden automatisch zur Liste hinzugefügt. Jede Camera braucht 'CameraManaged.cs'")]
        public bool DEBUG = false;
        [Header("Default Camera Settings: ")]
        public bool ShowQuickCameraCreaton = false;
        [ShowOnly] public string DefaultName = "MAIN_CAMERA";
        public bool AttachListener = true;
        public ProjType ProjectType = ProjType.Perspective;

        public List<CameraInfo> AllManagedCams = new List<CameraInfo>();

        public delegate void GetAllCameras();
        public event GetAllCameras RequestCameras;

        // Update is called once per frame
        void Update()
        {
            if (RequestCameras != null)
            {
                if(DEBUG)
                    Debug.Log("FIRE CAMERA REQUEST EVENT");
                RequestCameras();
            }
        }

        public void iInitialize()
        {
            //init the the Camera with basic settings

            //find Camera and add the Managed Component

            Debug.Log("TODO: " + this.ToString());
        }

        public void AddARequestedCamera(Camera _Cam)
        {
            for (int i = 0; i < AllManagedCams.Count; i++)
            {
                if (_Cam == AllManagedCams[i].Cam)
                {
                    if (DEBUG)
                        Debug.Log("Camera already referenced!" + this.ToString());
                    return;
                }
            }

            if (DEBUG)
                Debug.Log("New Camera added" + this.ToString());
            //if not already in list
            AllManagedCams.Add(new CameraInfo( _Cam ));
        }

        /// <summary>
        /// Get the instance of a Camera Script you want. Returns null if instance not found!
        /// </summary>
        /// <param name="_NameOfObj"></param>
        /// <returns></returns>
        public Camera GetCamera(string _NameOfObj)
        {
            for (int i = 0; i < AllManagedCams.Count; i++)
            {
                if (AllManagedCams[i].Name == _NameOfObj)
                {
                    return AllManagedCams[i].Cam;
                }
            }

            return null;
        }

        [System.Serializable]
        public class CameraInfo
        {
            public Camera Cam = null;
            [ShowOnly]
            public string Name = "ObjName";

            public CameraInfo(Camera _Cam)
            {
                Cam = _Cam;
                Name = _Cam.name;
            }
        }

        public enum ProjType
        {
            Perspective = 0,
            Orthographic = 1,
        }
    }
}
