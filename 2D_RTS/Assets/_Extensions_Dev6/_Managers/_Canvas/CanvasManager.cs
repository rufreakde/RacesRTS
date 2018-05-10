using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

namespace Dev6
{
    [AddComponentMenu("Dev6/MANAGER/Canvas Manager")]
    public class CanvasManager : MonoBehaviour, IamSingleton
    {

        //event and delegate for Update Request! When fired all subscripted "MainCanvas.cs" will deliver and store their data.
        public delegate void GetMainCanvasDelegate();
        public event GetMainCanvasDelegate RequestMainCanvasEvent;

        //settings for setup
        [SimpleButton("iInitialize", typeof(CanvasManager))] //setup is also called on INIT! no Setup button needed its just for testing
        public bool DEBUG = false;
        [Header("#SetUp Settings:")]
        [SerializeField][ShowOnly] string canvasName = "CANVAS";
        [SerializeField][ShowOnly] string canvasTag = "MainCanvas";
        [SerializeField][ShowOnly] string canvasLayer = "UI";
        [Header("Canvas:")]
        [SerializeField] RenderMode renderMode = RenderMode.ScreenSpaceOverlay;
        [Header("Scaler:")]
        [SerializeField][ShowOnly] CanvasScaler.ScaleMode scaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
        [Header("Graphic Raycaster:")]
        [SerializeField][ShowOnly] bool ignoreReversedGraphics = true;
        [SerializeField] GraphicRaycaster.BlockingObjects blockingObj = GraphicRaycaster.BlockingObjects.None;

        [Header("Set by UpdateRequest:")]
        [InfoBox("Folgende Werte werden automatisch gesetzt nicht anfassen!!")]
        [AutoAssign]public RectTransform MCTransform;
        [AutoAssign]public Canvas MCanvas;
        [AutoAssign]public CanvasScaler MCScaler;
        [AutoAssign]public GraphicRaycaster MCGraphicRaycaster;

        //init the manager
        public void iInitialize()
        {
            if(DEBUG)
                Debug.Log("Setting Up Canvas...");
            //find existing canvas
            GameObject tCanvasGO = GameObject.FindGameObjectWithTag(canvasTag);
            if (tCanvasGO) //USE EXISTING SCENE CANVAS <- works but a canvas in the scene is annoying as hell
            {
                if(DEBUG)
                    Debug.Log("MainCanvas found keeping settings");

                MainCanvas tCanvas = tCanvasGO.GetComponent<MainCanvas>(); //kein getsafe weil das auch null sein darf!
                if (tCanvas == null)
                    tCanvas = tCanvasGO.AddComponent<MainCanvas>(); // add simple event receaver
                TransformLock tLock = tCanvasGO.GetComponent<TransformLock>();
                if (tLock == null)
                    tLock = tCanvasGO.AddComponent<TransformLock>();
            }
            else //CREATE DEFAULT <- Better because a Canvas in the scene sux!
            {
                if(DEBUG)
                    Debug.Log("No MainCanvas found creating default Canvas!");

                tCanvasGO = new GameObject(canvasName, typeof(RectTransform));
                tCanvasGO.layer = LayerMask.NameToLayer(canvasLayer);
                tCanvasGO.tag = canvasTag;

                MCanvas = tCanvasGO.AddComponent<Canvas>(); //no need to store because of "MainCanvas.cs" but do anyway because we "create them" and we dont want 1 frame null references!
                MCanvas.renderMode = renderMode;

                MCScaler = tCanvasGO.AddComponent<CanvasScaler>();
                MCScaler.uiScaleMode = scaleMode;

                MCGraphicRaycaster = tCanvasGO.AddComponent<GraphicRaycaster>();
                MCGraphicRaycaster.ignoreReversedGraphics = ignoreReversedGraphics;
                MCGraphicRaycaster.blockingObjects = blockingObj;

                tCanvasGO.AddComponent<TransformLock>(); //Add Transform Lock script
                tCanvasGO.AddComponent<MainCanvas>(); // this is going to hold the ref up-to-date
            }

            //INFO: 
            //Create Canvas we dont want to have a canvas in the scene!
            //we will have a canvas scene where we can edit the canvas UI prefabs! But no canvas in main scene it will be created via script ( kein nerviger canvas mehr in der szene)
            if(DEBUG)
                Debug.Log("...finished!");
        }

        //always keep information of canvas up to date
        void Update()
        {
            //sends an update per frame if we change the canvas for eg it will get the new information of the current canvas because
            //every MainCanvas script subscribes and unsubscribes to this event!
            if (RequestMainCanvasEvent != null)
                RequestMainCanvasEvent();
        }
    }



    //public struct MainCanvasSettings
    //{
    //    RectTransform MainCanvasTransform;
    //    CanvasScaler MainCanvasScaler;
    //    GraphicRaycaster MainCanvasGraphicRaycaster;

    //    public MainCanvasSettings(RectTransform _Transform, CanvasScaler _Scaler, GraphicRaycaster _Raycaster)
    //    {
    //        MainCanvasTransform = _Transform;
    //        MainCanvasScaler = _Scaler;
    //        MainCanvasGraphicRaycaster = _Raycaster;
    //    }
    //}
}
