using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace Dev6
{

    public class MainCanvas : MonoBehaviour {

        [InfoBox("Dieses Script speichert eine Reference von allen relevanten Canvas Daten. Für Datenzugriff bitte CanvasManager benutzen!")]
        [SerializeField][AutoAssign]RectTransform MainCanvasTransform;
        [SerializeField][AutoAssign]Canvas CanvasScript;
        [SerializeField][AutoAssign]CanvasScaler MainCanvasScaler;
        [SerializeField][AutoAssign]GraphicRaycaster MainCanvasGraphicRaycaster;

        private CanvasManager TheCanvasManager = null;

        void Awake()
        {
            //get the values automaticaly
            CanvasScript = this.GetSafeComponent<Canvas>();
            MainCanvasTransform = this.GetSafeComponent<RectTransform>();
            MainCanvasScaler = this.GetSafeComponent<CanvasScaler>();
            MainCanvasGraphicRaycaster = this.GetSafeComponent<GraphicRaycaster>();
        }

        void OnEnable()
        {
            //try get CanvasManager through MANAGER
            TheCanvasManager = MANAGER.GET.GetSingleton<CanvasManager>();

            if(TheCanvasManager != null)
                TheCanvasManager.RequestMainCanvasEvent += OnUpdateRequest;
        }

        void OnDisable()
        {
            if(TheCanvasManager != null)
                TheCanvasManager.RequestMainCanvasEvent -= OnUpdateRequest;
        }

        void OnUpdateRequest() //EVENT
        {
            //set the references:
            TheCanvasManager.MCanvas = CanvasScript;
            TheCanvasManager.MCTransform = MainCanvasTransform;
            TheCanvasManager.MCScaler = MainCanvasScaler;
            TheCanvasManager.MCGraphicRaycaster = MainCanvasGraphicRaycaster;

            //depricated
            //return new MainCanvasSettings(MainCanvasTransform, MainCanvasScaler, MainCanvasGraphicRaycaster);
        }
    }
}
