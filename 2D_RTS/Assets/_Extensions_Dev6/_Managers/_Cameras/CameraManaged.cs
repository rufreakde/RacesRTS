using UnityEngine;
using System.Collections;

namespace Dev6
{
    [AddComponentMenu("Dev6/CAMERA/CameraData Managed")]
    public class CameraManaged : MonoBehaviour
    {
        private CameraManager theCamManager = null;
        [AutoAssign]public Camera MyCamera = null;

        void Awake()
        {
            if (!MyCamera)
                MyCamera = this.GetComponent<Camera>();
        }

        void OnEnable()
        {
            theCamManager = MANAGER.GET.GetSingleton<CameraManager>();

            if (theCamManager)
                theCamManager.RequestCameras += OnUpdateRequest;
            else
            {
                Debug.LogError("Didn't find the CameraManager!");
            }
        }

        void OnDisable()
        {
            if (theCamManager)
                theCamManager.RequestCameras -= OnUpdateRequest;
        }

        void OnUpdateRequest()
        {
            if (theCamManager != null)
            {
                theCamManager.AddARequestedCamera(MyCamera);
            }
        }
    }
}
